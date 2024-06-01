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
    /// Interaction logic for OrderDataUserControl.xaml
    /// </summary>
    public partial class OrderDataUserControl : UserControl {
        public OrderDataUserControl() {
            InitializeComponent();
        }

        public FoodModel Food { get; set; }

        public void SetData(FoodModel food) {
            Food = food;
            lblContentCategory.Content =  food.Name;
            lblContentPrice.Content =  food.Price.ToString();
            lblContentQuantity.Content =  food.Quantity.ToString();
            lblContentType.Content =  food.Section;
        }

        public void UpdateQuantity(int quantity) {
            lblContentQuantity.Content = quantity.ToString();
        }

        public void UpdatePrice(int price) {
            lblContentPrice.Content = "$" + price.ToString();
        }
    }
}
