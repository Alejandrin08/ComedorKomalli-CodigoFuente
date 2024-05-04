using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Pages;
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
                tbkFoodName.Text = food.Name;
                if(food.Quantity == 1) {
                    SetImage("Trash.png");                    
                } else if(food.Quantity > 1){
                    SetImage("Substract.png");                    
                }
                int subtotal = food.Quantity * food.Price;
                tbkFoodSubtotal.Text = "$ " + subtotal.ToString();
            }
        }

        private void SetImage (string image) {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/Resources/Images/"+ image, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgDeleteFood.Source = bitmap;
        }

        

        private void MouseDownDeleteOrDecrease(object sender, MouseButtonEventArgs e) {
            int quantity = int.Parse(tbkQuantityFood.Text);
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            if (quantity > 1) {
                selectedFood.Quantity--;
                selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
                tbkQuantityFood.Text = selectedFood.Quantity.ToString();
                UpdateSelectedFoodsCollection(sender);
                if (selectedFood.Quantity == 1) {
                    SetImage("Trash.png");
                }
            } else {
                SingletonClass.Instance.SelectedFoods.Remove(selectedFood);
            }
        }

        private void MouseDownAddFood(object sender, MouseButtonEventArgs e) {
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);
            selectedFood.Quantity++;
            selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
            tbkQuantityFood.Text = selectedFood.Quantity.ToString();
            tbkFoodSubtotal.Text =  "$ " + selectedFood.Subtotal.ToString();
            UpdateSelectedFoodsCollection(sender);
            if (selectedFood.Quantity > 1) {
                SetImage("Substract.png");
            }
        }


        private void UpdateSelectedFoodsCollection(object sender) {
            var foodName = tbkFoodName.Text;
            var changeType = SingletonClass.Instance.SelectedFoods.Any(food => food.Name == foodName) ?
                             NotifyCollectionChangedAction.Replace : NotifyCollectionChangedAction.Add;

            var args = new NotifyCollectionChangedEventArgs(changeType, new List<FoodModel> { new FoodModel { Name = foodName } }, SingletonClass.Instance.SelectedFoods);

            MakeOrder parentWindow = FindParent<MakeOrder>(this);
            parentWindow?.SelectedFoodsCollectionChanged(sender, args);
        }

        public void UpdateQuantity(int quantity) {
            tbkQuantityFood.Text = quantity.ToString();
        }

        public void UpdateSubtotal (int subtotal) {
            tbkFoodSubtotal.Text = "$ " + subtotal.ToString();
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
