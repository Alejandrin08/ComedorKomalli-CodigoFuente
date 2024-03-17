using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Controller {
    internal class EmployeeController : IEmployee {
        public int UpdatePassword(string email, string password) {
            const int USER_NEW = 1;
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.User.Where(user => user.Email == email).FirstOrDefault();
                    if (query != null) {
                        query.Password = password;
                        query.Available = USER_NEW;
                    }
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error in UpdatePassword", ex);
            }
            return result;
        }

        public Dictionary<UserRole, int> ValidateUser(string email, string password) {
            const int USER_INVALID = -1;
            Dictionary<UserRole, int> result = new Dictionary<UserRole, int>();
            try {
                using (var context = new KomalliEntities()) {
                    ObjectParameter userRole = new ObjectParameter("UserRole", typeof(string));
                    ObjectParameter userAvailable = new ObjectParameter("UserAvailable", typeof(int));

                    int userValidation = context.ValidateUserLogin(email, password, userRole, userAvailable);

                    string userRoleValue = userRole.Value != DBNull.Value ? userRole.Value.ToString() : null;

                    if (Enum.TryParse(userRoleValue, true, out UserRole role)) {
                        result.Add(role, Convert.ToInt32(userAvailable.Value));
                    } else {
                        result.Add(UserRole.Invalid, USER_INVALID);
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error in ValidateUser", ex);
            }
            return result;
        }
    }
}
