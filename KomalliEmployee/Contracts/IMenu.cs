using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IMenu {
        public MenuModel GetBreakfastOfTheDay(DateTime date);
        public MenuModel GetFoodOfTheDay(DateTime date);
    }
}
