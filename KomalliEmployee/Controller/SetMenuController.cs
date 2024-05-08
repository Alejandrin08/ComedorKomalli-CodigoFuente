using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KomalliEmployee.Controller
{
    public class SetMenuController : ISetMenu
    {
        public int AddSetMenu(SetMenuModel setMenu)
        {
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var newSetMenu = new SetMenu
                    {
                        KeySetMenu = setMenu.KeySetMenu,
                        Date = setMenu.DateMenu,
                        Starter = setMenu.Starter,
                        MainFood = setMenu.MainFood,
                        SideDish = setMenu.SideDish,
                        Salad = setMenu.Salad,
                        Drink = setMenu.Drink,
                        StudentPrice = setMenu.PriceStudent,
                        GeneralPrice = setMenu.Pricegeneral,
                        TypeMenu = setMenu.Type.ToString() 
                    };
                    context.SetMenu.Add(newSetMenu);
                    result = context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un menú", ex);
            }
            catch (EntityException ex)
            {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un menú", ex);
            }
            return result;
        }

        public int ExistingTypeMenu(SetMenuModel setMenu)
        {
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    bool menuExists = context.SetMenu.Any(menu =>
                        menu.Date == setMenu.DateMenu &&
                        menu.TypeMenu == setMenu.Type.ToString());

                    result = menuExists ? 1 : 0;
                }
            }
            catch (EntityException ex)
            {
                result = -1;
                LoggerManager.Instance.LogError("Error al verificar la existencia de un menú", ex);
            }
            return result;
        }
    }
}
