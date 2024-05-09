using KomalliEmployee.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model
{
    public class SetMenuModel
    {
        public string KeySetMenu { get; set; }
        public DateTime DateMenu { get; set; }
        public string Starter {  get; set; }
        public string MainFood { get; set; }
        public string SideDish {  get; set; }
        public string Salad {  get; set; }
        public string Drink { get; set; }
        public int PriceStudent { get; set; }
        public int Pricegeneral { get; set; }
        public TypeMenu Type { get; set; }
    }
}
