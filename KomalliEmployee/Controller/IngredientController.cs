using KomalliEmployee.Contracts;
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
    public class IngredientController : IIngredient{

        /**
         * <summary>
         * Este método se encarga agregar nuevos ingredientes a la base de datos del sistema
         * </summary>
         * <param name="ingredient">objeto ingrediente contodos los satos necesarios</param>
         * <returns>Regresa 1 si se realizo la operacion correctamente, 0 si no.</returns>
         */
        public int AddIngredient(Ingredient ingredient){
            int result = 0;
            try { 
                using (var context = new KomalliEntities())
                {
                    var newIngredient = new KomalliServer.Ingredient
                    {
                        KeyIngredient = ingredient.KeyIngredient,
                        Barcode = ingredient.Barcode,
                        NameIngredient = ingredient.NameIngredient,
                        Quantity = ingredient.Quantity,
                        Measurement = ingredient.Measurement
                    };

                    context.Ingredient.Add(newIngredient);
                    context.SaveChanges();
                    result = 1;

                }
                return result;
            }
            catch (DbUpdateException){
                result = 2;
                return result;
            }
            catch (EntityException){
                result = 2;
                return result;
            }
        }

        /**
         * <summary>
         * Este método se encarga de obtener todos los datos de cada uno de los ingredientes registrados en la base de datos.
         * </summary>
         * <returns> Regresa la lista de los ingredientes obtenidos en la base de datos.</returns>
         */
        public List<Ingredient> ConsultIngredients(){
            List<Ingredient> ingredients = new List<Ingredient> ();
            try
            {
                using (var context = new KomalliEntities())
                {
                    ingredients = context.Ingredient.ToList(); 
                }
                return ingredients;
            }
            catch (EntityException){
                return null;
            }
        }

        /**
         * <summary>
         * Este método se encarga de buscar dentro de la base de datos si existe algún registro anterior con el codigo de barras ingresado.
         * </summary>
         * <param name="barCodeingredient">Codigo de barras del ingrediente</param>
         * <returns>Regresa 1 si existe un ingrediente con ese codigo de barras, 0 si no.</returns>
         */

        public int IsBarCodeExisting(string barCodeIngredient){
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var query = context.Ingredient.Where(ingredient => ingredient.Barcode == barCodeIngredient).FirstOrDefault();
                    if (query != null)
                    {
                        result = 1;
                    }
                }
                return result;
            }
            catch (EntityException){
                result = 2;
                return result;
            }
        }

        /**
         * <summary>
         * Este método se encarga de buscar dentro de la base de datos si existe algún registro anterior con el nombre del ingrediente ingresado.
         * </summary>
         * <param name="nameIngredient">Codigo de barras del ingrediente</param>
         * <returns>Regresa 1 si existe un ingrediente con ese nombre, 0 si no.</returns>
         */

        public int IsNameIngredientExisting(string nameIngredient){
            int result = 0;
            try
            {
                using (var context = new KomalliEntities())
                {
                    var query = context.Ingredient.Where(ingredient => ingredient.NameIngredient == nameIngredient).FirstOrDefault();
                    if (query == null)
                    {
                        result = 1;
                    }
                }
                return result;
            }
            catch (EntityException)
            {
                result = 2;
                return result;
            }
        }
    }
}
