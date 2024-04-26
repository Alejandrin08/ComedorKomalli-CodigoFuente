using KomalliEmployee.Model;
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

        public int AddUser(EmployeeModel employeeModel);

        public int AddEmployee(EmployeeModel employeeModel);

        public int ValidateEmail(string email);

        public int ValidatePersonalNumber(string personalNumber);

        public string GetNameUser(string email);


        public int GetNoPersonalEmployee(string email);


        public List<EmployeeModel> ConsultUsers();
    }
}
