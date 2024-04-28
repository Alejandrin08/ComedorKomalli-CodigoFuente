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
    /// Lógica de interacción para FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl {

        public FoodModel Food { get; set; } 
        public FoodUserControl() {
            InitializeComponent();
        }

        public void BindData() {
            if (Food != null) {
                txtbFoodName.Text = Food.Name;
                txtbFoodPrice.Text = Food.Price.ToString();
                imgFoodImage.Source = new BitmapImage(new Uri(Food.Image));
            }
        }
    }
}
