using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class DishOrderModel {
        public string DishName { get; set; }
        public int NumberDishes { get; set; }
        public int UnitPrice { get; set; }
        public int Total {  get; set; }
    }
}
