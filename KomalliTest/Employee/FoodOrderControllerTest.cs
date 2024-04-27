using KomalliEmployee.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomalliTest.Employee {
    [TestClass]
    public class FoodOrderControllerTest {
        [TestMethod]
        public void CalculateTotalDailyChange_Sucessfull() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();
            int resultExpected = 103;
            int result = test.CalculateTotalDailyChange();
            Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void CalculateTotalDailyChange_Failed() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();
            int resultExpected = 110;
            int result = test.CalculateTotalDailyChange();
            Assert.AreNotEqual(resultExpected, result);
        }

        [TestMethod]
        public void CalculateTotalDailyEntries_Sucessfull() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();
            int resultExpected = 197;
            int result = test.CalculateTotalDailyEntries();
            Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void CalculateTotalDailyEntries_Failed() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();
            int resultExpected = 200;
            int result = test.CalculateTotalDailyEntries();
            Assert.AreNotEqual(resultExpected, result);
        }

        [TestMethod]
        public void DailyFoodOrders_Sucessfull() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();

            var foodOrdersExpected = new List<FoodOrderModel> {
                new FoodOrderModel {
                    IdFoodOrder = "003",
                    Date = DateTime.Today,
                    ClientName = "Alejandro",
                    NumberDishes = 2,
                    Total = 60
                },
                new FoodOrderModel {
                    IdFoodOrder = "004",
                    Date = DateTime.Today,
                    ClientName = "Paloma",
                    NumberDishes = 1,
                    Total = 30
                }, 
                new FoodOrderModel {
                    IdFoodOrder = "005",
                    Date = DateTime.Today,
                    ClientName = "Ares",
                    NumberDishes = 3,
                    Total = 80
                }, 
                new FoodOrderModel {
                    IdFoodOrder = "006",
                    Date = DateTime.Today,
                    ClientName = "Miriam",
                    NumberDishes = 1,
                    Total = 27
                }
            };

            var foodOrdersResult = test.DailyFoodOrders();

            Assert.AreEqual(foodOrdersExpected.Count, foodOrdersResult.Count);
            for (int i = 0; i < foodOrdersExpected.Count; i++) {
                Assert.AreEqual(foodOrdersExpected[i].IdFoodOrder, foodOrdersResult[i].IdFoodOrder);
                Assert.AreEqual(foodOrdersExpected[i].Date, foodOrdersResult[i].Date);
                Assert.AreEqual(foodOrdersExpected[i].ClientName, foodOrdersResult[i].ClientName);
                Assert.AreEqual(foodOrdersExpected[i].NumberDishes, foodOrdersResult[i].NumberDishes);
                Assert.AreEqual(foodOrdersExpected[i].Total, foodOrdersResult[i].Total);
            }
        }

        [TestMethod]
        public void DailyFoodOrders_Failed() {
            KomalliEmployee.Controller.FoodOrderController test = new KomalliEmployee.Controller.FoodOrderController();

            var foodOrdersExpected = new List<FoodOrderModel> {
                new FoodOrderModel {
                    IdFoodOrder = "001",
                    Date = DateTime.Today.AddDays(2),
                    ClientName = "Alejandrin",
                    NumberDishes = 1,
                    Total = 608
                },
                new FoodOrderModel {
                    IdFoodOrder = "002",
                    Date = DateTime.Today.AddDays(2),
                    ClientName = "Palomita",
                    NumberDishes = 10,
                    Total = 301
                },
                new FoodOrderModel {
                    IdFoodOrder = "003",
                    Date = DateTime.Today.AddDays(2),
                    ClientName = "Aresiño",
                    NumberDishes = 31,
                    Total = 8
                }
            };

            var foodOrdersResult = test.DailyFoodOrders();

            Assert.AreNotEqual(foodOrdersExpected.Count, foodOrdersResult.Count);
            for (int i = 0; i < foodOrdersExpected.Count; i++) {
                Assert.AreNotEqual(foodOrdersExpected[i].IdFoodOrder, foodOrdersResult[i].IdFoodOrder);
                Assert.AreNotEqual(foodOrdersExpected[i].Date, foodOrdersResult[i].Date);
                Assert.AreNotEqual(foodOrdersExpected[i].ClientName, foodOrdersResult[i].ClientName);
                Assert.AreNotEqual(foodOrdersExpected[i].NumberDishes, foodOrdersResult[i].NumberDishes);
                Assert.AreNotEqual(foodOrdersExpected[i].Total, foodOrdersResult[i].Total);
            }
        }
    }
}
