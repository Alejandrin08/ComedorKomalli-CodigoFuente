using Microsoft.VisualStudio.TestTools.UnitTesting;
using KomalliEmployee.Model;
using System;
using System.Transactions;
using System.Data.Entity.Core;
using System.Collections.Generic;
using KomalliEmployee.Model.Utilities;
using KomalliServer;


namespace KomalliTest.Employee
{
    [TestClass]
    public class IngredientControllerTest
    {
        [TestMethod]
        public void AddIngredient_Sucessfull(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                IngredientModel ingredient = new IngredientModel {
                    KeyIngredient = "SABR0256",
                    NameIngredient = "Sabritas Originales",
                    BarCode = "1326784590214",
                    Quantity = "10",
                    Measurement = TypeQuantity.Unidades,
                    ReplenishmentDate = null
                };
                int resultExpected = 1;
                int result = test.AddIngredient(ingredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddIngredient_Failed(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                IngredientModel ingredient = new IngredientModel {
                    KeyIngredient = "TOMA8861",
                    NameIngredient = "Tomate Bola",
                    BarCode = null,
                    Quantity = "20",
                    Measurement = TypeQuantity.Kg,
                    ReplenishmentDate = null
                };
                int resultExpected = -1;
                int result = test.AddIngredient(ingredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsNameIngredientExisting_Sucessfull(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                string nameIngredient = "Tomate Bola";
                int resultExpected = 1;
                int result = test.IsNameIngredientExisting(nameIngredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsNameIngredientExisting_Failed(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                string nameIngredient = "Pay de limon";
                int resultExpected = 0;
                int result = test.IsNameIngredientExisting(nameIngredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsBarCodeExisting_Sucessfull(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                string barCodeIngredient = "1234567890123";
                int resultExpected = 1;
                int result = test.IsBarCodeExisting(barCodeIngredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsBarCodeExisting_Failed()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                string barCodeIngredient = "1326784632576";
                int resultExpected = 0;
                int result = test.IsBarCodeExisting(barCodeIngredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ModifyIngredients_Sucessfull()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                List<IngredientModel> listIngredientsModified = new List<IngredientModel>();
                IngredientModel ingredientTest1 = new IngredientModel();
                ingredientTest1.KeyIngredient = "TOMA8861";
                ingredientTest1.NameIngredient = "Tomate Bola";
                ingredientTest1.Measurement = TypeQuantity.Kg;
                ingredientTest1.BarCode = null;
                ingredientTest1.Quantity = "30";
                listIngredientsModified.Add(ingredientTest1);
                int resultExpected = 1;
                int result = test.ModifyIngredients(listIngredientsModified);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void ModifyIngredients_Failed()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                List<IngredientModel> listIngredientsModified = new List<IngredientModel>();
                IngredientModel ingredientTest1 = new IngredientModel();
                ingredientTest1.KeyIngredient = "TOMA8860";
                ingredientTest1.NameIngredient = "Tomate Bola";
                ingredientTest1.Measurement = TypeQuantity.Kg;
                ingredientTest1.BarCode = null;
                ingredientTest1.Quantity = "30";
                listIngredientsModified.Add(ingredientTest1);
                int resultExpected = 1;
                int result = test.ModifyIngredients(listIngredientsModified);
                Assert.AreEqual(resultExpected, result);
            }
        }


    }
}
