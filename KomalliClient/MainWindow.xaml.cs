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

        public void UpdateTotalPrice() {
            int totalPrice = 0;
            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                totalPrice += food.Subtotal;
            }
            txtbTotalPrice.Text = "$" + totalPrice.ToString();
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
                }
            }
        }

        private void ClickContinueWithPaymet(object sender, RoutedEventArgs e) {
            //Aqui se agregará la continuación al caso de uso 9. Generar pedido.
            //Para obtener la información del pedido debe ser algo como lo siguiente
            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                Console.WriteLine($"Nombre del pedido: {food.Name}, Precio: {food.Price}, Cantidad: {food.Quantity}, Seccion: {food.Section}");
                Console.WriteLine($"Subtotal: {food.Subtotal}");
            }
        }
    }
}
