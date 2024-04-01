using KomalliServer;
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

        public int AddIngredient(Ingredient ingredient);

        public List<Ingredient> ConsultIngredients();


    }
}
