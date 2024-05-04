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

        public void SelectedFoodsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            EnableButtonCollection();
            lstSelectedFoods.Items.Clear();
            int subtotal = 0;

            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                subtotal = subtotal + food.Quantity * food.Price;
                var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(foodModel => foodModel.Name == food.Name);
                FoodDetails foodDetails = new FoodDetails();
                foodDetails.BindData(food);
                foodDetails.Margin = new Thickness(3);
                foodDetails.UpdateQuantity(selectedFood.Quantity);
                foodDetails.UpdateSubtotal(selectedFood.Quantity * selectedFood.Price);
                lstSelectedFoods.Items.Add(foodDetails);
            }
            tbkMnakegOrder.Text ="Subtotal: $ " + subtotal.ToString();
        }

        private void EnableButtonCollection() {
            if (SingletonClass.Instance.SelectedFoods.Count > 0) {
                btnCharge.IsEnabled = true;
            } else {
                btnCharge.IsEnabled = false;
            }
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
                    foodControl.BindData(food);
                    foodControl.Margin = new Thickness(10);
                    wrapPanel.Children.Add(foodControl);
                }
                grid.Children.Add(wrapPanelFood);
            } else {
                App.ShowMessageError("No se pudo recuperar el menú de " + section , "Error");
            }
        }

        private void ShowMenu() {
            var wrapPanelFood = new WrapPanel { Orientation = Orientation.Horizontal };
            SetMenu("Desayuno");
            SetMenu("Comida");            
            grdContent.Children.Add(wrapPanelFood);
        }

        private void SetMenu(string typeMenu) {
            var foodControl = new MenuUserControl();
            foodControl.BindData(typeMenu);
            foodControl.Margin = new Thickness(10);
            wrpMenu.Children.Add(foodControl);
        }

        private void MouseDownCancelOrder(object sender, MouseButtonEventArgs e) {
            if (SingletonClass.Instance.SelectedFoods.Count > 0) {
                MessageBoxResult result = App.ShowMessageBoxButton("¿Está seguro de cancelar la orden?", "Confirmación");
                if (result == MessageBoxResult.Yes) {
                    SingletonClass.Instance.SelectedFoods.Clear();
                    btnCharge.IsEnabled = false;
                }
            }

        }

        private FoodOrderModel GetInfoOrder() {
            var order = SingletonClass.Instance.SelectedFoods;
            int quantity = 0;
            int total = 0;
            foreach (var food in order) {
                quantity += food.Quantity;
                total += food.Quantity * food.Price;
            }
            FoodOrderModel foodOrderModel = new FoodOrderModel() {
                Total = total,
                NumberDishes = quantity,
                IdFoodOrder = SingletonClass.Instance.NewIdFoodOrder
            };
            return foodOrderModel;
        }

        private void ClickMakeCharge(object sender, RoutedEventArgs e) {
            btnCharge.IsEnabled = false;

            FoodOrderController foodOrderController = new FoodOrderController();
            SingletonClass.Instance.NewIdFoodOrder = foodOrderController.MakeIdFoodOrder();
            
            int resultRegistryOrder = foodOrderController.RegistryOrder(GetInfoOrder());

            if (resultRegistryOrder > 0 ) {
                FoodController foodController = new FoodController();
                int resultRegistryOrderMenu = 1;
                int resultRegistryOrderMenuCard = 1;
                var order = SingletonClass.Instance.SelectedFoods;
                foreach (var food in order) {                    
                    FoodModel foodModel = new FoodModel() {
                        KeyCard = food.KeyCard,
                        Quantity = food.Quantity,
                        Price = food.Price,
                        Subtotal = food.Quantity * food.Price,
                    };
                    if (food.KeyCard.StartsWith("Des") || food.KeyCard.StartsWith("Com")) {
                        resultRegistryOrderMenu = foodController.RegistryOrderMenu(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    } else {
                        resultRegistryOrderMenuCard = foodController.RegistryOrderMenuCard(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    }
                }
                if (resultRegistryOrderMenu > 0 && resultRegistryOrderMenuCard > 0) {
                    SingletonClass.Instance.IdFoodOrderSelected = SingletonClass.Instance.NewIdFoodOrder;
                    ShowPageMakeCollection();
                } else {
                    App.ShowMessageError("No se pudieron registrar los detalles del pedido", "Error al registrar el pedido");
                }

            } else {
                App.ShowMessageError("No se pudo registrar el pedido", "Error al registrar el pedido");
            }
        }

        public void ShowPageMakeCollection() {
            MakeCollection makeCollection = new MakeCollection();
            this.NavigationService.Navigate(makeCollection);
        }
    }
}
