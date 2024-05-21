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
    /// Lógica de interacción para Others.xaml
    /// </summary>
    public partial class Others : Page {
        public Others() {
            InitializeComponent();
            ShowOthers();
        }

        private void ShowOthers() {
            var foods = new FoodController().GetFoodsPerSection("Otros");

            if (foods != null) {
                var wrapPanel = new WrapPanel { Orientation = Orientation.Horizontal };

                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData();
                    foodControl.Margin = new Thickness(8);
                    wrpFood.Children.Add(foodControl);
                }
                grdContent.Children.Add(wrapPanel);
            } else {
                App.ShowMessageError("No se pudo recuperar los paquetes de comida", "Error");
            }
        }

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
