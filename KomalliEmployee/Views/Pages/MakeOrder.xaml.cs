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
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.WebControls;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Page {

        bool navigate = false; 
        public MakeOrder() {
            InitializeComponent();
            GetKeysMenus();
            ShowFood();
            SingletonClass.Instance.SelectedFoods.CollectionChanged += SelectedFoodsCollectionChanged;
            this.Loaded += MakeOrderPage_Loaded;
            this.Loaded += MakeOrderWindow_Loaded;
            
        }

        private void MakeOrderWindow_Loaded(object sender, RoutedEventArgs e) {
            
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null) {
                parentWindow.Closing += ParentWindow_Closing;
            }
        }

        private void ParentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (SingletonClass.Instance.SelectedFoods.Count > 0) {
                    MessageBoxResult result = App.ShowMessageBoxButton("Si sale cancelará la orden, ¿Está seguro de cancelar la orden?", "Confirmación");
                    if (result == MessageBoxResult.Yes) {
                        SingletonClass.Instance.SelectedFoods.Clear();
                        SingletonClass.Instance.IsFoodValidSelected = false;
                    } else {
                        e.Cancel = true; 
                    }
            }
        }

        private void MakeOrderPage_Loaded(object sender, RoutedEventArgs e) {
            if (this.NavigationService != null) {
                this.NavigationService.Navigating += NavigationService_Navigating;
            }
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e) {
            if (SingletonClass.Instance.SelectedFoods.Count > 0) {

                Uri allowedNavigationUri = new Uri("MakeCollection.xaml", UriKind.Relative);
                if(e.Uri != allowedNavigationUri && navigate == false ) {
                    MessageBoxResult result = App.ShowMessageBoxButton("Si sale cancelará la orden, ¿Está seguro de cancelar la orden?", "Confirmación");
                    if (result == MessageBoxResult.Yes) {
                        SingletonClass.Instance.SelectedFoods.Clear();
                        SingletonClass.Instance.IsFoodValidSelected = false;
                    } else {
                        e.Cancel = true; 
                    }
                }
                
            }
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
            EvaluateSelectedFood();
            tbkSubtotalOrder.Text ="Subtotal: $ " + subtotal.ToString();            
        }

        public void EvaluateSelectedFood() {
            var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(foodModel => foodModel.Section == "Antojitos" || foodModel.Section == "Tortas" || foodModel.KeyCard == "Pk7");
            if (selectedFood != null) {
                SingletonClass.Instance.IsFoodValidSelected = true;
            } else {
                SingletonClass.Instance.IsFoodValidSelected = false;
            }
            EnabledEnlargePackage();
        }

        public void EnabledEnlargePackage() {
            for (int i = 0; i < 7; i++) {
                wrpPackage.Children.RemoveAt(0);
            }
             ShowFoodPerSection("Paquetes", wrpPackage, grdPackage);            
        }
        

        private bool IsBreakfastTime() {
            DateTime now = DateTime.Now;
            DateTime breakfastStart = DateTime.Today.AddHours(8);
            DateTime breakfastEnd = DateTime.Today.AddHours(12).AddMinutes(30);
            bool isBreakfastTime = now >= breakfastStart && now <= breakfastEnd;
            return isBreakfastTime;
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
                var wrapPanelFood = new WrapPanel { Orientation = System.Windows.Controls.Orientation.Horizontal };
                foreach (var food in foods) {
                    var foodControl = new FoodUserControl { Food = food };
                    foodControl.BindData(food);
                    if(section == "Paquetes") {
                        if (food.KeyCard != "Pk7" && SingletonClass.Instance.IsFoodValidSelected == false) {
                            foodControl.IsEnabled = false;
                            foodControl.Opacity = 0.7;
                        }
                    }
                    foodControl.Margin = new Thickness(10);
                    wrapPanel.Children.Add(foodControl);
                }
                grid.Children.Add(wrapPanelFood);
                
            } else {
                App.ShowMessageError("No se pudo recuperar el menú de " + section , "Error");
            }
        }

        private void ShowMenu() {
            var wrapPanelFood = new WrapPanel { Orientation = System.Windows.Controls.Orientation.Horizontal };
            SetMenu("Desayuno");
            SetMenu("Comida");            
            grdContent.Children.Add(wrapPanelFood);
        }

        private void SetMenu(string typeMenu) {
            var foodControl = new MenuUserControl();
            foodControl.BindData(typeMenu);
            foodControl.Margin = new Thickness(10);
            bool isMenuAvailable = IsMenuAvailable(typeMenu);
            if (!isMenuAvailable) {
                foodControl.IsEnabled = false;
                foodControl.Opacity = 0.7;
            }
            wrpMenu.Children.Add(foodControl);
        }

        private bool IsMenuAvailable(string typeMenu) {
            bool isMenuAvailable = false;
            switch (typeMenu) {
                case "Desayuno":
                    isMenuAvailable = IsBreakfastAvailable();
                    break;
                case "Comida":
                    isMenuAvailable = IsFoodAvailable();
                    break;
                default:
                   break; 
            }
            return isMenuAvailable;
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

        public void GetKeysMenus() {
            FoodController foodController = new FoodController();
            var menu = foodController.GetKeyMenu();
            foreach (var food in menu) {
                if (food.KeyCard.StartsWith("DES")) {
                    SingletonClass.Instance.keyBreakfast = food.KeyCard;
                } else if (food.KeyCard.StartsWith("COM")) {
                    SingletonClass.Instance.KeyMeal = food.KeyCard;
                }
            }
        }

        public bool IsBreakfastAvailable() {
            bool isValid = false;
            if(IsBreakfastTime() == true && SingletonClass.Instance.keyBreakfast != null) {
                isValid = true;
            }
            return isValid;
        }

        public bool IsFoodAvailable() {
            bool isValid = false;            
            if (IsBreakfastTime() == false && SingletonClass.Instance.KeyMeal != null) {
                isValid = true;
            }
            return isValid;
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
            navigate = true; 
            btnCharge.IsEnabled = false;

            FoodOrderController foodOrderController = new FoodOrderController();
            SingletonClass.Instance.NewIdFoodOrder = foodOrderController.MakeIdFoodOrder();
            
            int resultRegistryOrder = foodOrderController.RegistryOrder(GetInfoOrder());

            if (resultRegistryOrder > 0 ) {
                int resultRegisterOrderDetails = RegisterOrderDetails();
                if (resultRegisterOrderDetails > 0 ) {
                    SingletonClass.Instance.IdFoodOrderSelected = SingletonClass.Instance.NewIdFoodOrder;
                    ShowPageMakeCollection();
                } else {
                    App.ShowMessageError("No se pudieron registrar los detalles del pedido", "Error al registrar el pedido");
                }
            } else {
                App.ShowMessageError("No se pudo registrar el pedido", "Error al registrar el pedido");
            }
        }

        private int RegisterOrderDetails() {
            FoodController foodController = new FoodController();
            int resultRegistryOrderMenu = 1;
            int resultRegistryOrderMenuCard = 1;
            int resultUpdateStock = 1;
            var order = SingletonClass.Instance.SelectedFoods;
            foreach (var food in order) {
                FoodModel foodModel = new FoodModel() {
                    KeyCard = food.KeyCard,
                    Quantity = food.Quantity,
                    Price = food.Price,
                    Subtotal = food.Quantity * food.Price,
                };
                if (food.KeyCard.StartsWith("DES") || food.KeyCard.StartsWith("COM")) {
                    resultRegistryOrderMenu = foodController.RegistryOrderMenu(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    resultUpdateStock = foodController.UpdateStockSetMenu(foodModel.KeyCard, foodModel.Quantity);
                } else {
                    resultRegistryOrderMenuCard = foodController.RegistryOrderMenuCard(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    resultUpdateStock = foodController.UpdateStockMenuCard(foodModel.KeyCard, foodModel.Quantity);
                }
            }
            return (resultRegistryOrderMenu > 0 && resultRegistryOrderMenuCard > 0 && resultUpdateStock > 0) ? 1 : 0;
        }

        public void ShowPageMakeCollection() {
            MakeCollection makeCollection = new MakeCollection();
            this.NavigationService.Navigate(makeCollection);
        }
    }
}
