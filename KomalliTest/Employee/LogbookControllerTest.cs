using KomalliEmployee.Model;
using KomalliEmployee.Views.Pages;
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
                    Date = DateTime.Now,
                    Section = "Merma",
                    Commentary = "Se reportan 5 comidas sobrantes",
                    NoPersonalEmployee = "777",
                    Section = "Merma"  
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
                    Date = DateTime.Now,
                    Commentary = "Hola jsjsj",
                    NoPersonalEmployee = "111",
                    Section = "Otro"
                };
                int resultExpected = -1;
                int result = test.AddCommentary(logbook);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void GetEmployeeComments_Succesful() {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

            var logbookExpected = new List<LogbookModel> {
                new LogbookModel {
                    Date = '08-05-2024',
                    Commentary = "Se reubico la zona donde se encuentran las verduras",
                    Section = "Otro"
                    
                },
                new LogbookModel
                {
                    Date = '08-05-2024',
                    Commentary = "No hay botellas aguas dentro del inventario",
                    Section = "Incidencia"
                    
                },

            };
            string NoPersonalEmployee = "11";
            var logbookResult = test.GetEmployeeComments(NoPersonalEmployee);
            Assert.AreEqual(logbookExpected.Count, logbookResult.Count);
            for (int i = 0; i < logbookExpected.Count; i++)
            {
                Assert.AreEqual(logbookExpected[i].Date, logbookResult[i].Date);
                Assert.AreEqual(logbookExpected[i].Commentary, logbookResult[i].Commentary);
                Assert.AreEqual(logbookExpected[i].Section, logbookResult[i].Section);
           
            }

        }

        [TestMethod]
        public void GetEmployeeComments_Failed()
        {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

            var logbookExpected = new List<LogbookModel> {
                new LogbookModel {
                    Date = '08-05-2024',
                    Commentary = "Se reubico la zona donde se encuentran las verduras",
                    Section = "Otro"

                },
                new LogbookModel
                {
                    Date = '08-05-2024',
                    Commentary = "No hay botellas aguas dentro del inventario",
                    Section = "Incidencia"

                },

            };

            string NoPersonalEmployee = "11";

            var logbookResult = test.GetEmployeeComments(NoPersonalEmployee);

            Assert.AreNotEqual(logbookExpected.Count, logbookResult.Count);
            for (int i = 0; i < logbookExpected.Count; i++)
            {
                Assert.AreNotEqual(logbookExpected[i].Date, logbookResult[i].Date);
                Assert.AreNotEqual(logbookExpected[i].Commentary, logbookResult[i].Commentary);
                Assert.AreNotEqual(logbookExpected[i].Section, logbookResult[i].Section);
            }

        }

        [TestMethod]
        public void GetCommentId()
        {
            
        }
    }
}