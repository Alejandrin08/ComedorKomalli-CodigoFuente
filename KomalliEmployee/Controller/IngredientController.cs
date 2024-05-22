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
    public class IngredientController : IIngredient {

        /**
         * <summary>
         * Este método se encarga agregar nuevos ingredientes a la base de datos del sistema
         * </summary>
         * <param name="ingredient">objeto ingrediente contodos los satos necesarios</param>
         * <returns>Regresa 1 si se realizo la operacion correctamente, 0 si no.</returns>
         */
        public int AddIngredient(IngredientModel ingredientModel) {
            int result = 0;
            DateTime replenishmentDate = DateTime.Now.Date;
            try {
                using (var context = new KomalliEntities()) {
                    var newIngredient = new Ingredient {
                        KeyIngredient = ingredientModel.KeyIngredient,
                        Barcode = ingredientModel.BarCode,
                        NameIngredient = ingredientModel.NameIngredient,
                        Quantity = ingredientModel.Quantity,
                        Measurement = ingredientModel.Measurement.ToString(),
                        Category = ingredientModel.Category.ToString(),
                        ReplenishmentDate = replenishmentDate
                    };

                    context.Ingredient.Add(newIngredient);
                    result = context.SaveChanges();
                }
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un ingrediente", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al agregar un ingrediente", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de obtener todos los datos de cada uno de los ingredientes registrados en la base de datos.
         * </summary>
         * <returns> Regresa la lista de los ingredientes obtenidos en la base de datos.</returns>
         */
        public List<IngredientModel> ConsultIngredients() {
            List<IngredientModel> ingredients = new List<IngredientModel>();
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Ingredient.Select(ingredient => new {
                        ingredient.KeyIngredient,
                        ingredient.NameIngredient,
                        ingredient.Quantity,
                        ingredient.Measurement,
                        ingredient.Barcode,
                        ingredient.ReplenishmentDate,
                        ingredient.Category
                    }).ToList()
                    .Select(ingredient => new IngredientModel {
                        KeyIngredient = ingredient.KeyIngredient,
                        NameIngredient = ingredient.NameIngredient,
                        Quantity = ingredient.Quantity,
                        Measurement = (TypeQuantity)Enum.Parse(typeof(TypeQuantity), ingredient.Measurement),
                        Category = (IngredientCategory)Enum.Parse(typeof(IngredientCategory), ingredient.Category),
                        BarCode = ingredient.Barcode,
                        ReplenishmentDate = ingredient.ReplenishmentDate
                    }).ToList();

                    ingredients = query;
                }
            } catch (EntityException ex) {
                ingredients = null;
                LoggerManager.Instance.LogError("Error al consultar un ingrediente", ex);
            }
            return ingredients;
        }

        /**
         * <summary>
         * Este método se encarga de buscar dentro de la base de datos si existe algún registro anterior con el codigo de barras ingresado.
         * </summary>
         * <param name="barCodeingredient">Codigo de barras del ingrediente</param>
         * <returns>Regresa 1 si existe un ingrediente con ese codigo de barras, 0 si no.</returns>
         */

        public int IsBarCodeExisting(string barCodeIngredient) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Ingredient.Where(ingredient => ingredient.Barcode == barCodeIngredient).FirstOrDefault();
                    if (query != null) {
                        result = 1;
                    }
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al validar la existenciia de un código de barras", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de buscar dentro de la base de datos si existe algún registro anterior con el nombre del ingrediente ingresado.
         * </summary>
         * <param name="nameIngredient">Codigo de barras del ingrediente</param>
         * <returns>Regresa 1 si existe un ingrediente con ese nombre, 0 si no.</returns>
         */

        public int IsNameIngredientExisting(string nameIngredient) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Ingredient.Where(ingredient => ingredient.NameIngredient == nameIngredient).FirstOrDefault();
                    if (query != null) {
                        result = 1;
                    }
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al validar la existencia del nombre de un ingrediente", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de modificar los ingredientes en la base de datos. Verifica si hay algún otro ingrediente con el mismo nombre antes de actualizar.
         * </summary>
         * <param name="ingredients">La lista de ingredientes a modificar</param>
         * <returns>1 si la modificación se realizó con éxito, 0 si hay un conflicto de nombres, -1 si ocurre un error durante la actualización</returns>
         */
        public int ModifyIngredients(List<IngredientModel> ingredients) {
            int result = 0;
            DateTime replenishmentDate = DateTime.Now.Date;
            try {
                using (var context = new KomalliEntities()) {
                    foreach (var ingredient in ingredients) {
                        var existingIngredientWithSameName = context.Ingredient.FirstOrDefault(i => i.KeyIngredient != ingredient.KeyIngredient && i.NameIngredient == ingredient.NameIngredient);
                        if (existingIngredientWithSameName != null) {
                            result = 0;
                            return result;
                        }
                        var existingIngredient = context.Ingredient.FirstOrDefault(i => i.KeyIngredient == ingredient.KeyIngredient);
                        if (existingIngredient != null) {
                            existingIngredient.NameIngredient = ingredient.NameIngredient;
                            existingIngredient.Quantity = ingredient.Quantity;
                            existingIngredient.Measurement = ingredient.Measurement.ToString();
                            existingIngredient.Barcode = ingredient.BarCode;
                            existingIngredient.ReplenishmentDate = replenishmentDate;
                            existingIngredient.Category = ingredient.Category.ToString();
                        }
                    }
                    context.SaveChanges();
                    result = 1;
                }
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al actualizar información de ingredientes", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al actualizar información de ingredientes", ex);
            }
            return result;
        }

        /**
        * <summary>
        * Este método realiza una búsqueda de ingredientes basada en los criterios de búsqueda proporcionados.
        * </summary>
        * <param name="searchIngredient">La palabra clave para buscar dentro de los nombres de los ingredientes, la clave o el código de barras.</param>
        * <param name="category">La categoría de ingredientes para filtrar los resultados de búsqueda. Use "General" para buscar en todas las categorías.</param>
        * <returns>
        * Una lista de objetos <see cref="IngredientModel"/> que coinciden con los criterios de búsqueda. Si ocurre un error durante la búsqueda, devuelve null.
        * </returns>
        * <remarks>
        * Este método consulta la base de datos en busca de ingredientes según la palabra clave de búsqueda y la categoría proporcionadas. Si se proporciona una palabra clave de búsqueda, busca coincidencias dentro de la clave, el nombre o el código de barras de los ingredientes.
        * Además, permite filtrar los resultados de búsqueda según la categoría del ingrediente. Si la categoría proporcionada es "General", la búsqueda se realiza en todas las categorías.
        * La lista devuelta contiene objetos <see cref="IngredientModel"/> mapeados desde las entidades de la base de datos. Si ocurre un error durante la búsqueda, registra el error y devuelve null.
        * </remarks>
        * <exception cref="EntityException">Se lanza cuando ocurre un error al acceder a la base de datos.</exception>
        */
        public List<IngredientModel> SearchIngredients(string searchIngredient, string category) {
            List<IngredientModel> ingredients = new List<IngredientModel>();
            try {
                using (var context = new KomalliEntities()) {
                    IQueryable<Ingredient> query = context.Ingredient;
                    if (!string.IsNullOrEmpty(searchIngredient)) {
                        query = query.Where(ingredient =>
                            ingredient.KeyIngredient.Contains(searchIngredient) ||
                            ingredient.NameIngredient.Contains(searchIngredient) ||
                            ingredient.Barcode.Contains(searchIngredient));
                    }
                    if (category != "General") {
                        query = query.Where(ingredient => ingredient.Category == category);
                    }

                    var result = query.ToList();
                    ingredients = result.Select(ingredient => new IngredientModel {
                        KeyIngredient = ingredient.KeyIngredient,
                        NameIngredient = ingredient.NameIngredient,
                        Quantity = ingredient.Quantity,
                        Measurement = (TypeQuantity)Enum.Parse(typeof(TypeQuantity), ingredient.Measurement),
                        Category = (IngredientCategory)Enum.Parse(typeof(IngredientCategory), ingredient.Category),
                        BarCode = ingredient.Barcode,
                        ReplenishmentDate = ingredient.ReplenishmentDate
                    }).ToList();
                }
            } catch (EntityException ex) {
                ingredients = null;
                LoggerManager.Instance.LogError("Error al buscar ingredientes", ex);
            }
            return ingredients;
        }
    }
}
