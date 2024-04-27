using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;

namespace KomalliTest.Employee {
    [TestClass]
    public class CashCutControllerTest {
        [TestMethod]
        public void GetLastInitialBalance_Sucessfull() {
            KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

            int? result = 1000;
            int? resultExpected = test.GetLastInitialBalance();
            Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void GetLastInitialBalance_Failed() {
            KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

            int? result = 0;
            int? resultExpected = test.GetLastInitialBalance();
            Assert.AreNotEqual(resultExpected, result);
        }

        [TestMethod]
        public void RegisterCashCutNextDay_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

                int initialBalance = 500;
                int result = 1;
                int resultExpected = test.RegisterCashCutNextDay(initialBalance);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void RegisterCashCutNextDay_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

                int initialBalance = 500;
                int result = -1;
                int resultExpected = test.RegisterCashCutNextDay(initialBalance);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdateDailyCashCut_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

                int? initialBalance = 1000;
                int result = 1;
                int resultExpected = test.UpdateDailyCashCut(initialBalance);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void UpdateDailyCashCut_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.CashCutController test = new KomalliEmployee.Controller.CashCutController();

                int? initialBalance = 2000;
                int result = -1;
                int resultExpected = test.UpdateDailyCashCut(initialBalance);
                Assert.AreNotEqual(resultExpected, result);
            }
        }
    }
}
