using KomalliEmployee.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KomalliTest.Employee {
    [TestClass]
    public class LogbookControllerTest {
        [TestMethod]
        public void AddComment_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

                LogbookModel logbook = new LogbookModel() {
                    IdCommentary = 8,
                    Date = DateTime.Now,
                    Commentary = "Se reportan 5 comidas sobrantes",
                    NoPersonalEmployee = "11"
                };
                int resultExpected = 1;
                int result = test.AddCommentary(logbook);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddComment_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

                LogbookModel logbook = new LogbookModel() {
                    IdCommentary = 12,
                    Date = DateTime.Now,
                    Commentary = "Se reportan 5 comidas sobrantes",
                    NoPersonalEmployee = "0"
                };
                int resultExpected = -1;
                int result = test.AddCommentary(logbook);
                Assert.AreEqual(resultExpected, result);
            }
        }
    }
}