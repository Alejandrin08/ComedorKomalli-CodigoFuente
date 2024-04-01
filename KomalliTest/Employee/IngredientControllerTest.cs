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

                Ingredient ingredient = new Ingredient();
                ingredient.KeyIngredient = "SABR0256";
                ingredient.NameIngredient = "Sabritas Originales";
                ingredient.Barcode = "1326784590214";
                ingredient.Quantity = "10";
                ingredient.Measurement = "Unidades";
                int resultExpected = 1;
                int result = test.AddIngredient(ingredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void AddIngredient_Failed(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                Ingredient ingredient = new Ingredient();
                ingredient.KeyIngredient = "TOMA0541";
                ingredient.NameIngredient = "Tomate En Bola";
                ingredient.Barcode = null;
                ingredient.Quantity = "20";
                ingredient.Measurement = "Kg";
                int resultExpected = 2;
                int result = test.AddIngredient(ingredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsNameIngredientExisting_Sucessfull(){
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted })){
                KomalliEmployee.Controller.IngredientController test = new KomalliEmployee.Controller.IngredientController();

                string nameIngredient = "Tomate En Bola";
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

                string barCodeIngredient = "1326784590214";
                int resultExpected = 1;
                int result = test.IsBarCodeExisting(barCodeIngredient);
                Assert.AreEqual(resultExpected, result);
            }
        }

        [TestMethod]
        public void IsBarCodeExisting_failed()
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

    }
}
