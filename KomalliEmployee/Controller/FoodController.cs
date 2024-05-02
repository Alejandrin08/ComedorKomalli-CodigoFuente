using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Controller {
    public class FoodController : IFood {

        /**
         * <summary>
         * Este método obtiene los alimentos por sección.
         * </summary>
         * <param name="sectionName">Nombre de la sección.</param>
         * <returns>Lista de alimentos.</returns>
         */
        public List<FoodModel> GetFoodsPerSection(string sectionName) {
            List<FoodModel> foods = new List<FoodModel>();
            try {
                using (var context = new KomalliEntities()) {
                    var query = foods = context.MenuCard
                        .Where(food => food.Section == sectionName)
                        .Select(food => new FoodModel {
                            KeyCard = food.KeyCard,
                            Name = food.NameFood,
                            Price = food.Price,
                            Section = food.Section
                        }).ToList();
                }
            } catch (EntityException ex) {
                foods = null;
                LoggerManager.Instance.LogError("Error al obtener los alimentos por sección: ", ex);
            }
            return foods;
        }

        public List<FoodModel> GetKeyMenu() {
            List<FoodModel> foods = new List<FoodModel>();
            try {
                using (var context = new KomalliEntities()) {
                    var query = foods = context.SetMenu
                        .Where(food => (DbFunctions.TruncateTime(food.Date) == DateTime.Today))
                        .Select(food => new FoodModel {
                            KeyCard = food.KeySetMenu
                        }).ToList();
                }
            } catch (EntityException ex) {
                foods = null;
                LoggerManager.Instance.LogError("Error al obtener los alimentos por sección: ", ex);
            }
            return foods;
        }

        public int RegistryOrderMenuCard(FoodModel foodModel, string idOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    
                    var order = new FoodOrder_MenuCard {
                        IDFoodOrderFoodOrder = idOrder,
                        KeyCardMenuCard = foodModel.KeyCard,
                        Quantity = foodModel.Quantity,
                        SellingPrice = foodModel.Subtotal
                    };
                    context.FoodOrder_MenuCard.Add(order);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar pedido de menu carta", ex);
            }
            return result;
        }

        public int RegistryOrderMenu(FoodModel foodModel, string idOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    
                    var order = new FoodOrder_SetMenu {
                        IDFoodOrderFoodOrder = idOrder,
                        KeySetMenuSetMenu = foodModel.KeyCard,
                        Quantity = foodModel.Quantity,
                        SellingPrice = foodModel.Subtotal,
                        UnitPrice = foodModel.Price
                    };
                    context.FoodOrder_SetMenu.Add(order);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar pedido de menu", ex);
            }
            return result;
        }


    }
}
