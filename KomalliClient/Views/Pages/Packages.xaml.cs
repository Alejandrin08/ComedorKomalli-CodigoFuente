using KomalliClient.Controller;
using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliClient.Views.UserControls;
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

namespace KomalliClient.Views.Pages {
    /// <summary>
    /// Lógica de interacción para Packages.xaml
    /// </summary>
    public partial class Packages : Page {
        public Packages() {
            InitializeComponent();
            ShowPackages();
        }
        private void ShowPackages() {
            var foods = new FoodController().GetFoodsPerSection("Paquetes");

            if (foods != null) {
                var wrapPanel = new WrapPanel { Orientation = Orientation.Horizontal };

                bool antojitosOrTortasSelected = SingletonClass.Instance.SelectedFoods.Any(food =>
                    food.Section == "Antojitos" || food.Section == "Tortas" || food.Name == "Pechuga a la plancha");

                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData();
                    foodControl.Margin = new Thickness(8);

                    if (food.Name == "Pechuga a la plancha") {
                        foodControl.Margin = new Thickness(8, 25, 0, 0);
                    } else {
                        foodControl.IsEnabled = antojitosOrTortasSelected;
                        foodControl.Opacity = antojitosOrTortasSelected ? 1.0 : 0.5;
                    }
                    wrPFood.Children.Add(foodControl);
                }
                grdContent.Children.Add(wrapPanel);
            } else {
                App.ShowMessageError("No se pudo recuperar los paquetes", "Error");
            }
        }

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
