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

        /**
         * <summary>
         * Este método se encarga de obtener la clave de los menus que habra disponibles por dia.
         * </summary>
         * <returns>Regresa una lista de foodModel que contiene la clave de los menu, o nulos si no encuentra</returns>
         */

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
                LoggerManager.Instance.LogError("Error al obtener los las claves de los menu del dia", ex);
            }
            return foods;
        }

        /**
         * <summary>
         * Este método se encarga de registrar los detalles de un pedido que corresponden a los platillos que hay a la carta.
         * </summary>
         * <param name="idOrder">Identificador del pedido. </param>
         * <param name="foodModel">Objeto de foodOrder que contiene los detalles a registrar del pedido</param>
         * <returns>Regresa 1 si se registra correctamente, 0 si ocurre un error.</returns>
         */

        public int RegistryOrderMenuCard(FoodModel foodModel, string idOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    
                    var order = new FoodOrder_MenuCard {
                        IDFoodOrderFoodOrder = idOrder,
                        KeyCardMenuCard = foodModel.KeyCard,
                        Quantity = foodModel.Quantity,
                        SellingPrice = foodModel.Subtotal,
                        Status = "Pendiente"
                    };
                    context.FoodOrder_MenuCard.Add(order);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar pedido de menu carta", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de registrar los detalles de un pedido que corresponden a los menu del dia.
         * </summary>
         * <param name="idOrder">Identificador del pedido. </param>
         * <param name="foodModel">Objeto de foodOrder que contiene los detalles a registrar del pedido</param>
         * <returns>Regresa 1 si se registra correctamente, 0 si ocurre un error.</returns>
         */

        public int RegistryOrderMenu(FoodModel foodModel, string idOrder) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    
                    var order = new FoodOrder_SetMenu {
                        IDFoodOrderFoodOrder = idOrder,
                        KeySetMenuSetMenu = foodModel.KeyCard,
                        Quantity = foodModel.Quantity,
                        SellingPrice = foodModel.Subtotal,
                        UnitPrice = foodModel.Price,
                        Status = "Pendiente"
                    };
                    context.FoodOrder_SetMenu.Add(order);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en Registrar pedido de menu", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de actualizar el stock de un producto de la carta.
         * </summary>
         * <param name="keyCard">Identificador del platillo. </param>
         * <param name="quantity">Cantidad de platillos a disminuir</param>
         * <returns>Regresa 1 si se actualiza correctamente, 0 si ocurre un error.</returns>
         */

        public int UpdateStockMenuCard (string keyCard, int quantity) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var menuCard = context.MenuCard.SingleOrDefault(mc => mc.KeyCard == keyCard);
                    if (menuCard != null && menuCard.Stock > 0) {                       
                        menuCard.Stock -= quantity;
                        result = context.SaveChanges();
                    }
                    
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar stock en menu card", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de obtener el stock de un producto de la carta.
         * </summary>
         * <param name="keyCard">Identificador del platillo. </param>
         * <returns>Regresa el stock del platillo.</returns>
         */

        public int GetStockMenuCard (string keyCard) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var stock = context.MenuCard
                       .Where(mc => mc.KeyCard == keyCard)
                       .Select(mc => mc.Stock)
                       .SingleOrDefault();
                    if (stock != null) {
                        result = stock ?? 0;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar stock en menu card", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de actualizar el stock de un menu del dia.
         * </summary>
         * <param name="keySetMenu">Identificador del menu. </param>
         * <param name="quantity">Cantidad de menu a disminuir</param>
         * <returns>Regresa 1 si se actualiza correctamente, 0 si ocurre un error.</returns>
         */

        public int UpdateStockSetMenu(string keySetMenu, int quantity) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var menuCard = context.SetMenu.SingleOrDefault(mc => mc.KeySetMenu == keySetMenu);
                    if (menuCard != null && menuCard.Stock > 0) {
                        menuCard.Stock -= quantity;
                        result = context.SaveChanges();
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar stock en menu card", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de obtener el stock de un menu del dia.
         * </summary>
         * <param name="keySetMenu">Identificador del menu. </param>
         * <returns>Regresa el stock del menu.</returns>
         */

        public int GetStockSetMenu(string keySetMenu) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var stock = context.SetMenu
                       .Where(mc => mc.KeySetMenu == keySetMenu)
                       .Select(mc => mc.Stock)
                       .SingleOrDefault();
                    if (stock != null) {
                        result = stock ?? 0;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar stock en menu card", ex);
            }
            return result;
        }


    }
}
