using KomalliClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Contracts {
    public interface IFood {

        public List<FoodModel> GetFoodsPerSection(string sectionName);
        public int RegistryOrderMenu(FoodModel foodModel, string idOrder);
        public int RegistryOrderMenuCard(FoodModel foodModel, string idOrder);
        public List<FoodModel> GetKeyMenu();
        public int UpdateStockMenuCard(string keyCard, int quantity);
        public int GetStockMenuCard(string keyCard);
        public int UpdateStockSetMenu(string keySetMenu, int quantity);
        public int GetStockSetMenu(string keySetMenu);
    }
}
