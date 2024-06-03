using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KomalliEmployee.Controller {
    public class FoodOrderController : IFoodOrder
    {

        /**
         * <summary>
         * Este método se encarga de calcular el total de cambio
         * </summary>
         * <returns>Regresa el total de cambio</returns>
         */
        public int CalculateTotalDailyChange()
        {
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var today = DateTime.Today;
                    result = context.FoodOrder
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == "Pagado")
                        .Sum(foodOrder => (int?)foodOrder.Change) ?? 0;
                }
            }
            catch (EntityException ex)
            {
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
        public int CalculateTotalDailyEntries()
        {
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var today = DateTime.Today;
                    result = context.FoodOrder
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == "Pagado")
                        .Sum(foodOrder => (int?)foodOrder.Total) ?? 0;
                }
            }
            catch (EntityException ex)
            {
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
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == "Pagado")
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
        public List<FoodOrderModel> ConsultFoodOrdersKiosko()
        {
            List<FoodOrderModel> foodOrder = new List<FoodOrderModel>();
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Today;
                    var query = (from foodOrders in context.FoodOrder
                                 where DbFunctions.TruncateTime(foodOrders.Date) == today
                                       && foodOrders.Status == "Pendiente"
                                       && foodOrders.IDFoodOrder.StartsWith("Kio")
                                 select new {
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

        /**
         * <summary>
         * Este método se encarga de obtener el nombre de cliente y total de un pedido.
         * </summary>
         * <param name="idFoodOrder">Identificador del pedido. </param>
         * <returns>Regresa el total y el nombre de cliente si es que lo encuentra .</returns>
         */
        public FoodOrderModel GetTotalAndNameFromOrder(string idFoodOrder) {
            FoodOrderModel foodOrderModel = null;
            try {
                using (var context = new KomalliEntities()) {
                    var order = (from foodOrder in context.FoodOrder                                     
                                     where foodOrder.IDFoodOrder == idFoodOrder
                                 select new {
                                     foodOrder.ClientName,
                                     foodOrder.Total
                                     }).FirstOrDefault();
                    foodOrderModel = new FoodOrderModel();
                    if (order != null) {
                        foodOrderModel.ClientName = order.ClientName;
                        foodOrderModel.Total = order.Total;
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en obtener el nombre del cliente y total de un pedido", ex);
            }
            return foodOrderModel;
        }

        /**
         * <summary>
         * Este método se encarga de modificar los datos de un pedido de la tabla FoodOrder de la base de datos.
         * </summary>
         * <param name="idFoodOrder">Identificador del pedido. </param>
         * <param name="foodOrderModel">Objeto de foodOrder que contiene los detalles a actualizar del pedido</param>
         * <returns>Regresa 1 si se actualiza correctamente, 0 si ocurre un error..</returns>
         */

        public int UpdateFoodOrder(FoodOrderModel foodOrderModel, string idFoodOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var foodOrder = context.FoodOrder.Where(foodOrder => foodOrder.IDFoodOrder == idFoodOrder).FirstOrDefault();

                    if (foodOrder != null) {
                        foodOrder.Status = foodOrderModel.Status;
                        foodOrder.Change = foodOrderModel.Change;
                        foodOrder.ClientName = foodOrderModel.ClientName;
                    }
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en actualizar los datos de un pedido", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de generar id aleatorios para pedidos de Caja.
         * </summary>
         * <returns>Regresa un string que combina la clave y el numero aleatorio generado</returns>
         */

        private string GenerateRamdomIdFoodOrder() {
            Random rand = new Random();
            string id = "Caj";

            for (int i = 0; i < 4; i++) {
                id += rand.Next(0, 10); 
            }
            return id;
            
        }

        /**
         * <summary>
         * Este método se encarga de verificar que los id aleatorios, no existan en la base de datos.
         * </summary>
         * <returns>Regresa un string que contiene el id unico</returns>
         */

        public string MakeIdFoodOrder() {
            string idGenerated = GenerateRamdomIdFoodOrder();
            try {  
                using (var context = new KomalliEntities()) {
                    var idOrder = context.FoodOrder.Where(food => food.IDFoodOrder == idGenerated).FirstOrDefault();

                    while (idOrder != null) {
                        idGenerated = GenerateRamdomIdFoodOrder();
                        idOrder = context.FoodOrder.Where(food => food.IDFoodOrder == idGenerated).FirstOrDefault();
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al validar un id de pedido de caja", ex);
            }

            return idGenerated;
        }

        /**
         * <summary>
         * Este método se encarga de registrar un pedido de la tabla FoodOrder de la base de datos.
         * </summary>
         * <param name="foodModel">Objeto de foodOrder que contiene los detalles del pedido</param>
         * <returns>Regresa 1 si se registro correctamente, 0 si ocurre un error.</returns>
         */

        public int RegistryOrder(FoodOrderModel foodModel) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var order = new FoodOrder {
                        IDFoodOrder = foodModel.IdFoodOrder,
                        Status = "Pendiente",
                        Date = DateTime.Now,
                        Total = foodModel.Total,
                        NumberDishes = foodModel.NumberDishes
                    };
                    context.FoodOrder.Add(order);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar orden desde caja", ex);
            }
            return result;
        }

        public List<OrderUser> GetStatusOrderSetMenu(string status) {
            List<OrderUser> orders = new List<OrderUser>();
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Today;
                    var currentTime = DateTime.Now;

                    var setMenuOrders = (from foodOrder in context.FoodOrder
                                         join foodOrderSetMenu in context.FoodOrder_SetMenu
                                         on foodOrder.IDFoodOrder equals foodOrderSetMenu.IDFoodOrderFoodOrder
                                         join setMenu in context.SetMenu
                                         on foodOrderSetMenu.KeySetMenuSetMenu equals setMenu.KeySetMenu
                                         where DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == status
                                         select new OrderUser {
                                             OrderID = foodOrder.IDFoodOrder,
                                             DishQuantity = foodOrder.NumberDishes,
                                             FoodName = foodOrderSetMenu.KeySetMenuSetMenu,
                                             Quantity = foodOrderSetMenu.Quantity,
                                             Status = foodOrder.Status,
                                             DishStatus = foodOrderSetMenu.Status,
                                             ClientName = foodOrder.ClientName
                                         }).ToList();

                    orders = setMenuOrders;
                }
            }
            catch (EntityException ex) {
                orders = null;
                LoggerManager.Instance.LogError("Error al consultar la orden", ex);
            }
            return orders;
        }

        public List<OrderUser> GetStatusOrderMenuCard(string status) {
            List<OrderUser> orders = new List<OrderUser>();
            try {
                using (var context = new KomalliEntities()) {
                    var today = DateTime.Today;
                    var currentTime = DateTime.Now;
                    var menuCardOrders = (from foodOrder in context.FoodOrder
                                          join unionTable in context.FoodOrder_MenuCard
                                          on foodOrder.IDFoodOrder equals unionTable.IDFoodOrderFoodOrder
                                          join menuCard in context.MenuCard
                                          on unionTable.KeyCardMenuCard equals menuCard.KeyCard
                                          where DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == status
                                          select new OrderUser {
                                              OrderID = foodOrder.IDFoodOrder,
                                              DishQuantity = foodOrder.NumberDishes,
                                              FoodName = menuCard.NameFood,
                                              Quantity = unionTable.Quantity,
                                              Status = foodOrder.Status,
                                              DishStatus = unionTable.Status,
                                              ClientName = foodOrder.ClientName
                                          }).ToList();

                    orders.AddRange(menuCardOrders);
                }
            }
            catch (EntityException ex) {
                orders = null;
                LoggerManager.Instance.LogError("Error al consultar la orden", ex);
            }
            return orders;
        }

        public List<OrderUser> GetCombinedOrdersByStatus(string status) {
            List<OrderUser> orders = new List<OrderUser>();

            var setMenuOrders = GetStatusOrderSetMenu(status);
            var menuCardOrders = GetStatusOrderMenuCard(status);

            orders.AddRange(setMenuOrders);
            orders.AddRange(menuCardOrders);

            return orders;
        }

        /**
        * <summary>
        * Este método se encarga de eliminar un pedido de la tabla FoodOrder de la base de datos.
        * </summary>
        * <param name="orderId">Id que identifica el pedido a actualizar su estado</param>
        * <param name="newStatus">parametro para actualizar el estado</param>
        * <returns>Regresa true si se actualizo correctamente, false si ocurre un error.</returns>
        */
        public bool UpdateOrderStatus(string orderId, string newStatus) {
            try {
                using (var context = new KomalliEntities()) {
                    var order = context.FoodOrder.SingleOrDefault(o => o.IDFoodOrder == orderId);
                    if (order != null) {
                        order.Status = newStatus;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex) {
                LoggerManager.Instance.LogError("Error al actualizar el estado de la orden", ex);
                return false;
            }
        }

        /**
         * <summary>
         * Este método se encarga de eliminar un pedido de la tabla FoodOrder de la base de datos.
         * </summary>
         * <param name="idFoodOrder">Id que identifica el pedido a eliminar</param>
         * <returns>Regresa 1 si se elimino correctamente, 0 si ocurre un error.</returns>
         */

        public int DeleteOrder(string idFoodOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var orderToDelete = context.FoodOrder.FirstOrDefault(o => o.IDFoodOrder == idFoodOrder);
                    if (orderToDelete != null) {
                        context.FoodOrder.Remove(orderToDelete);
                        
                    }
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en eliminar orden", ex);
            }
            return result;
        }
    }
}
