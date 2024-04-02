using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        /**
         * <summary>
         * Este método agrega un usuario a la base de datos.
         * </summary>
         * <param name="employeeModel">Objeto de tipo EmployeeModel que regresa el email y contraseña. </param>
         * <returns>Regresa 1 se se agrega correctamente, 0 si ocurre un error.</returns>
         */

        public int AddUser(EmployeeModel employeeModel) {
            const int NEW_REGISTER = 0;
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var user = new User {
                        Email = employeeModel.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(employeeModel.Password),
                        Available = NEW_REGISTER,
                    };
                    context.User.Add(user);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en AddUser", ex);
            } catch (DbUpdateException ex) {
                LoggerManager.Instance.LogError("Error en AddUser", ex);
            }
            return result;

        }

        /**
         * <summary>
         * Este método agrega un empleado a la base de datos.
         * </summary>
         * <param name="employeeModel">Objeto de tipo EmployeeModel que regresa el email, numero de personal, rol y nombre. </param>
         * <returns>Regresa 1 se se agrega correctamente, 0 si ocurre un error.</returns>
         */

        public int AddEmployee(EmployeeModel employeeModel) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var user = new Employee {
                        NoPersonal = employeeModel.PersonalNumber,
                        UserEmail = employeeModel.Email,
                        Role = employeeModel.Role.ToString(),
                        Name = employeeModel.Name,
                    };
                    context.Employee.Add(user);
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en AddEmployee", ex);
            }
            return result;

        }

        /**
         * <summary>
         * Este método valida la existencia de un email.
         * </summary>
         * <param name="email">Correo del usuario. </param>
         * <returns>Regresa -1 si no hay registros de ese email, 1 si los hay.</returns>
         */

        public int ValidateEmail(string email) {
            int result = -1;
            try {
                using (var context = new KomalliEntities()) {
                    var validateEmail = (from user in context.User
                                         where user.Email == email
                                         select user).FirstOrDefault();
                    if (validateEmail != null) {
                        result = 1;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en ValidateEmail", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método valida la existencia de un numero de personal.
         * </summary>
         * <param name="personalNumber">Numero de personal del usuario. </param>
         * <returns>Regresa -1 si no hay registros de ese numero de personal, 1 si los hay.</returns>
         */

        public int ValidateNoPersonal(string personalNumber) {
            int result = -1;
            try {
                using (var context = new KomalliEntities()) {
                    var validateNoPersonal = (from employee in context.Employee
                                              where employee.NoPersonal == personalNumber
                                              select employee).FirstOrDefault();
                    if (validateNoPersonal != null) {
                        result = 1;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en ValidateNoPersonal", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de regresar el nombre de un usuario.
         * </summary>
         * <param email="email">Correo del usuario.. </param>
         * <returns>Regresa el nombre si es que lo encuntra, si no un regresa null.</returns>
         */

        public string GetNameUser(string email) {
            string nameResult = null;
            try {
                using (var context = new KomalliEntities()) {
                    var getName = (from employee in context.Employee
                                              where employee.UserEmail == email
                                              select employee).FirstOrDefault();
                    if (getName != null) {
                        nameResult = getName.Name;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en obtener el nombre de usuario", ex);
            }
            return nameResult;
        }
    }
}
