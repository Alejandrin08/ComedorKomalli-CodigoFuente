using KomalliEmployee.Model;
using KomalliEmployee.Views.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    Commentary = "Se reportan 5 comidas sobrantes",
                    NoPersonalEmployee = "111",
                    Section = "Otro"
                };
                int resultExpected = -1;
                int result = test.AddCommentary(logbook);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void GetEmployeeComments_Sucessfull() {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

            var logbookExpected = new List<LogbookModel> {
                new LogbookModel {
                    Date = DateTime.ParseExact("21/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se han roto cinco platos para sopa y se perdieron dos cucharas",
                    Section = "Incidencia"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se han comprado tres paquetes de servilletas",
                    Section = "General"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se han caducado dos paquetes de jugos",
                    Section = "Merma"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Presentarse a una reunion informal el dia viernes 25 de mayo 10 minutos antes de la hora de entrada",
                    Section = "Otro"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Hoy sobraron 5 menus de comida de los chicos becados",
                    Section = "General"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "El dia viernes se tiene programado hacer crema de zanahoria, necesitaremos mas mantequilla",
                    Section = "General"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se debe comprar mas paquetes de servilletas",
                    Section = "General"

                },

                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "El dia jueves faltaran por su desayuno dos becados ",
                    Section = "General"

                },
                
            };
            string NoPersonalEmployee = "111";
            var logbookResult = test.GetEmployeeComments(NoPersonalEmployee);
            Assert.AreEqual(logbookExpected.Count, logbookResult.Count);
            for (int i = 0; i < logbookExpected.Count; i++) {
                Assert.AreEqual(logbookExpected[i].Date, logbookResult[i].Date);
                Assert.AreEqual(logbookExpected[i].Commentary, logbookResult[i].Commentary);
                Assert.AreEqual(logbookExpected[i].Section, logbookResult[i].Section);
           
            }

        }

        [TestMethod]
        public void GetEmployeeComments_Failed() {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();
            var logbookExpected = new List<LogbookModel> {
                new LogbookModel {
                    Date = DateTime.ParseExact("22/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se quedo un paquete de queso, se queda almacenado el refrigerador, util para otro menú.",
                    Section = "Otro"

                },
                new LogbookModel {
                    Date = DateTime.ParseExact("21/05/24", "dd/MM/yy", CultureInfo.InvariantCulture),
                    Commentary = "Se han roto cinco platos.",
                    Section = "Merma"

                },

            };
            string NoPersonalEmployee = "111";
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
        public void DeleteCommentary_Sucessfull() {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();
            int idComment = 92;
            int resultExpected = 1;
            int result = test.DeleteCommentary(idComment);
            Assert.AreEqual(resultExpected, result);

        }

        [TestMethod]
        public void DeleteCommentary_Failed() {
            KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();
            int idComment = 93;
            int resultExpected = -1;
            int result = test.DeleteCommentary(idComment);
            Assert.AreNotEqual(resultExpected, result);
        }

        [TestMethod]
        public void UpdateComment_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

                LogbookModel logbookModel = new LogbookModel {
                    Section = "General",
                    Commentary = "Se han comprado dos paquetes de servilletas"
                };
                int idComment = 41;
                int result = 1;
                int resultExpected = test.UpdateComment(logbookModel, idComment);
                Assert.AreEqual(resultExpected, result);
            }


        }

        [TestMethod]
        public void UpdateComment_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.LogbookController test = new KomalliEmployee.Controller.LogbookController();

                LogbookModel logbookModel = new LogbookModel {
                    Section = "General",
                    Commentary = "Se han comprado dos paquetes de servilletas"
                };

                int idComment = 45;

                int resultExpected = 1;
                int result = test.UpdateComment(logbookModel, idComment);
                Assert.AreNotEqual(resultExpected, result);
            }


        }
    }
}