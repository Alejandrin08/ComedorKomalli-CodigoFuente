using KomalliClient.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomalliTest.Client {
    [TestClass]
    public class MenuControllerTest {
        [TestMethod]
        public void GetBreakfastOfTheDay_Sucessfull() {
            KomalliClient.Controller.MenuController menuController = new KomalliClient.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = null,
                MainFood = "Huevo en salsa",
                SideDish = "Frijoles",
                Salad = "Fruta de temporada",
                Drink = "Agua de horchata"
            };

            var result = menuController.GetBreakfastOfTheDay();

            if (result != null) {
                Assert.AreEqual(menuModel.Starter, result.Starter);
                Assert.AreEqual(menuModel.MainFood, result.MainFood);
                Assert.AreEqual(menuModel.SideDish, result.SideDish);
                Assert.AreEqual(menuModel.Salad, result.Salad);
                Assert.AreEqual(menuModel.Drink, result.Drink);
            }
        }

        [TestMethod]
        public void GetBreakfastOfTheDay_Failed() {
            KomalliClient.Controller.MenuController menuController = new KomalliClient.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = "Sopa de fideo",
                MainFood = "Pollo en salsa",
                SideDish = "Arroz",
                Salad = "Ensalada de lechuga",
                Drink = "Agua de jamaica"
            };

            var result = menuController.GetBreakfastOfTheDay();

            if (result != null) {
                Assert.AreNotEqual(menuModel.Starter, result.Starter);
                Assert.AreNotEqual(menuModel.MainFood, result.MainFood);
                Assert.AreNotEqual(menuModel.SideDish, result.SideDish);
                Assert.AreNotEqual(menuModel.Salad, result.Salad);
                Assert.AreNotEqual(menuModel.Drink, result.Drink);
            }
        }

        [TestMethod]
        public void GetFoodOfTheDay_Sucessfull() {
            KomalliClient.Controller.MenuController menuController = new KomalliClient.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = "Crema de coliflor",
                MainFood = "Rajas Poblanas",
                SideDish = "Arroz Rojo",
                Salad = "Ensalada de lechuga",
                Drink = "Agua de pepino"
            };

            var result = menuController.GetFoodOfTheDay();

            if (result != null) {
                Assert.AreEqual(menuModel.Starter, result.Starter);
                Assert.AreEqual(menuModel.MainFood, result.MainFood);
                Assert.AreEqual(menuModel.SideDish, result.SideDish);
                Assert.AreEqual(menuModel.Salad, result.Salad);
                Assert.AreEqual(menuModel.Drink, result.Drink);
            }
        }

        [TestMethod]
        public void GetFoodOfTheDay_Failed() {
            KomalliClient.Controller.MenuController menuController = new KomalliClient.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = null,
                MainFood = "Huevo en salsa",
                SideDish = "Frijoles",
                Salad = "Fruta de temporadas",
                Drink = "Agua de horchata"
            };

            var result = menuController.GetFoodOfTheDay();

            if (result != null) {
                Assert.AreNotEqual(menuModel.Starter, result.Starter);
                Assert.AreNotEqual(menuModel.MainFood, result.MainFood);
                Assert.AreNotEqual(menuModel.SideDish, result.SideDish);
                Assert.AreNotEqual(menuModel.Salad, result.Salad);
                Assert.AreNotEqual(menuModel.Drink, result.Drink);
            }
        }
    }
}
