using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomalliEmployee.Model.Utilities;

namespace KomalliEmployee.Controller {
    public class MenuController : IMenu {
        /** <summary>
          * Este método obtiene los menús registrados del día de los desayunos
          * </summary>
          * <param name="date">Fecha de la que se desea obtener el menú</param>
          * <returns>Objeto MenuModel</returns>
          */
        public MenuModel GetBreakfastOfTheDay(DateTime date) {
            MenuModel menu = null;
            const string KEY_BREAKFAST = "Des";
            try {
                using (var context = new KomalliEntities()) {
                    menu = context.SetMenu
                        .Where(menuOrder => menuOrder.KeySetMenu.StartsWith(KEY_BREAKFAST) &&
                                            DbFunctions.TruncateTime(menuOrder.Date) == date)
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
          * <param name="date">Fecha de la que se desea obtener el menú</param>
          * <returns>Objeto MenuModel</returns>
          */
        public MenuModel GetFoodOfTheDay(DateTime date) {
            MenuModel menu = null;
            const string KEY_FOOD = "Com";
            try {
                using (var context = new KomalliEntities()) {
                    menu = context.SetMenu
                        .Where(menuOrder => menuOrder.KeySetMenu.StartsWith(KEY_FOOD) &&
                                            DbFunctions.TruncateTime(menuOrder.Date) == date)
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
