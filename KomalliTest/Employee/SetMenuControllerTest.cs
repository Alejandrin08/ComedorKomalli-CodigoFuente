using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KomalliTest.Employee
{
    [TestClass]
    public class SetMenuControllerTest
    {

        [TestMethod]
        public void AddSetMenu_Sucessfull()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModel = new SetMenuModel
                {
                    KeySetMenu = "Des0183",
                    DateMenu = DateTime.Now,
                    Starter = "papas",
                    MainFood = "milanesa",
                    SideDish = "arroz",
                    Salad = "tomate",
                    Drink = "horchata",
                    PriceStudent = 30,
                    Pricegeneral = 50,
                    Type = TypeMenu.Comida,
                };

                int resultExpected = 1;
                int result = test.AddSetMenu(setMenuModel);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddSetMenu_Failed()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModel = new SetMenuModel
                {
                    KeySetMenu = "Des002",
                    DateMenu = DateTime.Now,
                    Starter = "papas",
                    MainFood = "milanesa",
                    SideDish = "arroz",
                    Salad = "tomate",
                    Drink = "horchata",
                    PriceStudent = 30,
                    Pricegeneral = 50,
                    Type = TypeMenu.Comida,
                };

                int resultExpected = 1;
                int result = test.AddSetMenu(setMenuModel);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ExistingTypeMenu_Sucessfull()
        {
            
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModel = new SetMenuModel
                {
                    KeySetMenu = "Com001",
                    DateMenu =  new DateTime (2024, 04, 28),                     
                    Type = TypeMenu.Comida,
                };

                int resultExpected = 1;
                int result = test.ExistingTypeMenu(setMenuModel);
                Assert.AreEqual(resultExpected, result);
            
        }

        [TestMethod]
        public void ExistingTypeMenu_Failed()
        {

            KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
            SetMenuModel setMenuModel = new SetMenuModel
            {
                KeySetMenu = "Des0183",
                DateMenu = new DateTime(2023, 05, 07),
                Type = TypeMenu.Comida,
            };

            int resultExpected = 1;
            int result = test.ExistingTypeMenu(setMenuModel);
            Assert.AreNotEqual(resultExpected, result);

        }

        [TestMethod]
        public void ExistingMenu_Sucessfull()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModelExpected = new SetMenuModel
                {
                    KeySetMenu = "Com001",
                    DateMenu = new DateTime(2024, 04, 28),
                    Starter = "Crema de coliflor",
                    MainFood = "Rajas Poblanas",
                    SideDish = "Arroz Rojo",
                    Salad = "Ensalada de lechuga",
                    Drink = "Agua de pepino",
                    PriceStudent = 30,
                    Pricegeneral = 50,
                    Type = TypeMenu.Comida,
                };
                SetMenuModel result = test.ExistingMenu(setMenuModelExpected.DateMenu, setMenuModelExpected.Type.ToString());
                Assert.IsNotNull(result);
                Assert.AreEqual(setMenuModelExpected.KeySetMenu, result.KeySetMenu);
                Assert.AreEqual(setMenuModelExpected.Starter, result.Starter);
                Assert.AreEqual(setMenuModelExpected.MainFood, result.MainFood);
                Assert.AreEqual(setMenuModelExpected.SideDish, result.SideDish);
                Assert.AreEqual(setMenuModelExpected.Salad, result.Salad);
                Assert.AreEqual(setMenuModelExpected.Drink, result.Drink);
                Assert.AreEqual(setMenuModelExpected.PriceStudent, result.PriceStudent);
                Assert.AreEqual(setMenuModelExpected.Pricegeneral, result.Pricegeneral);
                Assert.AreEqual(setMenuModelExpected.Type, result.Type);
            }
        }

        [TestMethod]
        public void ModifySetMenu_Sucessfull()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModel = new SetMenuModel
                {
                    KeySetMenu = "Com001",
                    DateMenu = DateTime.Now,
                    Starter = "papas",
                    MainFood = "milanesa",
                    SideDish = "arroz",
                    Salad = "tomate",
                    Drink = "horchata",
                    PriceStudent = 30,
                    Pricegeneral = 50,
                    Type = TypeMenu.Comida,
                };

                int resultExpected = 1;
                int result = test.ModifyMenu(setMenuModel);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ModifySetMenu_Failed()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                SetMenuModel setMenuModel = new SetMenuModel
                {
                    KeySetMenu = "Com100",
                    DateMenu = DateTime.Now,
                    Starter = "papas",
                    MainFood = "milanesa",
                    SideDish = "arroz",
                    Salad = "tomate",
                    Drink = "horchata",
                    PriceStudent = 30,
                    Pricegeneral = 50,
                    Type = TypeMenu.Comida,
                };

                int resultExpected = 1;
                int result = test.ModifyMenu(setMenuModel);
                Assert.AreNotEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void DeleteSetMenu_Sucessfull()
        {
                KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
                string keySetMenu = "DES793";
                int resultExpected = 1;
                int result = test.DeleteMenu(keySetMenu);
                Assert.AreEqual(resultExpected, result);
        }

        [TestMethod]
        public void DeleteSetMenu_Failed()
        {
            KomalliEmployee.Controller.SetMenuController test = new KomalliEmployee.Controller.SetMenuController();
            string keySetMenu = "Com005";
            int resultExpected = 1;
            int result = test.DeleteMenu(keySetMenu);
            Assert.AreNotEqual(resultExpected, result);

        }

    }
}
