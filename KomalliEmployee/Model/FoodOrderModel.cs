using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class FoodOrderModel {

        public string IdFoodOrder { get; set; }
        public string ClientName {  get; set; }
        public DateTime? Date { get; set; }
        public int Total {  get; set; }
        public int NumberDishes { get; set; }

        public string DateFormat {
            get {
                return Date.Value.ToShortDateString();
            }
        }
    }
}
