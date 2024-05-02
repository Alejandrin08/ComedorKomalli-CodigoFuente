using KomalliClient.Controller;
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
    /// Lógica de interacción para Drinks.xaml
    /// </summary>
    public partial class Drinks : Page {
        public Drinks() {
            InitializeComponent();
            ShowDrinks();
        }

        private void ShowDrinks() {
            var foods = new FoodController().GetFoodsPerSection("Bebidas");

            if (foods != null) {
                var wrapPanel = new WrapPanel { Orientation = Orientation.Horizontal };
                
                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData();
                    foodControl.Margin = new Thickness(8);
                    wrPFood.Children.Add(foodControl);
                }
                grdContent.Children.Add(wrapPanel);
            } else {
                App.ShowMessageError("No se pudo recuperar el menú de bebidas", "Error");
            }
        }

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
