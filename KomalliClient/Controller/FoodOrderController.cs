using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Controller {
    public class FoodOrderController {
        public int RegistryOrder(FoodOrderModel foodModel) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var order = new FoodOrder
                    {
                        IDFoodOrder = foodModel.IdFoodOrder,
                        Status = "Pendiente",
                        ClientName = "",
                        Date = DateTime.Now,
                        Total = foodModel.Total,
                        NumberDishes = foodModel.NumberDishes
                    };
                    context.FoodOrder.Add(order);
                    result = context.SaveChanges();
                }
            }
            catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar orden desde Kiosko", ex);
            }
            return result;
        }

        public string GenerateRamdomIdFoodOrder() {
            Random rand = new Random();
            string id = "Kio";

            for (int i = 0; i < 4; i++) {
                id += rand.Next(0, 10);
            }
            return id;

        }

        public string MakeIdFoodOrder() {
            string idGenerated = GenerateRamdomIdFoodOrder();

            using (var context = new KomalliEntities()) {
                var idOrder = context.FoodOrder.Where(food => food.IDFoodOrder == idGenerated).FirstOrDefault();

                while (idOrder != null) {
                    idGenerated = GenerateRamdomIdFoodOrder();
                    idOrder = context.FoodOrder.Where(food => food.IDFoodOrder == idGenerated).FirstOrDefault();
                }
            }

            return idGenerated;
        }
    }
}
