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
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "alexsandermarin@outlook.com";
                string newPassword = "123Ale";
                int resultExpected = 1;
                int result = test.UpdatePassword(email, newPassword);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdatePassword_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ale@gmail.com";
                string newPassword = "123Ale";
                int resultExpected = 0;
                int result = test.UpdatePassword(email, newPassword);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ValidateUser_Successful() {
            var test = new KomalliEmployee.Controller.EmployeeController();

            var rolesTest = new Dictionary<UserRole, int> { { UserRole.Cajero, 1 } };
            var result = test.ValidateUser("alexsandermarin@outlook.com", "12345Ale");

            Assert.AreEqual(rolesTest.Count, result.Count);

            foreach (var kvp in rolesTest) {
                int expectedValue;
                bool keyExists = result.TryGetValue(kvp.Key, out expectedValue);

                Assert.IsTrue(keyExists, $"El rol '{kvp.Key}' no está presente en el resultado");
                Assert.AreEqual(kvp.Value, expectedValue, $"La disponibilidad para el rol '{kvp.Key}' no coincide");
            }
        }

        [TestMethod]
        public void ValidateUser_Failed() {
            var test = new KomalliEmployee.Controller.EmployeeController();

            var rolesTest = new Dictionary<UserRole, int> { { UserRole.Invalid, 0 } };
            var result = test.ValidateUser("alexsandermarin@outlook.com", "12345Ale");
            
            Assert.AreEqual(rolesTest.Count, result.Count);

            foreach (var kvp in rolesTest) {
                int expectedValue;
                bool keyExists = result.TryGetValue(kvp.Key, out expectedValue);

                Assert.IsFalse(keyExists, $"El rol '{kvp.Key}' está presente en el resultado, pero no debería estarlo");
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
                    Email = "dmysela@gmail.com",
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
                    Role = UserRole.Cajero,
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
                    Email = "dmysela@gmail.com",
                    Name = "Pancho villa",                    
                    Role = UserRole.Cajero,
                    PersonalNumber = "1",
                    Password = "5464komalli",
                };

                int resultExpected = 1;
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

            int resultExpected = -1;
            int result = test.ValidateEmail(email);
            Assert.AreNotEqual(resultExpected, result);

        }

        [TestMethod]
        public void ValidateNoPersonal_Sucessfull() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string personalNumber = "43";

            int resultExpected = -1;
            int result = test.ValidateNoPersonal(personalNumber);
            Assert.AreEqual(resultExpected, result);

        }

        [TestMethod]
        public void ValidateNoPersonal_Failed() {

            KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

            string personalNumber = "1";

            int resultExpected = -1;
            int result = test.ValidateNoPersonal(personalNumber);
            Assert.AreNotEqual(resultExpected, result);

        }


    }
}
