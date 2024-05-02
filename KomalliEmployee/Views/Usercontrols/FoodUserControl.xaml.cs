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

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl {
        public FoodUserControl() {
            InitializeComponent();
        }

        public FoodModel Food { get; set; }
       

        public void BindData() {
            if (Food != null) {
                tbkFoodName.Text = Food.Name;
                tbkFoodPrice.Text = "$" + Food.Price.ToString();
                
            }
        }

        private void ClickAddFood(object sender, RoutedEventArgs e) {
            
            int price = int.Parse(tbkFoodPrice.Text.TrimStart('$'));
            FoodModel foodModel = new FoodModel() {
                Name = tbkFoodName.Text,
                Price = price,
                IsSelected = true
            };
            SingletonClass.Instance.SelectedFoods.Add(foodModel);
            
        }
    }
}
