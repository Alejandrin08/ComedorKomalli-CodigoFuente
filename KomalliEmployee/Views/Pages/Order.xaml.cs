using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Views.Usercontrols;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
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

namespace KomalliEmployee.Views.Pages
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        private OrderUser _order;
        public Order()
        {
            InitializeComponent();
            ShowOrder();
        }

        private void ShowOrder()
        {
            ShowOrdersPerStatus("Pagado", lstOrdersPending);
            ShowOrdersPerStatus("Hecho", lstOrdersDone);
            ShowOrdersPerStatus("Entregado", lstOrdersDelivered);
        }

        private void ShowOrdersPerStatus(string status, ListView listView)
        {
            FoodOrderController foodOrder = new FoodOrderController();
            List<OrderUser> orders = foodOrder.GetCombinedOrdersByStatus(status);
            listView.Items.Clear();

            if (orders != null)
            {
                foreach (OrderUser order in orders)
                {
                    var orderUserControl = new OrdersUserControl { order = order };
                    orderUserControl.SetData(order);
                    listView.Items.Add(orderUserControl);
                }

            }
            else
            {
                App.ShowMessageError("No se pudo recuperar la orden de estado " + status, "Error");
            }
        }

    }
}

