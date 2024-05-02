using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Model;
using KomalliEmployee.Views.Usercontrols;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KomalliEmployee.Contracts;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Page {
        public MakeOrder() {
            InitializeComponent();
            ShowFood();
            SingletonClass.Instance.SelectedFoods.CollectionChanged += SelectedFoodsCollectionChanged;
        }

        private void SelectedFoodsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            lstSelectedFoods.Items.Clear();

            //WrapPanel wrapPanel = FindWrapPanel(this);

            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                FoodDetails foodDetails = new FoodDetails();
                foodDetails.BindData(food);
                foodDetails.Margin = new Thickness(3);
                lstSelectedFoods.Items.Add(foodDetails);
                /*
                if (food.IsSelected) {
                    foreach (var child in wrapPanel.Children) {
                        if (child is FoodUserControl foodControl && foodControl.Food.Name == food.Name) {
                            foodControl.IsEnabled = false;
                            foodControl.Opacity = 0.5;
                        }
                    }
                }*/
            }
            //txtbTotalPrice.Text = "$" + CalculateTotalPrice().ToString();
        }

        private void ShowFood() {
            ShowFoodPerSection("Paquetes", wrpPackage, grdPackage);
            ShowFoodPerSection("Antojitos", wrpAntojitos, grdAntojitos);
            ShowFoodPerSection("Tortas", wrpSandwich, grdSandwich);
            ShowFoodPerSection("Bebidas", wrpDrinks, grdDrinks);
            ShowFoodPerSection("Postres", wrpDesserts, grdDesserts);
            ShowFoodPerSection("Guarniciones", wrpGarnish, grdGarnish);
            ShowFoodPerSection("Otros", wrpOthers, grdOthers);
            ShowMenu();
        }


        private void ShowFoodPerSection(string section, WrapPanel wrapPanel, Grid grid) {
            var foods = new FoodController().GetFoodsPerSection(section);

            if (foods != null) {
                var wrapPanelFood = new WrapPanel { Orientation = Orientation.Horizontal };

                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData();
                    foodControl.Margin = new Thickness(10);
                    wrapPanel.Children.Add(foodControl);
                }
                grid.Children.Add(wrapPanelFood);
            } else {
                App.ShowMessageError("No se pudo recuperar el menú de bebidas", "Error");
            }
        }

        private void ShowMenu() {

            var wrapPanelFood = new WrapPanel { Orientation = Orientation.Horizontal };
            var foodControl = new MenuUserControl ();
            var foodControl1 = new MenuUserControl();
            foodControl.BindData("Desayuno");
            foodControl1.BindData("Comida");
            foodControl.Margin = new Thickness(10);
            foodControl1.Margin = new Thickness(10);
            wrpMenu.Children.Add(foodControl);
            wrpMenu.Children.Add(foodControl1);
            grdContent.Children.Add(wrapPanelFood);
        }
    }
}
