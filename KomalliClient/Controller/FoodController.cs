using KomalliClient.Contracts;
using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Controller {
    public class FoodController : IFood {
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
                            Section = food.Section,
                            Image = food.Image
                        }).ToList();
                }
            } catch (EntityException ex) {
                foods = null;
                LoggerManager.Instance.LogError("Error al obtener los alimentos por sección: ", ex);
            }
            return foods;
        }
    }
}
