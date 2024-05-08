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
    }
}
