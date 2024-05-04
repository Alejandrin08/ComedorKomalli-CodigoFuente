using KomalliEmployee.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliTest.Employee {
    [TestClass]
    public class MenuControllerTest {

        [TestMethod]
        public void GetBreakfastOfTheDay_Sucessfull() {
            KomalliEmployee.Controller.MenuController menuController = new KomalliEmployee.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = "sopa",
                MainFood = "carne",
                SideDish = "nsalada",
                Salad = "pepino",
                Drink = "horchata"
            };
            DateTime date = new DateTime(2024, 05, 04 ); 
            var result = menuController.GetBreakfastOfTheDay(date);

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
            KomalliEmployee.Controller.MenuController menuController = new KomalliEmployee.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = "Sopa de fideo",
                MainFood = "Pollo en salsa",
                SideDish = "Arroz",
                Salad = "Ensalada de lechuga",
                Drink = "Agua de jamaica"
            };
            DateTime date = new DateTime(2024, 05, 03);
            var result = menuController.GetBreakfastOfTheDay(date);

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
            KomalliEmployee.Controller.MenuController menuController = new KomalliEmployee.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = "Crema de coliflor",
                MainFood = "Rajas Poblanas",
                SideDish = "Arroz Rojo",
                Salad = "Ensalada de lechuga",
                Drink = "Agua de pepino"
            };

            DateTime date = new DateTime(2024, 04, 28);

            var result = menuController.GetFoodOfTheDay(date);

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
            KomalliEmployee.Controller.MenuController menuController = new KomalliEmployee.Controller.MenuController();
            MenuModel menuModel = new MenuModel() {
                Starter = null,
                MainFood = "Huevo en salsa",
                SideDish = "Frijoles",
                Salad = "Fruta de temporadas",
                Drink = "Agua de horchata"
            };

            DateTime date = new DateTime(2024, 05, 04);
            var result = menuController.GetFoodOfTheDay(date);

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
