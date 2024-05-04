﻿using KomalliEmployee.Contracts;
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
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == "Pagado")
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
                        .Where(foodOrder => DbFunctions.TruncateTime(foodOrder.Date) == today && foodOrder.Status == "Pagado")
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
        public List<FoodOrderModel> ConsultFoodOrdersKiosko() {
            List<FoodOrderModel> foodOrder = new List<FoodOrderModel>();
            foodOrder = null;
            //DbFunctions.TruncateTime(foodOrders.Date) == today && foodOrders.Status == "Pendiente"
            //agregar cuando ya se tengan varios registros, mientras no se pone para poder probar y que siemore muestre algo
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
                LoggerManager.Instance.LogError("Error en actualizar un pedido", ex);
            }
            return result;
        }

        public string GenerateRamdomIdFoodOrder() {
            Random rand = new Random();
            string id = "Caj";

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


        public int RegistryOrder(FoodOrderModel foodModel) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var order = new FoodOrder {
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
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar orden desde caja", ex);
            }
            return result;
        }
    }
}
