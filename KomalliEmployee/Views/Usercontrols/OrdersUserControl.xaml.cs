using KomalliEmployee.Controller;
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

namespace KomalliEmployee.Views.Usercontrols
{
    /// <summary>
    /// Interaction logic for OrdersUserControl.xaml
    /// </summary>
    public partial class OrdersUserControl : UserControl {
        private OrderUser _order;

        public OrdersUserControl() {
            InitializeComponent();
        }

        public void SetData(OrderUser order) {
            _order = order;
            lblContentQuantity.Content = order.Quantity;
            lblContentCategory.Content = order.OrderType;
            lblContentType.Content = order.FoodName;
           
            UpdateButtonContent();
        }

        private void ClickChangeStatus(object sender, RoutedEventArgs e) {
            if (_order == null) return;

            string newStatus = GetNextStatus(_order.Status);
            FoodOrderController controller = new FoodOrderController();

            if (controller.UpdateOrderStatus(_order.OrderID, newStatus)) {   
                _order.Status = newStatus;
                UpdateButtonContent();
            }
            else {
                MessageBox.Show("Error al actualizar el estado de la orden.");
            }
        }

        private void UpdateButtonContent() {
            btnStatus.Content = _order.Status;
        }

        private string GetNextStatus(string currentStatus) {
            switch (currentStatus) {
                case "Pendiente":
                    return "Hecho";
                case "Hecho":
                    return "Entregado";
                case "Entregado":
                    return "Entregado";
                default:
                    return "Pendiente";
            }
        }
    }
}
