using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts
{
    public interface IIngredient
    {
        public int IsNameIngredientExisting(string nameIngredient);

        public int IsBarCodeExisting(string barCodeIngredient);

        public int AddIngredient(IngredientModel ingredient);

        public List<IngredientModel> ConsultIngredients();

        public int ModifyIngredients(List<IngredientModel> ingredients);

        public List<IngredientModel> SearchIngredients (string searchIngredient);

        public List<IngredientModel> SearchIngredientsByCategory(string selectedCategory);

    }
}
