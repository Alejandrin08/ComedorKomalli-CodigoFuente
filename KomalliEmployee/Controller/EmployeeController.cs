using KomalliEmployee.Contracts;
using KomalliEmployee.Model.Utilities;
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
    public class EmployeeController : IEmployee {

        /**
         * <summary>
         * Este método se encarga de obtener el rol de un usuario.
         * </summary>
         * <param name="email">Correo del usuario</param>
         * <returns>Regresa el rol del usuario</returns>
         */                                                
        public UserRole GetUserRule(string email) {
            UserRole userRole = UserRole.Invalid;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Employee.Where(user => user.UserEmail == email).FirstOrDefault();
                    if (query != null) {
                        userRole = (UserRole)Enum.Parse(typeof(UserRole), query.Role);   
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al obtener el rol del usuario. Método GetUserRule", ex);
            }
            return userRole;
        }

        /**
         * <summary>
         * Este método se encarga de actualizar la contraseña de un usuario así como cambiar su estado lógico.
         * </summary>
         * <param name="email">Correo del usuario</param>
         * <param name="password">Nueva contraseña</param>
         * <returns>Regresa 1 si se realizo la actualización de la contraseña correctamente, 0 si no.</returns>
         */
        public int UpdatePassword(string email, string newPassword) {
            const int USER_NEW = 1;
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.User.Where(user => user.Email == email).FirstOrDefault();
                    if (query != null) {
                        query.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        query.Available = USER_NEW;
                    }
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al actualizar la contraseña. Método UpdatePassword", ex);
            } catch (BCrypt.Net.SaltParseException ex) {
                LoggerManager.Instance.LogError("Error al actualizar la contraseña. Método UpdatePassword", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de validar un usuario.
         * </summary>
         * <param name="email">Correo del usuario</param>
         * <param name="password">Contraseña del usuario</param>
         * <returns>Regresa 1 si el usuario ya cambio su contraseña, 0 si no, -1 si hubo un error.</returns>
         */ 
        public int ValidateUser(string email, string password) {
            int result = -1;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.User.Where(user => user.Email == email).FirstOrDefault();
                    if (query != null && BCrypt.Net.BCrypt.Verify(password, query.Password)) {
                        result = query.Available;    
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al validar el usuario. Método ValidateUser", ex);
            } catch (BCrypt.Net.SaltParseException ex) {
                LoggerManager.Instance.LogError("Error al validar el usuario. Método ValidateUser", ex);
            }
            return result;
        }
    }
}
