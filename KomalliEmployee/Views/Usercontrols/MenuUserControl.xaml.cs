using KomalliEmployee.Controller;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl {
  
        public MenuUserControl() {
            InitializeComponent();
        }
        public void BindData(string typeMenu) {            
            tbkFoodName.Text = typeMenu;            
        }

        

        public string GetKeyMenuSelected() {
            string keyMenu = "";
            if (tbkFoodName.Text == "Desayuno") {
                keyMenu = SingletonClass.Instance.keyBreakfast;
            } else {
                keyMenu = SingletonClass.Instance.KeyMeal;
            }
            return keyMenu;
        }

        private void ClickAddMenu(object sender, RoutedEventArgs e) {
            FoodModel foodModel = new FoodModel();
            string keyMenu = GetKeyMenuSelected();
            string menu = "";

            if (rdbPublic.IsChecked == true) {
                foodModel = SetInfo(keyMenu, " Público G.", 50);
                menu = tbkFoodName.Text + " Público G.";
            } else if (rdbStudent.IsChecked==true) {
                foodModel = SetInfo(keyMenu, " Estudiante", 30);
                menu = tbkFoodName.Text + " Estudiante";
            }

            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == menu);

            FoodController foodController = new FoodController();
            int stock = foodController.GetStockSetMenu(keyMenu);
            if (stock > 0) {
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
                    UpdateSelectedFoodsCollection(menu, sender);
                } else {
                    SingletonClass.Instance.SelectedFoods.Add(foodModel);
                }
            } else {
                App.ShowMessageWarning("Ya no hay.", "Advertencia");
                return;
            }
            ResetOptions();
        }

        private FoodModel SetInfo(string keyMenu, string typeClient, int price) {
            FoodModel foodModel = new FoodModel();
            foodModel = new FoodModel() {
                KeyCard = keyMenu,
                Name = tbkFoodName.Text + typeClient,
                Price = price,
                Quantity = 1,
            };
            return foodModel;
        }

        private void ResetOptions() {
            rdbStudent.IsChecked = false;
            rdbPublic.IsChecked = false;
            btnAddMenu.IsEnabled = false;
        }

        private void UpdateSelectedFoodsCollection(string menu, object sender) {
            var foodName = menu;
            var changeType = SingletonClass.Instance.SelectedFoods.Any(food => food.Name == foodName) ?
                             NotifyCollectionChangedAction.Replace : NotifyCollectionChangedAction.Add;

            var args = new NotifyCollectionChangedEventArgs(changeType, new List<FoodModel> { new FoodModel { Name = foodName } }, SingletonClass.Instance.SelectedFoods);

            MakeOrder parentWindow = FindParent<MakeOrder>(this);
            parentWindow?.SelectedFoodsCollectionChanged(sender, args);
        }

        private void CheckedRadioButton(object sender, RoutedEventArgs e) {
            if (rdbStudent.IsChecked == true || rdbPublic.IsChecked == true) {
                btnAddMenu.IsEnabled = true;                
            } else {
                btnAddMenu.IsEnabled = false;
            }
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
