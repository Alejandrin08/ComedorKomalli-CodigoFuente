using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using System;
using System.Collections.Generic;
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

namespace KomalliClient.Views.UserControls {
    /// <summary>
    /// Lógica de interacción para FoodDetails.xaml
    /// </summary>
    public partial class FoodDetails : UserControl {

        public FoodModel Food { get; set; }
        public FoodDetails() {
            InitializeComponent();
        }

        public void BindData(FoodModel food) {
            Food = food;
            if (food != null) {
                txtbFoodName.Text = food.Name;
                tbkFoodPrice.Text = "$" + food.Subtotal.ToString();
                tbkQuantityFood.Text = food.Quantity.ToString();

                if (food.Quantity > 1) {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Substract.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                } else if (food.Quantity == 1) {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Trash.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                }
            }
        }

        private void MouseDownDeleteFood(object sender, MouseButtonEventArgs e) {
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            if (selectedFood == null) return;

            if (selectedFood.Quantity > 1) {
                selectedFood.Quantity--;
                tbkQuantityFood.Text = selectedFood.Quantity.ToString();

                if (selectedFood.Quantity == 1) {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Trash.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                }
            } else {
                RemoveSelectedFood(selectedFood);
            }

            UpdateFoodDetails(selectedFood);
        }

        private void MouseDownAddFood(object sender, MouseButtonEventArgs e) {
            var existingFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            if (existingFood != null) {
                if (existingFood.Quantity == 9) {
                    App.ShowMessageWarning("No puedes agregar más de 9 unidades de un mismo producto.", "Advertencia");
                    return;
                }
                existingFood.Quantity++;
                tbkQuantityFood.Text = existingFood.Quantity.ToString();
                UpdateFoodDetails(existingFood);

                if (existingFood.Quantity > 1) {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Substract.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                }
            }
        }

        private void RemoveSelectedFood(FoodModel food) {
            SingletonClass.Instance.SelectedFoods.Remove(food);
            FoodUserControl.SelectedStates.Remove(food.Name);

            if (SingletonClass.Instance.SelectedFoods.Count == 0) {
                DisableMainButtons();
                FoodUserControl.SelectedStates.Clear();
            }

            MainWindow parentWindow = FindParent<MainWindow>(this);
            parentWindow?.UpdateTotalPrice();
        }

        private void DisableMainButtons() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.btnContinue.IsEnabled = false;
            mainWindow.btnCancel.IsEnabled = false;
        }

        private void UpdateFoodDetails(FoodModel food) {
            food.Subtotal = food.Quantity * food.Price;
            tbkFoodPrice.Text = "$" + food.Subtotal.ToString();

            MainWindow parentWindow = FindParent<MainWindow>(this);
            parentWindow?.UpdateTotalPrice();
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject {
            T parent = null;
            while ((child = VisualTreeHelper.GetParent(child)) != null) {
                if (child is T typedParent) {
                    parent = typedParent;
                    break;
                }
            }
            return parent;
        }
    }
}
