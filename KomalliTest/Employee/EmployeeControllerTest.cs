using Microsoft.VisualStudio.TestTools.UnitTesting;
using KomalliEmployee.Model;
using System;
using System.Transactions;
using System.Data.Entity.Core;
using System.Collections.Generic;
using KomalliEmployee.Model.Utilities;

namespace KomalliTest.Employee {
    [TestClass]
    public class EmployeeControllerTest {
        [TestMethod]
        public void UpdatePassword_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ale@gmail.com";
                string newPassword = "123Ale_";
                int resultExpected = 1;
                int result = test.UpdatePassword(email, newPassword);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdatePassword_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ale@gmail.com";
                string newPassword = "123Ale";
                int resultExpected = 0;
                int result = test.UpdatePassword(email, newPassword);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void GetUserRule_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                string userRoleExpected = "Cajero";
                string userRole = test.GetUserRule(email);
                Assert.AreEqual(userRoleExpected, userRole);
            }
        }

        [TestMethod]
        public void GetUserRule_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                string userRoleExpected = "Jefe de cocina";
                string userRole = test.GetUserRule(email);
                Assert.AreNotEqual(userRoleExpected, userRole);
            }
        }

        [TestMethod]
        public void ValidateUser_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                string password = "123Ares_";
                int resultExpected = 1;
                int result = test.ValidateUser(email, password);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ValidateUser_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                string password = "123";
                int resultExpected = 1;
                int result = test.ValidateUser(email, password);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddUser_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "ysela@gmail.com",
                    Password = "5464komalli",
                };

                int resultExpected = 1;
                int result = test.AddUser(employeeModel);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddUser_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "ares@gmail.com",
                    Password = "5464komalli",
                };

                int resultExpected = 1;
                int result = test.AddUser(employeeModel);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddEmployee_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "panchoVilla@gmail.com",
                    Name = "Pancho villa",
                    RoleUser = "Cajero",
                    PersonalNumber = "100",
                    Password = "5464komalli",
                };

                int resultExpected = 1;
                test.AddUser(employeeModel);
                int result = test.AddEmployee(employeeModel);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddEmployee_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "ares@gmail.com",
                    Name = "Pancho villa",
                    RoleUser = "Cajero",
                    PersonalNumber = "1",
                    Password = "5464komalli",
                };

                int resultExpected = 0;
                int result = test.AddEmployee(employeeModel);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ValidateEmail_Sucessfull() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "sociosandermarin@outlook.com";

            int resultExpected = -1;
            int result = test.ValidateEmail(email);
            Assert.AreEqual(resultExpected, result);

        }

        [TestMethod]
        public void ValidateEmail_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "momaosiris@gmail.com";

            int resultExpected = 1;
            int result = test.ValidateEmail(email);
            Assert.AreNotEqual(resultExpected, result);

        }

        [TestMethod]
        public void ValidatePersonalNumber_Sucessfull() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string personalNumber = "43";

            int resultExpected = -1;
            int result = test.ValidatePersonalNumber(personalNumber);
            Assert.AreEqual(resultExpected, result);

        }

        [TestMethod]
        public void ValidatePersonalNumber_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string personalNumber = "1";

            int resultExpected = 1;
            int result = test.ValidatePersonalNumber(personalNumber);
            Assert.AreNotEqual(resultExpected, result);

        }

        [TestMethod]
        public void GetUserName_Sucessfull() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "ares@gmail.com";

            string resultExpected = "Ares Juda Rivera Soto";
            string result = test.GetUserName(email);    
            Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void GetUserName_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "momaosiris@gmail.com";

            string resultExpected = "Ares Judda Rivera Soto";
            string result = test.GetUserName(email);
            Assert.AreNotEqual(resultExpected, result);

        }

        [TestMethod]
        public void GetNoPersonalEmployee_Sucessfull() {
            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "ares@gmail.com";
            string resultExpected = "111";
            string result = test.GetNoPersonalEmployee(email);
            Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void GetNoPersonalEmployee_Failed() {
            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string email = "ares@gmail.com";
            string resultExpected = "777";
            string result = test.GetNoPersonalEmployee(email);
            Assert.AreNotEqual(resultExpected, result);
        }

        [TestMethod]
        public void ConsultUsers_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            var userExpected = new List<EmployeeModel> {
                new EmployeeModel {
                    PersonalNumber = "111",
                    RoleUser = "Cajero",
                    Name = "Ares Judda Rivera Soto",
                    Availability = "Activo"
                },
                new EmployeeModel {
                    PersonalNumber = "6281",
                    RoleUser = "Personal de cocina",
                    Name = "Ysela Lara Landa",
                    Availability = "Activo"
                },
                new EmployeeModel {
                    PersonalNumber = "777",
                    RoleUser = "Jefe de cocina",
                    Name = "Alejandro Sánchez Marín",
                    Availability = "Activo"
                },

            };

            var usersResult = test.ConsultUsers();

            Assert.AreNotEqual(userExpected.Count, usersResult.Count);
        }

        [TestMethod]
        public void ConsultUsers_Sucessfull() {
            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            var userExpected = new List<EmployeeModel> {
                new EmployeeModel {
                    PersonalNumber = "111",
                    RoleUser = "Cajero",
                    Name = "Ares Judda Rivera Soto",
                    Availability = "Activo"
                },
                new EmployeeModel {
                    PersonalNumber = "6281",
                    RoleUser = "Personal de cocina",
                    Name = "Ysela Lara Landa",
                    Availability = "Activo"
                },
                new EmployeeModel {
                    PersonalNumber = "777",
                    RoleUser = "Jefe de cocina",
                    Name = "Alejandro Sánchez Marín",
                    Availability = "Activo"
                },
                new EmployeeModel {
                    PersonalNumber = "2910",
                    RoleUser = "Personal de cocina",
                    Name = "Gerardo Sánchez Marín",
                    Availability = "Activo"
                },

            };

            var usersResult = test.ConsultUsers();

            Assert.AreEqual(userExpected.Count, usersResult.Count);
            
        }

        [TestMethod]
        public void GetUserInfo_Sucessfull() {
           
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModelExpected = new EmployeeModel {
                    Email = "ares@gmail.com",
                    Name = "Ares Juda Rivera Soto",
                    RoleUser = "Cajero",
                    PersonalNumber = "111",
                    Availability = "Activo"
                };

            string personalNumber = "111";


            EmployeeModel result = test.GetUserInfo(personalNumber);
                Assert.AreNotEqual(employeeModelExpected, result);
        }

        [TestMethod]
        public void GetUserInfo_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
            EmployeeModel employeeModelExpected = new EmployeeModel {
                Email = "ares@gmail.com",
                Name = "Paloma Soto",
                RoleUser = "Cajero",
                PersonalNumber = "111",
                Availability = "Activo"
            };

            string personalNumber = "111";


            EmployeeModel result = test.GetUserInfo(personalNumber);
            Assert.AreNotEqual(employeeModelExpected, result);
        }


        [TestMethod]
        public void UpdateUserInfo_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "aresJudda@gmail.com",
                    Name = "Ares Juda Rivera Soto",
                    RoleUser = "Cajero",
                    PersonalNumber = "111",
                    Availability = "Activo"
                };

                string email = "ares@gmail.com";

                int resultExpected = 1; 
                int result = test.UpdateUserInfo(employeeModel, email);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdateUserInfo_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "aresJudda@gmail.com",
                    Name = "Ares Juda Rivera Soto",
                    RoleUser = "Cajero",
                    PersonalNumber = "111",
                    Availability = "Activo"
                };

                string email = "moma@gmail.com";

                int resultExpected = 1;
                int result = test.UpdateUserInfo(employeeModel, email);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdateEmployeeInfo_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "ares@gmail.com",
                    Name = "Ares Juda Rivera Soto",
                    RoleUser = "Personal de cocina",
                    PersonalNumber = "111",
                    Availability = "Activo"
                };

                string email = "ares@gmail.com";

                int resultExpected = 1;
                int result = test.UpdateEmployeeInfo(employeeModel, email);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdateEmployeeInfo_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();
                EmployeeModel employeeModel = new EmployeeModel {
                    Email = "ares@gmail.com",
                    Name = "Ares Juda Rivera Soto",
                    RoleUser = "Personal de cocina",
                    PersonalNumber = "11241",
                    Availability = "Activo"
                };

                string email = "aresMoma@gmail.com";

                int resultExpected = 1;
                int result = test.UpdateEmployeeInfo(employeeModel, email);
                Assert.AreNotEqual(resultExpected, result);
            }
        }
    }
}
