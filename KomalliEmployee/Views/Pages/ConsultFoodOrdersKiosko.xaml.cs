using KomalliEmployee.Controller;
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

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para ConsultFoodOrdersKiosko.xaml
    /// </summary>
    public partial class ConsultFoodOrdersKiosko : Page {
        public ConsultFoodOrdersKiosko() {
            InitializeComponent();
            InitializeListOfFoodOrders();
        }

        private void InitializeListOfFoodOrders() {
            List<FoodOrderModel> foodOrders;
            FoodOrderController foodOrderControler = new FoodOrderController();
            foodOrders = foodOrderControler.ConsultFoodOrdersKiosko();
            dgFoodOrders.ItemsSource = foodOrders;
        }

        private void MouseDownOptions(object sender, MouseButtonEventArgs e) {
            var dataContext = (sender as FrameworkElement)?.DataContext;
            if (dataContext != null) {
                var rowObject = (FoodOrderModel)dataContext;
                var idFoodOrder = rowObject.IdFoodOrder;
                SingletonClass.Instance.IdFoodOrderSelected = idFoodOrder;
            }
            MakeCollection modifyUser = new MakeCollection();
            this.NavigationService.Navigate(modifyUser);
        }

        private void TextChangedSearchFoodOrder(object sender, TextChangedEventArgs e) {
            string searchTerm = txbSearchFoodOrder.Text.ToLower();

            List<FoodOrderModel> foodOrders;
            FoodOrderController foodOrderControler = new FoodOrderController();
            foodOrders = foodOrderControler.ConsultFoodOrdersKiosko();

            if (string.IsNullOrWhiteSpace(searchTerm)) {
                dgFoodOrders.ItemsSource = foodOrders;
            } else {
                var filteredList = foodOrders.Where(item => item.IdFoodOrder.ToLower().Contains(searchTerm)).ToList();
                dgFoodOrders.ItemsSource = filteredList;
            }
        }
    }
}
