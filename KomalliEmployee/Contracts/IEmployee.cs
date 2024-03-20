using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IEmployee {

        public Dictionary<UserRole, int> ValidateUser(string email, string password);
        public int UpdatePassword(string email, string password);
    }
}
