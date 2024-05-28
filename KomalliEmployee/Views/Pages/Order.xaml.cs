using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Views.Usercontrols;
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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page {
        public Order() {
            InitializeComponent();
            ShowAllOrders();
        }

        private void AddOrders(OrderUser order) {
            OrdersUserControl orderUser = new OrdersUserControl();
            orderUser.SetData(order);
            lstOrders.Items.Add(orderUser);

        }



        private void ShowOrders(List<OrderUser> orders) {
            lstOrders.Items.Clear();
            if (orders.Any()) {
                foreach (OrderUser order in orders) {
                    AddOrders(order);
                    Console.WriteLine(order.ToString());
                }
            }
        }
        
        private void ShowAllOrders() {
            FoodOrderController foodOrderController = new FoodOrderController();
            var statuses = new List<string> { "Pagado","Pendiente", "Hecho", "Entregado" };
            ShowOrders(foodOrderController.GetOrdersByStatuses(statuses));
        }


        private void ClickAllOrders(object sender, RoutedEventArgs e) {
            ShowAllOrders();
        }

        private void ClickPendingOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            ShowOrders(foodOrderController.GetStatusOrder("Pendiente"));
        }

        private void ClickPlacedOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            ShowOrders(foodOrderController.GetStatusOrder("Hecho"));
        }

        private void ClickDelivereddOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            ShowOrders(foodOrderController.GetStatusOrder("Entregado"));
        }
    }
}
