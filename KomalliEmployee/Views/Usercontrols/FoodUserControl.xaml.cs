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
using KomalliEmployee.Controller;

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl {
        public FoodModel Food { get; set; }
        public FoodUserControl() {
            InitializeComponent();
        }

        public void BindData(FoodModel food) {
            Food = food;
            if (food != null) {
                tbkFoodName.Text = Food.Name;
                tbkFoodPrice.Text = "$" + Food.Price.ToString();                
            }
        }

        private void ClickAddFood(object sender, RoutedEventArgs e) {
            
            int price = int.Parse(tbkFoodPrice.Text.TrimStart('$'));
            FoodModel foodModel = new FoodModel() {
                KeyCard = Food.KeyCard,
                Name = tbkFoodName.Text,
                Section = Food.Section,
                Quantity = 1,
                Price = price
            };
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == Food.Name);

            FoodController foodController = new FoodController();
            int stock = foodController.GetStockMenuCard(Food.KeyCard);
            if(stock > 0) {
                if (selectedFood != null) {
                    if (selectedFood.Quantity == 9) {
                        App.ShowMessageWarning("No puedes agregar más de 9 unidades de un mismo producto.", "Advertencia");
                        return;
                    }
                    int quantity = selectedFood.Quantity;
                    if (quantity++ >= stock) {
                        App.ShowMessageWarning("No puedes agregar más, ya no hay.", "Advertencia");
                        return;
                    }
                    selectedFood.Quantity++;
                    selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;
                    UpdateSelectedFoodsCollection(sender);
                } else {
                    SingletonClass.Instance.SelectedFoods.Add(foodModel);
                }
            } else {
                App.ShowMessageWarning("Ya no hay.", "Advertencia");
                return;
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
