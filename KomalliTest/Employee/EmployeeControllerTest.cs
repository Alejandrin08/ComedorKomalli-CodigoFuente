using Microsoft.VisualStudio.TestTools.UnitTesting;
using KomalliEmployee.Model;
using System;
using System.Transactions;
using System.Data.Entity.Core;
using System.Collections.Generic;

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
    }
}
