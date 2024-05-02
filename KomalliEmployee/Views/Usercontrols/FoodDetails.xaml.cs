using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
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

namespace KomalliEmployee.Views.Usercontrols {
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
                if(food.Quantity == 1) {
                    BitmapImage bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Trash.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                } else if(food.Quantity > 1){
                    BitmapImage bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Substract.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                }
            }
        }

        

        private void MouseDownDelete(object sender, MouseButtonEventArgs e) {
            int quantity = int.Parse(txtbQuantityFood.Text);
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            if (quantity > 1) {
                selectedFood.Quantity--;
                selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
                txtbQuantityFood.Text = selectedFood.Quantity.ToString();
                if(selectedFood.Quantity == 1) {
                    BitmapImage bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("/Resources/Images/Trash.png", UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    imgDeleteFood.Source = bitmap;
                }
            } else {
                SingletonClass.Instance.SelectedFoods.Remove(selectedFood);
            }
            

        }

        private void MouseDownAddFood(object sender, MouseButtonEventArgs e) {
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            selectedFood.Quantity++;
            selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
            txtbQuantityFood.Text = selectedFood.Quantity.ToString();

            if(selectedFood.Quantity > 1) {
                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();
                bitmap.UriSource = new Uri("/Resources/Images/Substract.png", UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imgDeleteFood.Source = bitmap;
            }
        }

        public void UpdateQuantity(int quantity) {
            // Actualiza la cantidad mostrada en el control con el nuevo valor
            txtbQuantityFood.Text = quantity.ToString();
        }
    }
}
