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
        public void GetUserRule_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                UserRole userRoleExpected = UserRole.Gerente;
                UserRole userRole = test.GetUserRule(email);
                Assert.AreEqual(userRoleExpected, userRole);
            }
        }

        [TestMethod]
        public void GetUserRule_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                UserRole userRoleExpected = UserRole.Gerente;
                UserRole userRole = test.GetUserRule(email);
                Assert.AreEqual(userRoleExpected, userRole);
            }
        }

        [TestMethod]
        public void ValidateUser_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
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
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted})) {
                KomalliEmployee.Controller.EmployeeController test = new KomalliEmployee.Controller.EmployeeController();

                string email = "ares@gmail.com";
                string password = "123";
                int resultExpected = -1;
                int result = test.ValidateUser(email, password);
                Assert.AreEqual(resultExpected, result);
            }
        }
    }
}
