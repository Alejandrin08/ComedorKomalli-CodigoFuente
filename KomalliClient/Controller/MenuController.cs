using KomalliClient.Contracts;
using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Controller {
    public class MenuController : IMenu {
        /** <summary>
          * Este método obtiene los menús registrados del día de los desayunos
          * </summary>
          * <returns>Objeto MenuModel</returns>
          */
        public MenuModel GetBreakfastOfTheDay() {
            MenuModel menu = null;
            const string KEY_BREAKFAST = "Des";
            try {
                using (var context = new KomalliEntities()) {
                    menu = context.SetMenu
                        .Where(menuOrder => menuOrder.KeySetMenu.StartsWith(KEY_BREAKFAST) &&
                                            DbFunctions.TruncateTime(menuOrder.Date) == DateTime.Today)
                        .Select(food => new MenuModel {
                            Starter = food.Starter,
                            MainFood = food.MainFood,
                            SideDish = food.SideDish,
                            Salad = food.Salad,
                            Drink = food.Drink
                        }).FirstOrDefault();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al obtener los menús de desayuno del día, ", ex);
            }
            return menu;
        }

        /** <summary>
          * Este método obtiene los menús registrados del día de las comidas
          * </summary>
          * <returns>Objeto MenuModel</returns>
          */
        public MenuModel GetFoodOfTheDay() {
            MenuModel menu = null;
            const string KEY_FOOD = "Com";
            try {
                using (var context = new KomalliEntities()) {
                    menu = context.SetMenu
                        .Where(menuOrder => menuOrder.KeySetMenu.StartsWith(KEY_FOOD) &&
                                            DbFunctions.TruncateTime(menuOrder.Date) == DateTime.Today)
                        .Select(food => new MenuModel {
                            Starter = food.Starter,
                            MainFood = food.MainFood,
                            SideDish = food.SideDish,
                            Salad = food.Salad,
                            Drink = food.Drink
                        }).FirstOrDefault();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al obtener los menús de comida del día, ", ex);
            }
            return menu;
        }
    }
}
