using KomalliClient.Controller;
using KomalliClient.Model;
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

                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData();
                    foodControl.Margin = new Thickness(8);
                    if (foodControl.Food.Name == "Pechuga a la plancha") {
                        foodControl.Margin = new Thickness(8,25,0,0);
                    }
                    wrPFood.Children.Add(foodControl);
                }
                grdContent.Children.Add(wrapPanel);
            }
        }
    }
}
