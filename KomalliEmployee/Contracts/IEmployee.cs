using KomalliEmployee.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface IEmployee {

        public int UpdatePassword(string email, string password);

        public UserRole GetUserRule(string email);

        public int ValidateUser(string email, string password);

        public int GetNoPersonalEmployee(string email);
    }
}
