﻿using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KomalliEmployee.Views.Usercontrols
{
    /// <summary>
    /// Interaction logic for OrdersUserControl.xaml
    /// </summary>
    public partial class OrdersUserControl : UserControl {
       
        
        private OrderUser _order;
        public OrderUser order { get; set; }

        public OrdersUserControl()
        {
            InitializeComponent();
            ShowDish();
        }

        public void SetData(OrderUser user)
        {
            _order = user;
            lblIDDishContent.Content = user.OrderID;
            lblNameContent.Content = user.ClientName;
            ShowDish();
        }

        public void AddOrders(OrderUser order) {
            DishUserControl dishUser = new DishUserControl();
            dishUser.SetData(order);
            stkDish.Children.Add(dishUser);
        }


        public void ShowDish() {
            if (_order == null || _order.OrderID == null) {
                return;
            }
            FoodOrderController foodOrder = new FoodOrderController();
            List<OrderUser> orders = foodOrder.GetCombinedDishesByStatus("Pagado", _order.OrderID);
            stkDish.Children.Clear();

            if (orders.Any()) {
                foreach (OrderUser order in orders) {
                    AddOrders(order);
                }
            }

            List<OrderUser> doneOrders = foodOrder.GetCombinedDishesByStatus("Hecho", _order.OrderID);
            if (doneOrders.Any())
            {
                foreach (OrderUser order in doneOrders)
                {
                    AddOrders(order);
                }
            }

            List<OrderUser> deliveredOrders = foodOrder.GetCombinedDishesByStatus("Entregado", _order.OrderID);
            if (deliveredOrders.Any())
            {
                foreach (OrderUser order in deliveredOrders)
                {
                    AddOrders(order);
                }
            }
        }

    }
}
