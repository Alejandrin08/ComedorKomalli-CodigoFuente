using KomalliClient.Model;
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

namespace KomalliClient.Views.UserControls {
    /// <summary>
    /// Lógica de interacción para FoodDetails.xaml
    /// </summary>
    public partial class FoodDetails : UserControl {
        public FoodDetails() {
            InitializeComponent();
        }

        public void BindData(FoodModel food) {
            if (food != null) {
                txtbFoodName.Text = food.Name;
                txtbFoodPrice.Text = "$" + food.Price.ToString();
            }
        }
    }
}
