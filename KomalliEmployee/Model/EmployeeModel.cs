using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class EmployeeModel { 
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public string RoleUser { get; set; }
        public string Availability { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
    }
}
