﻿using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliClient.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KomalliClient {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            SingletonClass.Instance.SelectedFoods.CollectionChanged += SelectedFoodsCollectionChanged;
        }

        public void SelectedFoodsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            lvwSelectedFoods.Items.Clear();

            WrapPanel wrapPanel = FindWrapPanel(this);

            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                FoodDetails foodDetails = new FoodDetails();
                foodDetails.BindData(food);
                lvwSelectedFoods.Items.Add(foodDetails);

                if (wrapPanel != null) {
                    FoodUserControl foodControl = wrapPanel.Children.OfType<FoodUserControl>().FirstOrDefault(control => control.Food.Name == food.Name);
                    if (foodControl != null) {
                        foodControl.IsSelected = true;
                    }
                }
            }
            UpdateTotalPrice();
        }

        private WrapPanel FindWrapPanel(DependencyObject parent) {
            WrapPanel result = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is WrapPanel wrapPanel && wrapPanel.Name == "wrpFood") {
                    result = wrapPanel;
                    break;
                }
                result = FindWrapPanel(child);
                if (result != null)
                    break;
            }
            return result;
        }

        public void UpdateTotalPrice() {
            int totalPrice = 0;
            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                totalPrice += food.Subtotal;
            }
            tbkTotalPrice.Text = "$" + totalPrice.ToString();
        }

        private void ClickClose(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ClickRestore(object sender, RoutedEventArgs e) {
            if (WindowState == WindowState.Normal) {
                WindowState = WindowState.Maximized;
            } else {
                WindowState = WindowState.Normal;
            }
        }

        private void ClickMinimize(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void ClickCancel(object sender, RoutedEventArgs e) {
            if (SingletonClass.Instance.SelectedFoods.Count > 0) {
                MessageBoxResult result = App.ShowMessageBoxButton("¿Está seguro de cancelar la orden?", "Confirmación");
                if (result == MessageBoxResult.Yes) {
                    SingletonClass.Instance.SelectedFoods.Clear();
                    btnCancel.IsEnabled = false;
                    btnContinue.IsEnabled = false;
                    FoodUserControl.SelectedStates.Clear();
                    Views.Pages.HomeMenu homeMenu = new Views.Pages.HomeMenu();
                    fraPages.Content = homeMenu;
                }
            }
        }

        private void ClickContinueWithPaymet(object sender, RoutedEventArgs e) {
            Views.Pages.GenerateOrder generateOrder = new Views.Pages.GenerateOrder();
            fraPages.Content = generateOrder;
            grdLabel.Visibility = Visibility.Collapsed;
        }
    }
}
