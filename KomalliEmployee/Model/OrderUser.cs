using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class OrderUser {
        public string OrderID { get; set; }
        public int Quantity { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string DishStatus { get; set; }
        public string FoodName { get; set; }
        public int DishQuantity { get; set; }
        public string ClientName { get; set; }
    }
}
