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



        private void ShowOrders(List<OrderUser> orders, string orderType) {
            lstOrders.Items.Clear();
            if (orderType == "Menú del día") {
                if (orders.Any()) {
                    foreach (OrderUser order in orders) {
                        AddOrders(order);
                    }
                }
                
            }
            else if (orderType == "Menú a la carta") {
                if (orders.Any()) {
                    foreach (OrderUser order in orders) {
                        AddOrders(order);
                    }
                }
            }
        }
        
        private void ShowAllOrders() {
            FoodOrderController foodOrderController = new FoodOrderController();
            var today = DateTime.Today;
            
            var setMenuOrders = foodOrderController.GetStatusOrderSetMenu("Pagado");
            ShowOrders(setMenuOrders, "Menú del día");
            var menuCardOrders = foodOrderController.GetStatusOrderMenuCard("Pagado");
            ShowOrders(menuCardOrders, "Menú a la carta");
        }


        private void ClickAllOrders(object sender, RoutedEventArgs e) {
            ShowAllOrders();
        }

        private void ClickPendingOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            
        
            var setMenuOrders = foodOrderController.GetStatusOrderSetMenu("Pendiente");
            ShowOrders(setMenuOrders, "Menú del día");
            var menuCardOrders = foodOrderController.GetStatusOrderMenuCard("Pendiente");
            ShowOrders(menuCardOrders, "Menú a la carta");
        }

        private void ClickPlacedOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            var setMenuOrders = foodOrderController.GetStatusOrderSetMenu("Hecho");
            ShowOrders(setMenuOrders, "Menú del día");
            var menuCardOrders = foodOrderController.GetStatusOrderMenuCard("Hecho");
            ShowOrders(menuCardOrders, "Menú a la carta");
        }

        private void ClickDelivereddOrders(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            var setMenuOrders = foodOrderController.GetStatusOrderSetMenu("Entregado");
            ShowOrders(setMenuOrders, "Menú del día");
            var menuCardOrders = foodOrderController.GetStatusOrderMenuCard("Entregado");
            ShowOrders(menuCardOrders, "Menú a la carta");
        }
    }
}
