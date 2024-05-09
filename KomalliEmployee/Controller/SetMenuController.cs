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

        public SetMenuModel ExistingMenu(DateTime date, string typeMenu)
        {
            SetMenuModel setMenuModel = null;

            try
            {
                using (var context = new KomalliEntities())
                {
                    var existingMenu = context.SetMenu.FirstOrDefault(menu =>
                        menu.Date == date && menu.TypeMenu == typeMenu);
                    if (existingMenu != null)
                    {
                        
                        setMenuModel = new SetMenuModel
                        {
                            DateMenu = existingMenu.Date,
                            MainFood = existingMenu.MainFood,
                            Starter = existingMenu.Starter,
                            SideDish = existingMenu.SideDish,
                            Drink = existingMenu.Drink,
                            KeySetMenu = existingMenu.KeySetMenu,
                            Salad = existingMenu.Salad,
                            Pricegeneral = existingMenu.GeneralPrice,
                            PriceStudent = existingMenu.StudentPrice,
                            Type = existingMenu.TypeMenu == "Desayuno" ? TypeMenu.Desayuno : TypeMenu.Comida
                        };
                    }
                }
            }
            catch (EntityException ex)
            {
                LoggerManager.Instance.LogError("Error al verificar la existencia de un menú", ex);
            }

            return setMenuModel;
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

        public int ModifyMenu(SetMenuModel setMenu)
        {
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var existingMenu = context.SetMenu.FirstOrDefault(menu => menu.KeySetMenu == setMenu.KeySetMenu);

                    if (existingMenu != null)
                    {
                        existingMenu.Date = setMenu.DateMenu;
                        existingMenu.Starter = setMenu.Starter;
                        existingMenu.MainFood = setMenu.MainFood;
                        existingMenu.SideDish = setMenu.SideDish;
                        existingMenu.Salad = setMenu.Salad;
                        existingMenu.Drink = setMenu.Drink;
                        existingMenu.StudentPrice = setMenu.PriceStudent;
                        existingMenu.GeneralPrice = setMenu.Pricegeneral;
                        existingMenu.TypeMenu = setMenu.Type.ToString();
                        result = context.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                result = -1;
                LoggerManager.Instance.LogError("Error al modificar un menú", ex);
            }
            catch (EntityException ex)
            {
                result = -1;
                LoggerManager.Instance.LogError("Error al modificar un menú", ex);
            }
            return result;
        }
    }
}
