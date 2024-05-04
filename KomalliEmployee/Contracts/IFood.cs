using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IFood {

        public List<FoodModel> GetFoodsPerSection(string sectionName);
        public List<FoodModel> GetKeyMenu();
        public int RegistryOrderMenuCard(FoodModel foodModel, string idOrder);
        public int RegistryOrderMenu(FoodModel foodModel, string idOrder);



    }
}
