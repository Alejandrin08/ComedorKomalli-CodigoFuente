using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DishUserControl.xaml
    /// </summary>
    public partial class DishUserControl : UserControl {
        private OrderUser _order; 

        public DishUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetData(OrderUser order)
        {
            _order = order;
            lblDupe.Content = order.FoodName;
            lblQuantity.Content = order.Quantity;
            UpdateButtonContent();
        }

        private void ClickChangeStatus(object sender, RoutedEventArgs e)
        {
            UpdateStateDish(_order.OrderID, _order.FoodName);
        }

        public void UpdateStateDish(string idDish, string nameDish)
        {
            if (_order == null) return;
            string newStatus = GetNextStatus(_order.DishStatus);
            DishOrderController dishOrder = new DishOrderController();
            bool success = dishOrder.UpdateDishStatus(idDish, nameDish, newStatus);
            
            if (success)
            {
                UpdateButtonContent();
                CheckAndUpdateOrderStatus(idDish);
                MessageBox.Show("Estado del platillo actualizado con éxito.");
            }
            else
            {
                MessageBox.Show("Error al actualizar el estado del platillo.");
            }
        }

        private string GetNextStatus(string currentStatus) {
            switch (currentStatus) {
                case "Pendiente":
                    return  "Hecho";
                    
                case "Hecho":
                    return  "Entregado";
                    
                default:
                    return "Pendiente";
            }
        }

        private void UpdateButtonContent()
        {
            switch (_order.DishStatus)
            {
                case "Pendiente":
                    btnStatus.Content = "Hecho";
                    btnStatus.Visibility = Visibility.Visible;
                    break;
                case "Hecho":
                    btnStatus.Content = "Entregado";
                    btnStatus.Visibility = Visibility.Visible;
                    break;
                case "Entregado":
                    btnStatus.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CheckAndUpdateOrderStatus(string orderId)
        {
            FoodOrderController foodOrderController = new FoodOrderController();
            var combinedOrders = foodOrderController.GetCombinedOrders("Pagado")
                .Concat(foodOrderController.GetCombinedOrders("Hecho"))
                .Concat(foodOrderController.GetCombinedOrders("Entregado"))
                .Where(order => order.OrderID == orderId).ToList();

            if (combinedOrders.All(order => order.DishStatus == "Hecho"))
            {
                foodOrderController.UpdateOrderStatus(orderId, "Hecho");
                RefreshParentOrderPage();
            }
            else if (combinedOrders.All(order => order.DishStatus == "Entregado"))
            {
                foodOrderController.UpdateOrderStatus(orderId, "Entregado");
                RefreshParentOrderPage();
            }
        }

        private void RefreshParentOrderPage()
        {
            var parentWindow = Window.GetWindow(this);
            var parentOrderPage = FindVisualParent<Order>(parentWindow);
            parentOrderPage?.RefreshLists();
        }

        private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;

            return FindVisualParent<T>(parentObject);
        }
    }
    
}
