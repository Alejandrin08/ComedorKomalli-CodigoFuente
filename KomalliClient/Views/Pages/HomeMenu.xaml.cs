using KomalliClient.Controller;
using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace KomalliClient.Views.Pages {
    /// <summary>
    /// Lógica de interacción para HomeMenu.xaml
    /// </summary>
    public partial class HomeMenu : Page {

        private MenuController _menuController;

        public HomeMenu() {
            InitializeComponent();
            _menuController = new MenuController();

            MenuModel menuBreakfast = _menuController.GetBreakfastOfTheDay();
            if (menuBreakfast != null) {
                lstBreakfast.ItemsSource = new List<MenuModel> { menuBreakfast };
            } else {
                App.ShowMessageError("No se pudo recuperar el menú del desayuno", "Error");
            }

            MenuModel menuFood = _menuController.GetFoodOfTheDay();
            if (menuFood != null) {
                lstFood.ItemsSource = new List<MenuModel> { menuFood };
            } else {
                App.ShowMessageError("No se pudo recuperar el menú de comida", "Error");
            }

            if (IsBreakfastTime()) {
                rctBreakfast.IsEnabled = true;
                rctFood.IsEnabled = false;
            } else {
                rctBreakfast.IsEnabled = false;
                rctFood.IsEnabled = true;
            }
        }

        private bool IsBreakfastTime() {
            DateTime now = DateTime.Now;

            DateTime breakfastStart = DateTime.Today.AddHours(8); 
            DateTime breakfastEnd = DateTime.Today.AddHours(12).AddMinutes(30); 

            bool isBreakfastTime = now >= breakfastStart && now <= breakfastEnd;

            return isBreakfastTime;
        }

        private void MouseDownBreakfast(object sender, MouseButtonEventArgs e) {
            if (!SingletonClass.Instance.SelectedFoods.Any(food => food.Name == "Menú Desayuno")) {
                MessageBoxResult result = App.ShowMessageBoxButton("¿Es estudiante de la Universidad Veracruzana?", "Confirmación");
                if (result == MessageBoxResult.Yes) {
                    SingletonClass.Instance.Price = 30;
                } else if (result == MessageBoxResult.No) {
                    SingletonClass.Instance.Price = 50;
                }

                FoodModel foodModel = new FoodModel() {
                    Name = "Menú Desayuno",
                    Price = SingletonClass.Instance.Price,
                    IsSelected = true,
                    Section = "Menú a la carta",
                    Quantity = 1,
                    Subtotal = SingletonClass.Instance.Price
                };
                SingletonClass.Instance.SelectedFoods.Add(foodModel);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.btnContinue.IsEnabled = true;
                mainWindow.btnCancel.IsEnabled = true;
            }
        }

        private void MouseDownFood(object sender, MouseButtonEventArgs e) {
            if (!SingletonClass.Instance.SelectedFoods.Any(food => food.Name == "Menú Comida")) {
                MessageBoxResult result = App.ShowMessageBoxButton("¿Es estudiante de la Universidad Veracruzana?", "Confirmación");
                if (result == MessageBoxResult.Yes) {
                    SingletonClass.Instance.Price = 30;
                } else if (result == MessageBoxResult.No) {
                    SingletonClass.Instance.Price = 50;
                }

                FoodModel foodModel = new FoodModel() {
                    Name = "Menú Comida",
                    Price = SingletonClass.Instance.Price,
                    IsSelected = true,
                    Section = "Menú a la carta",
                    Quantity = 1,
                    Subtotal = SingletonClass.Instance.Price
                };
                SingletonClass.Instance.SelectedFoods.Add(foodModel);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.btnContinue.IsEnabled = true;
                mainWindow.btnCancel.IsEnabled = true;
            }
        }

        private void MouseDownPackages(object sender, MouseButtonEventArgs e) {
            Packages packagesView = new Packages();
            this.NavigationService.Navigate(packagesView);
        }

        private void MouseDownAntojitos(object sender, MouseButtonEventArgs e) {
            Antojitos antojitosView = new Antojitos();
            this.NavigationService.Navigate(antojitosView);
        }

        private void MouseDownGarnish(object sender, MouseButtonEventArgs e) {
            Garnish garnishView = new Garnish();
            this.NavigationService.Navigate(garnishView);
        }

        private void MouseDownSandwhiches(object sender, MouseButtonEventArgs e) {
            Sandwhiches sandwhichesView = new Sandwhiches();
            this.NavigationService.Navigate(sandwhichesView);
        }

        private void MouseDownDrinks(object sender, MouseButtonEventArgs e) {
            Drinks drinksView = new Drinks();
            this.NavigationService.Navigate(drinksView);
        }

        private void MouseDownDesserts(object sender, MouseButtonEventArgs e) {
            Desserts dessertsView = new Desserts();
            this.NavigationService.Navigate(dessertsView);
        }
        private void MouseDownOthers(object sender, MouseButtonEventArgs e) {
            Others othersView = new Others();
            this.NavigationService.Navigate(othersView);
        }
    }
}
