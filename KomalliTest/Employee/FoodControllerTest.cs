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
    public class FoodControllerTest {


        [TestMethod]
        public void GetFoodsPerSection_Sucessfull() {
            KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
            string sectionName = "Otros";

            var foodOrderExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "Otr1",
                    Name = "Porción de tortillas",
                    Price = 6,
                    Section = "Otros"},
                new FoodModel {
                    KeyCard = "Otr2",
                    Name = "Palomitas",
                    Price = 17,
                    Section = "Otros"},
                new FoodModel {
                    KeyCard = "Otr3",
                    Name = "Microondas",
                    Price = 4,
                    Section = "Otros"},
                new FoodModel {
                    KeyCard = "Otr4",
                    Name = "Desechable",
                    Price = 3,
                    Section = "Otros"},
                new FoodModel {
                    KeyCard = "Otr5",
                    Name = "Pieza de pan",
                    Price = 9,
                    Section = "Otros"
                }
            };

            var result = test.GetFoodsPerSection(sectionName);

            Assert.AreEqual(foodOrderExpected.Count, result.Count);
            for (int i = 0; i < foodOrderExpected.Count; i++) {
                Assert.AreEqual(foodOrderExpected[i].KeyCard, result[i].KeyCard);
                Assert.AreEqual(foodOrderExpected[i].Name, result[i].Name);
                Assert.AreEqual(foodOrderExpected[i].Price, result[i].Price);
                Assert.AreEqual(foodOrderExpected[i].Section, result[i].Section);

            }
        }

        [TestMethod]
        public void GetFoodsPerSection_Failed() {
            KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
            string sectionName = "Otros";

            var foodOrderExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "O1",
                    Name = "Tortillas",
                    Price = 20,
                    Section = "Otro",

                },
                new FoodModel {
                    KeyCard = "O2",
                    Name = "Palomita",
                    Price = 20,
                    Section = "Otro",

                },
                new FoodModel {
                    KeyCard = "O3",
                    Name = "Micro",
                    Price = 10,
                    Section = "Otro",

                },
                new FoodModel {
                    KeyCard = "O4",
                    Name = "Desechabl",
                    Price = 1,
                    Section = "Otro",

                }
            };

            var result = test.GetFoodsPerSection(sectionName);

            Assert.AreNotEqual(foodOrderExpected.Count, result.Count);
            for (int i = 0; i < foodOrderExpected.Count; i++) {
                Assert.AreNotEqual(foodOrderExpected[i].KeyCard, result[i].KeyCard);
                Assert.AreNotEqual(foodOrderExpected[i].Name, result[i].Name);
                Assert.AreNotEqual(foodOrderExpected[i].Price, result[i].Price);
                Assert.AreNotEqual(foodOrderExpected[i].Section, result[i].Section);

            }
        }

        [TestMethod]
        public void  GetKeyMenu_Sucessfull() {
            KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();

            var foodOrdersExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "Des49"
                },
                
            };

            var foodOrdersResult = test.GetKeyMenu();

            Assert.AreEqual(foodOrdersExpected.Count, foodOrdersResult.Count);
            for (int i = 0; i < foodOrdersExpected.Count; i++) {
                Assert.AreEqual(foodOrdersExpected[i].KeyCard, foodOrdersResult[i].KeyCard);
               
            }

        }

        [TestMethod]
        public void GetKeyMenu_Failed() {
            KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();

            var foodOrdersExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "Com49"
                },

            };

            var foodOrdersResult = test.GetKeyMenu();

            Assert.AreEqual(foodOrdersExpected.Count, foodOrdersResult.Count);
            for (int i = 0; i < foodOrdersExpected.Count; i++) {
                Assert.AreNotEqual(foodOrdersExpected[i].KeyCard, foodOrdersResult[i].KeyCard);

            }

        }

        [TestMethod]
        public void RegistryOrderMenuCard_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
                FoodModel foodOrderModel = new FoodModel {
                    KeyCard = "Otr1",
                    Quantity = 1,
                    Subtotal = 6,
                };

                string idFoodOrder = "Caj8939";

                int resultExpected = 1;
                int result = test.RegistryOrderMenuCard(foodOrderModel, idFoodOrder);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void RegistryOrderMenuCard_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
                FoodModel foodOrderModel = new FoodModel {
                    KeyCard = "Otr1",
                    Quantity = 1,
                    Subtotal = 6,
                };

                string idFoodOrder = "Caj0";

                int resultExpected = 1;
                int result = test.RegistryOrderMenuCard(foodOrderModel, idFoodOrder);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void RegistryOrderMenu_Sucessfull() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
                FoodModel foodOrderModel = new FoodModel {
                    KeyCard = "Des49",
                    Quantity = 1,
                    Subtotal = 30,
                };

                string idFoodOrder = "Caj8939";

                int resultExpected = 1;
                int result = test.RegistryOrderMenu(foodOrderModel, idFoodOrder);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void RegistryOrderMenu_Failed() {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })) {
                KomalliEmployee.Controller.FoodController test = new KomalliEmployee.Controller.FoodController();
                FoodModel foodOrderModel = new FoodModel {
                    KeyCard = "Des49",
                    Quantity = 1,
                    Subtotal = 30,
                };

                string idFoodOrder = "Caj0";

                int resultExpected = 1;
                int result = test.RegistryOrderMenu(foodOrderModel, idFoodOrder);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

    }
}
