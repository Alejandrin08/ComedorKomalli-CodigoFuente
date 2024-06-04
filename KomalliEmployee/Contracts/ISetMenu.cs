using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts
{
    public interface ISetMenu
    {
        public int ExistingTypeMenu(SetMenuModel setMenu);

        public int AddSetMenu(SetMenuModel setMenu);

        public SetMenuModel ExistingMenu(DateTime date, string typeMenu);

        public int ModifyMenu(SetMenuModel setMenu);

        public int DeleteMenu(string keySetmenu);
        public int UpdateMenuCard();
    }
}
