using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Controller;

namespace KomalliEmployee.Model
{
    public class IngredientModel{
        public string KeyIngredient { get; set; }
        public string NameIngredient { get; set; }
        public string Quantity {  get; set; }
        public TypeQuantity Measurement {  get; set; }
        public string BarCode { get; set; }

    }
}
