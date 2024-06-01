using KomalliEmployee.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliTest.Employee {
    [TestClass]
    public class DishOrderControllerTest {

        [TestMethod]
        public void ConsultDishOrderFromCard_Sucesssfull() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 27,
                    DishName = "Chilaquiles",
                    NumberDishes = 2,
                    Total = 54
                },
                new DishOrderModel {
                    UnitPrice = 20,
                    DishName = "Enfrijoladas 1/2 porción",
                    NumberDishes = 1,
                    Total = 20
                },
                new DishOrderModel {
                    UnitPrice = 20,
                    DishName = "Enmoladas 1/2 porción",
                    NumberDishes = 3,
                    Total = 60
                },
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrderFromCard(idFoodOrder);

            Assert.AreEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }

        [TestMethod]
        public void ConsultDishOrderFromCard_Failed() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 29,
                    DishName = "Chilaquiles Rojos",
                    NumberDishes = 28,
                    Total = 44
                },
                
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrderFromCard(idFoodOrder);

            Assert.AreNotEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreNotEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreNotEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreNotEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreNotEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }

        [TestMethod]
        public void ConsultDishOrderFromMenu_Sucesssfull() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 30,
                    DishName = "Comida",
                    NumberDishes = 1,
                    Total = 30
                },
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrderFromMenu(idFoodOrder);

            Assert.AreEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }

        [TestMethod]
        public void ConsultDishOrderFromMenu_Failed() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 50,
                    DishName = "Desayuno",
                    NumberDishes = 2,
                    Total = 100
                },
               
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrderFromMenu(idFoodOrder);

            Assert.AreEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreNotEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreNotEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreNotEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreNotEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }

        [TestMethod]
        public void ConsultDishOrder_Sucesssfull() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 27,
                    DishName = "Chilaquiles",
                    NumberDishes = 2,
                    Total = 54
                },
                new DishOrderModel {
                    UnitPrice = 20,
                    DishName = "Enfrijoladas 1/2 porción",
                    NumberDishes = 1,
                    Total = 20
                },
                new DishOrderModel {
                    UnitPrice = 20,
                    DishName = "Enmoladas 1/2 porción",
                    NumberDishes = 3,
                    Total = 60
                },
                new DishOrderModel {
                    UnitPrice = 30,
                    DishName = "Comida",
                    NumberDishes = 1,
                    Total = 30
                },
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrder(idFoodOrder);

            Assert.AreEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }

        [TestMethod]
        public void ConsultDishOrder_Failed() {
            KomalliEmployee.Controller.DishOrderController test = new KomalliEmployee.Controller.DishOrderController();

            var dishesCardExpected = new List<DishOrderModel> {
                new DishOrderModel {
                    UnitPrice = 22,
                    DishName = "Chilaquiles Verdes",
                    NumberDishes = 1,
                    Total = 84
                },
                new DishOrderModel {
                    UnitPrice = 28,
                    DishName = "Enfrijoladas",
                    NumberDishes = 4,
                    Total = 90
                },
                new DishOrderModel {
                    UnitPrice = 50,
                    DishName = "Desauno",
                    NumberDishes = 2,
                    Total = 100
                },
            };

            string idFoodOrder = "Kio003";

            var dishesCardResult = test.ConsultDishOrder(idFoodOrder);

            Assert.AreNotEqual(dishesCardExpected.Count, dishesCardResult.Count);
            for (int i = 0; i < dishesCardExpected.Count; i++) {
                Assert.AreNotEqual(dishesCardExpected[i].UnitPrice, dishesCardResult[i].UnitPrice);
                Assert.AreNotEqual(dishesCardExpected[i].DishName, dishesCardResult[i].DishName);
                Assert.AreNotEqual(dishesCardExpected[i].NumberDishes, dishesCardResult[i].NumberDishes);
                Assert.AreNotEqual(dishesCardExpected[i].Total, dishesCardResult[i].Total);
            }

        }
    }
}
