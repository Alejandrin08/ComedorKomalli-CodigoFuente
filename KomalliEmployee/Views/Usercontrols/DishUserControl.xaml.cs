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
    public partial class DishUserControl : UserControl, INotifyPropertyChanged
    {
        private OrderUser _order;
        private string _dishState;
        private string _buttonContent;
        private Visibility _buttonVisibility;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;  

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
            UpdateButtonContent(order.DishStatus);
        }

        private void ClickChangeStatus(object sender, RoutedEventArgs e)
        {
            if (_order == null) return;
            DishOrderController dishOrder = new DishOrderController();
            string newStatus = GetNextStatus(_order.DishStatus);

            if(dishOrder.UpdateStateDishMenuCard(_order.OrderID, newStatus)) {
                _order.DishStatus = newStatus;
                UpdateButtonContent(_order.DishStatus);
            }
            else
            {
                MessageBox.Show("Error al actualizar el estado de la orden.");
            }
            
        }

        private string GetNextStatus(string currentStatus)
        {
            switch (currentStatus)
            {
                case "Pendiente":
                    return  "Hecho";
                    
                case "Hecho":
                    return  "Entregado";
                    
                default:
                    return "Pendiente";
            }
        }

        private void UpdateButtonContent(string currentStatus)
        {
            switch (currentStatus)
            {
                case "Pendiente":
                    ButtonContent = "Hecho";
                    ButtonVisibility = Visibility.Visible;
                    break;
                case "Hecho":
                    ButtonContent = "Entregado";
                    ButtonVisibility = Visibility.Visible;
                    break;
                case "Entregado":
                    ButtonVisibility = Visibility.Collapsed;
                    break;
            }
        }


        public string ButtonContent
        {
            get => _buttonContent;
            set
            {
                if (_buttonContent != value)
                {
                    _buttonContent = value;
                    OnPropertyChanged(nameof(ButtonContent));
                }
            }
        }

        public Visibility ButtonVisibility
        {
            get => _buttonVisibility;
            set
            {
                if (_buttonVisibility != value)
                {
                    _buttonVisibility = value;
                    OnPropertyChanged(nameof(ButtonVisibility));
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
