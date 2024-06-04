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

namespace KomalliEmployee.Controller {
    public class SetMenuController : ISetMenu {
        /**
         * <summary>
         * Agrega un nuevo menú a la base de datos.
         * </summary>
         * <param name="setMenu">El modelo de datos del menú a agregar.</param>
         * <returns> 1 si se agrega correctamente, -1 si hay un error.</returns>
         * <exception cref="DbUpdateException">Se lanza cuando hay un problema al actualizar la base de datos.</exception>
         * <exception cref="EntityException">Se lanza cuando hay un error de acceso a la base de datos.</exception>
         */
        public int AddSetMenu(SetMenuModel setMenu) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var newSetMenu = new SetMenu {
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
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un menú", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un menú", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Elimina un menú de la base de datos.
         * </summary>
         * <param name="keySetmenu">La clave del menú a eliminar.</param>
         * <returns> 1 si se elimina correctamente, -1 si hay un error.</returns>
         * <exception cref="DbUpdateException">Se lanza cuando hay un problema al actualizar la base de datos.</exception>
         * <exception cref="EntityException">Se lanza cuando hay un error de acceso a la base de datos.</exception>
         */
        public int DeleteMenu(string keySetmenu) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var existingMenu = context.SetMenu.FirstOrDefault(menu => menu.KeySetMenu == keySetmenu);

                    if (existingMenu != null) {
                        context.SetMenu.Remove(existingMenu);
                        result = context.SaveChanges();
                    }
                }
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al eliminar un menú", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al eliminar un menú", ex);
            }
            return result;
        }

        public SetMenuModel ExistingMenu(DateTime date, string typeMenu) {
            SetMenuModel setMenuModel = null;

            try {
                using (var context = new KomalliEntities()) {
                    var existingMenu = context.SetMenu.FirstOrDefault(menu =>
                        menu.Date == date && menu.TypeMenu == typeMenu);
                    if (existingMenu != null) {

                        setMenuModel = new SetMenuModel {
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
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al verificar la existencia de un menú", ex);
            }

            return setMenuModel;
        }

        /**
         * <summary>
         * Verifica si ya existe un menú del mismo tipo para la fecha proporcionada en la base de datos.
         * </summary>
         * <param name="setMenu">El modelo de datos del menú para verificar.</param>
         * <returns>1 si existe un menú del mismo tipo para la fecha proporcionada, 0 si no existe, -1 si hay un error.</returns>
         * <exception cref="EntityException">Se lanza cuando hay un error de acceso a la base de datos.</exception>
         */
        public int ExistingTypeMenu(SetMenuModel setMenu) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    bool menuExists = context.SetMenu.Any(menu =>
                        menu.Date == setMenu.DateMenu &&
                        menu.TypeMenu == setMenu.Type.ToString());

                    result = menuExists ? 1 : 0;
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al verificar la existencia de un menú", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Modifica un menú existente en la base de datos.
         * </summary>
         * <param name="setMenu">El modelo de datos del menú a modificar.</param>
         * <returns>0 si se modifica correctamente, -1 si hay un error.</returns>
         * <exception cref="DbUpdateException">Se lanza cuando hay un problema al actualizar la base de datos.</exception>
         * <exception cref="EntityException">Se lanza cuando hay un error de acceso a la base de datos.</exception>
         */
        public int ModifyMenu(SetMenuModel setMenu) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var existingMenu = context.SetMenu.FirstOrDefault(menu => menu.KeySetMenu == setMenu.KeySetMenu);

                    if (existingMenu != null) {
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
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al modificar un menú", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al modificar un menú", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Actualiza la carta 
         * </summary>
         * <returns>1 si se modifica correctamente, 0 si hay un error.</returns>
         */
        public int UpdateMenuCard() {
            int result = 0;
            const int TOTAL_STOCK = 50;
            try {
                using (var context = new KomalliEntities()) {
                    var menuCard = context.MenuCard.ToList();
                    foreach (var menu in menuCard) {
                        menu.Stock = TOTAL_STOCK;
                    }
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar la carta", ex);
            }
            return result;
        }
    }
}
