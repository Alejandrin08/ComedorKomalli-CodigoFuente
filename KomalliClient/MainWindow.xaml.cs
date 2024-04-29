using KomalliClient.Model;
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
        private void SelectedFoodsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            lstSelectedFoods.Items.Clear();
            
            WrapPanel wrapPanel = FindWrapPanel(this);

            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                FoodDetails foodDetails = new FoodDetails();
                foodDetails.BindData(food);
                lstSelectedFoods.Items.Add(foodDetails);

                if (food.IsSelected) {
                    foreach (var child in wrapPanel.Children) {
                        if (child is FoodUserControl foodControl && foodControl.Food.Name == food.Name) {
                            foodControl.IsEnabled = false;
                            foodControl.Opacity = 0.5;
                        }
                    }
                }
            }
            txtbTotalPrice.Text = "$" + CalculateTotalPrice().ToString();
        }

        private WrapPanel FindWrapPanel(DependencyObject parent) {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is WrapPanel wrapPanel && wrapPanel.Name == "wrPFood") {
                    return wrapPanel;
                }

                var result = FindWrapPanel(child);
                if (result != null)
                    return result;
            }

            return null;
        }

        private int CalculateTotalPrice() {
            int totalPrice = 0;
            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                totalPrice += food.Price;
            }
            return totalPrice;
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
    }
}
