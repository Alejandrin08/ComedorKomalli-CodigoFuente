using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IDishOrder {

        public List<DishOrderModel> ConsultDishOrder(String IdFoodOrder);

    }
}
