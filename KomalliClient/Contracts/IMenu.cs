﻿using KomalliClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Contracts {
    public interface IMenu {
        public MenuModel GetBreakfastOfTheDay();
        public MenuModel GetFoodOfTheDay();
    }
}
