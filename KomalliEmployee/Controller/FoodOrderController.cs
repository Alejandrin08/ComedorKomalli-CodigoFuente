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
    public class FoodOrderController : IFoodOrder{

        /**
         * <summary>
         * Este método se encarga de calcular el total de cambio
         * </summary>
         * <returns>Regresa el total de cambio</returns>
         */
        public int CalculateTotalDailyChange() {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Today;
                    result = context.FoodOrder
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today)
                        .Sum(foodOrder => (int?)foodOrder.Change) ?? 0;
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al calcular el total de entradas. Error en CalculateTotalEntries", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de calcular el total de entradas ordenes de comida
         * </summary>
         * <returns>Regresa el total de entradas ordenes de comida</returns>
         */
        public int CalculateTotalDailyEntries() {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Today;
                    result = context.FoodOrder
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today)
                        .Sum(foodOrder => (int?)foodOrder.Total) ?? 0;
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al calcular el total de entradas. Error en CalculateTotalEntries", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de obtener todas las ordenes de comida
         * </summary>
         * <returns>Regresa una lista de las ordenes del día actual</returns>
         */

        public List<FoodOrderModel> DailyFoodOrders() {
            List<FoodOrderModel> foodOrderModels = null;
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Now.Date;
                    foodOrderModels = context.FoodOrder
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today)
                        .Select(foodOrder => new FoodOrderModel {
                            IdFoodOrder = foodOrder.IDFoodOrder,
                            Date = foodOrder.Date,
                            ClientName = foodOrder.ClientName,
                            NumberDishes = foodOrder.NumberDishes,
                            Total = foodOrder.Total
                        })
                        .ToList();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al listar las ordenes. Error en FoodOrders", ex);
            }
            return foodOrderModels;
        }

        /**
        * <summary>
        * Este método se encarga de obtener datos de pedidos realizados desde kiosko.
        * </summary>
        * <returns> Regresa la lista de los pedidos obtenidos en la base de datos.</returns>
        */
        public List<FoodOrderModel> ConsultFoodOrdersKiosko() {
            List<FoodOrderModel> foodOrder = new List<FoodOrderModel>();
            foodOrder = null;
            //DbFunctions.TruncateTime(foodOrders.Date) == today &&
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Now.Date;
                    var query = (from foodOrders in context.FoodOrder
                                 where  foodOrders.IDFoodOrder.StartsWith("Kio")
                                 select new  {
                                     foodOrders.IDFoodOrder,
                                     foodOrders.ClientName,
                                     foodOrders.NumberDishes,
                                     foodOrders.Total
                                 }).ToList()
                    .Select(result => new FoodOrderModel {
                        IdFoodOrder = result.IDFoodOrder,
                        ClientName = result.ClientName,
                        NumberDishes = result.NumberDishes,
                        Total = result.Total
                    }).ToList();

                    foodOrder = query;
                }
            } catch (EntityException ex) {
                foodOrder = null;
                LoggerManager.Instance.LogError("Error al consultar los pedidos de kiosko", ex);
            }
            return foodOrder;
        }
    }
}
