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
        public string GetUserRule(string email);
        public int AddUser(EmployeeModel employeeModel);
        public int AddEmployee(EmployeeModel employeeModel);
        public int ValidateEmail(string email);
        public int ValidateUser(string email, string password);
        public int ValidatePersonalNumber(string personalNumber);
        public string GetUserName(string email);
        public string GetNoPersonalEmployee(string email);
        public List<EmployeeModel> ConsultUsers();
        public EmployeeModel GetUserInfo(string personalNumber);
        public int UpdateUserInfo(EmployeeModel employeeModel, string email);
        public int UpdateEmployeeInfo(EmployeeModel employeeModel, string email);

    }
}
