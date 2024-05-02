using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Model;
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
using KomalliEmployee.Views.Pages;
using System.Collections.Specialized;

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl {
        public FoodUserControl() {
            InitializeComponent();
        }

        public FoodModel Food { get; set; }


        

        public void BindData() {
            if (Food != null) {
                tbkFoodName.Text = Food.Name;
                tbkFoodPrice.Text = "$" + Food.Price.ToString();
                
            }
        }

        private void ClickAddFood(object sender, RoutedEventArgs e) {
            
            int price = int.Parse(tbkFoodPrice.Text.TrimStart('$'));
            FoodModel foodModel = new FoodModel() {
                KeyCard = Food.KeyCard,
                Name = tbkFoodName.Text,
                Quantity = 1,
                Price = price,
                IsSelected = true
            };
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);

            if(selectedFood != null) {
                
                selectedFood.Quantity++;
                selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
                

                var foodName = tbkFoodName.Text;
                var changeType = SingletonClass.Instance.SelectedFoods.Any(food => food.Name == foodName) ?
                                 NotifyCollectionChangedAction.Replace : NotifyCollectionChangedAction.Add;

                // Construir un NotifyCollectionChangedEventArgs personalizado
                var args = new NotifyCollectionChangedEventArgs(changeType, new List<FoodModel> { new FoodModel { Name = foodName } }, SingletonClass.Instance.SelectedFoods);
                MakeOrder parentWindow = FindParent<MakeOrder>(this);
                parentWindow?.SelectedFoodsCollectionChanged(sender, args);

            } else {

                SingletonClass.Instance.SelectedFoods.Add(foodModel);

            }

            
            
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject {
            while ((child = VisualTreeHelper.GetParent(child)) != null) {
                if (child is T parent) return parent;
            }
            return null;
        }
    }
}
