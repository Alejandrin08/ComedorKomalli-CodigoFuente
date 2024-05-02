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

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl {

        public FoodModel Food { get; set; }
        public MenuUserControl() {
            InitializeComponent();
            ksd();
        }

        public void BindData(string typeMenu) {
            
                tbkFoodName.Text = typeMenu;
            
        }

        public void ksd() {
            FoodController foodController = new FoodController();
            var menu = foodController.GetKeyMenu();
            foreach (var food in menu) {
                if (food.KeyCard.StartsWith("Des")) {
                    SingletonClass.Instance.keyBreakfast = food.KeyCard;
                } else if (food.KeyCard.StartsWith("Com")){
                    SingletonClass.Instance.KeyMeal = food.KeyCard;
                }
            }
        }

        private void ClickAddMenu(object sender, RoutedEventArgs e) {
            FoodModel foodModel = new FoodModel();
            string keyMenu = "";
            if (tbkFoodName.Text == "Desayuno") {
                keyMenu = SingletonClass.Instance.keyBreakfast;
            } else {
                keyMenu = SingletonClass.Instance.KeyMeal;
            }
            string menu = "";

                if (rdbPublic.IsChecked == true) {
                foodModel = new FoodModel() {
                    KeyCard = keyMenu,
                    Name = tbkFoodName.Text + " Público G.",
                    Price = 50,
                    Quantity= 1,
                    IsSelected = true
                };
                menu = tbkFoodName.Text + " Público G.";
            } else if (rdbStudent.IsChecked==true) {
                foodModel = new FoodModel() {
                    KeyCard = keyMenu,
                    Name = tbkFoodName.Text + " Estudiante",
                    Price = 30,
                    Quantity = 1,
                    IsSelected = true
                };
                menu = tbkFoodName.Text + " Estudiante";
            }

            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(food => food.Name == menu);

            if (selectedFood != null) {

                selectedFood.Quantity++;
                selectedFood.Subtotal = selectedFood.Quantity * selectedFood.Price;


                var foodName = menu;
                var changeType = SingletonClass.Instance.SelectedFoods.Any(food => food.Name == foodName) ?
                                 NotifyCollectionChangedAction.Replace : NotifyCollectionChangedAction.Add;

                // Construir un NotifyCollectionChangedEventArgs personalizado
                var args = new NotifyCollectionChangedEventArgs(changeType, new List<FoodModel> { new FoodModel { Name = foodName } }, SingletonClass.Instance.SelectedFoods);
                MakeOrder parentWindow = FindParent<MakeOrder>(this);
                parentWindow?.SelectedFoodsCollectionChanged(sender, args);

            } else {

                SingletonClass.Instance.SelectedFoods.Add(foodModel);

            }
            rdbStudent.IsChecked = false;
            rdbPublic.IsChecked = false;
            btnAddMenu.IsEnabled = false;
        }

        private void CheckedRadioButton(object sender, RoutedEventArgs e) {
            if (rdbStudent.IsChecked == true || rdbPublic.IsChecked == true) {
                btnAddMenu.IsEnabled = true;
                
            } else {
                btnAddMenu.IsEnabled = false;
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
