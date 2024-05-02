using KomalliClient.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomalliTest.Client {
    [TestClass]
    public class FoodControllerTest {
        [TestMethod]
        public void GetFoodsPerSection_Sucessfull() {
            KomalliClient.Controller.FoodController test = new KomalliClient.Controller.FoodController();
            string sectionName = "Otros";

            var foodOrderExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "Otr1",
                    Name = "Porción de tortillas",
                    Price = 6,
                    Section = "Otros",
                    Image = "https://th.bing.com/th/id/OIP.jzGX4i_xMrBxyNL-zE-MrQAAAA?rs=1&pid=ImgDetMain"
                },
                new FoodModel {
                    KeyCard = "Otr2",
                    Name = "Palomitas",
                    Price = 17,
                    Section = "Otros",
                    Image = "https://th.bing.com/th/id/OIP.wMrBoRLKD8L-h7IhiMqc2wAAAA?rs=1&pid=ImgDetMain"
                }, 
                new FoodModel {
                    KeyCard = "Otr3",
                    Name = "Microondas",
                    Price = 4,
                    Section = "Otros",
                    Image = "https://www.sudamerisclub.com.py/wp-content/uploads/2021/01/MICROONDAS-300x300.jpg"
                }, 
                new FoodModel {
                    KeyCard = "Otr4",
                    Name = "Desechable",
                    Price = 3,
                    Section = "Otros",
                    Image = "https://th.bing.com/th/id/OIP.4OHibAxrAcTFtUN9iju3UQAAAA?rs=1&pid=ImgDetMain"
                },
                new FoodModel {
                    KeyCard = "Otr5",
                    Name = "Pieza de pan",
                    Price = 9,
                    Section = "Otros",
                    Image = "https://th.bing.com/th/id/OIP.iycF0KShS0YJA6htrk5jkwAAAA?rs=1&pid=ImgDetMain"
                }
            };

            var result = test.GetFoodsPerSection(sectionName);
            
            Assert.AreEqual(foodOrderExpected.Count, result.Count);
            for (int i = 0; i < foodOrderExpected.Count; i++) {
                Assert.AreEqual(foodOrderExpected[i].KeyCard, result[i].KeyCard);
                Assert.AreEqual(foodOrderExpected[i].Name, result[i].Name);
                Assert.AreEqual(foodOrderExpected[i].Price, result[i].Price);
                Assert.AreEqual(foodOrderExpected[i].Section, result[i].Section);
                Assert.AreEqual(foodOrderExpected[i].Image, result[i].Image);
            }
        }

        [TestMethod]
        public void GetFoodsPerSection_Failed() {
            KomalliClient.Controller.FoodController test = new KomalliClient.Controller.FoodController();
            string sectionName = "Otros";

            var foodOrderExpected = new List<FoodModel> {
                new FoodModel {
                    KeyCard = "O1",
                    Name = "Tortillas",
                    Price = 20,
                    Section = "Otro",
                    Image = "tortillas.png"
                },
                new FoodModel {
                    KeyCard = "O2",
                    Name = "Palomita",
                    Price = 20,
                    Section = "Otro",
                    Image = "palomitas.png"
                }, 
                new FoodModel {
                    KeyCard = "O3",
                    Name = "Micro",
                    Price = 10,
                    Section = "Otro",
                    Image = "MICROONDAS.jpg"
                }, 
                new FoodModel {
                    KeyCard = "O4",
                    Name = "Desechabl",
                    Price = 1,
                    Section = "Otro",
                    Image = "desechable.png"
                }
            };

            var result = test.GetFoodsPerSection(sectionName);
            
            Assert.AreNotEqual(foodOrderExpected.Count, result.Count);
            for (int i = 0; i < foodOrderExpected.Count; i++) {
                Assert.AreNotEqual(foodOrderExpected[i].KeyCard, result[i].KeyCard);
                Assert.AreNotEqual(foodOrderExpected[i].Name, result[i].Name);
                Assert.AreNotEqual(foodOrderExpected[i].Price, result[i].Price);
                Assert.AreNotEqual(foodOrderExpected[i].Section, result[i].Section);
                Assert.AreNotEqual(foodOrderExpected[i].Image, result[i].Image);
            }
        }
    }
}
