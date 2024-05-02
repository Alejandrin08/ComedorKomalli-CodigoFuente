using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Controller {
    public class DishOrderController : IDishOrder {

        /**
        * <summary>
        * Este método se encarga de obtener datos sobre los platillos de la carta que que se pidieron en un pedido
        * </summary>
        * * <param name="IdFoodOrder">Identificador del pedido. </param>
        * <returns> Regresa la lista de los detalles de los platillos del pedido.</returns>
        */
        public List<DishOrderModel> ConsultDishOrderFromCard(string IdFoodOrder) {
            
            List<DishOrderModel> dishesCard = new List<DishOrderModel>();
            dishesCard = null;
            try {               
                using (var context = new KomalliEntities()) {

                    var query = (from menuCard in context.MenuCard
                    join unionTable in context.FoodOrder_MenuCard
                    on menuCard.KeyCard equals unionTable.KeyCardMenuCard
                    where unionTable.IDFoodOrderFoodOrder == IdFoodOrder
                    select new {
                        menuCard.Price,
                        unionTable.Quantity,
                        unionTable.SellingPrice,
                        menuCard.NameFood
                    }
                ).ToList()
                .Select(result => new DishOrderModel {
                    DishName = result.NameFood,
                    UnitPrice = result.Price,
                    NumberDishes = result.Quantity,
                    Total = result.SellingPrice,

                }).ToList();
                    dishesCard = query;
                }
            } catch (EntityException ex) {
                dishesCard = null;
                LoggerManager.Instance.LogError("Error al consultar los platillos del pedido de la carta", ex);
            }
            return dishesCard;
        }

        /**
        * <summary>
        * Este método se encarga de obtener datos sobre los platillos o menu que que se pidieron en un pedido, ya sea de menu o de carta.
        * </summary>
        * * <param name="IdFoodOrder">Identificador del pedido. </param>
        * <returns> Regresa la lista de los detalles de los platillos del pedido.</returns>
        */

        public List<DishOrderModel> ConsultDishOrder(string IdFoodOrder) {
            List<DishOrderModel> dishes = new List<DishOrderModel>();
            List<DishOrderModel> dishesCard = new List<DishOrderModel>();
            List<DishOrderModel> dishesMenu = new List<DishOrderModel>();
            dishesCard = ConsultDishOrderFromCard(IdFoodOrder);
            dishesMenu = ConsultDishOrderFromMenu(IdFoodOrder);
            dishes.AddRange( dishesCard );
            dishes.AddRange(dishesMenu );
            return dishes;
        }


        /**
        * <summary>
        * Este método se encarga de obtener datos sobre los menu del dia que que se pidieron en un pedido
        * </summary>
        * * <param name="IdFoodOrder">Identificador del pedido. </param>
        * <returns> Regresa la lista de los detalles de los platillos del pedido.</returns>
        */
        public List<DishOrderModel> ConsultDishOrderFromMenu(string IdFoodOrder) {
            List<DishOrderModel> dishesMenu = new List<DishOrderModel>();
            dishesMenu = null;
           
            try {
                using (var context = new KomalliEntities()) {

                    var query = (from menu in context.SetMenu
                                 join unionTable in context.FoodOrder_SetMenu
                                 on menu.KeySetMenu equals unionTable.KeySetMenuSetMenu
                                 where unionTable.IDFoodOrderFoodOrder == IdFoodOrder
                                 select new {
                                     unionTable.UnitPrice,
                                     unionTable.Quantity,
                                     unionTable.SellingPrice,
                                     unionTable.KeySetMenuSetMenu
                                 }

                ).ToList()
                .Select(result => new DishOrderModel {
                    DishName = result.KeySetMenuSetMenu.StartsWith("Com") ? "Comida" : "Desayuno",
                    UnitPrice = (int)result.UnitPrice,
                    NumberDishes = result.Quantity,
                    Total = result.SellingPrice,

                }).ToList();
                    dishesMenu = query;
                }

            } catch (EntityException ex) {
                dishesMenu = null;
                LoggerManager.Instance.LogError("Error al consultar los menu del dia del pedido", ex);
            }
            return dishesMenu;
        }

    }
}
