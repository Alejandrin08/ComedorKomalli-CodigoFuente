﻿using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IFoodOrder {

        public List<FoodOrderModel> DailyFoodOrders();
        public int CalculateTotalDailyEntries();
        public int CalculateTotalDailyChange();
        public List<FoodOrderModel> ConsultFoodOrdersKiosko();
        public FoodOrderModel GetTotalAndNameFromOrder(string idFoodOrder);
        public int UpdateFoodOrder(FoodOrderModel foodOrderModel, string idFoodOrder);
        public string MakeIdFoodOrder();
        public int RegistryOrder(FoodOrderModel foodModel);
        public int DeleteOrder(string idFoodOrder);



    }
}
