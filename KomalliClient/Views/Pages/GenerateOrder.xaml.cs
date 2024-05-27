using KomalliClient.Model;
using KomalliClient.Model.Utilities;
using KomalliClient.Views.UserControls;
using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using KomalliClient.Controller;
using System.Collections.Specialized;

namespace KomalliClient.Views.Pages
{
    /// <summary>
    /// Interaction logic for GenerateOrder.xaml
    /// </summary>
    public partial class GenerateOrder : Page {
        public GenerateOrder() {
            InitializeComponent();
            ShowDataOrder();
            txtName.TextChanged += TextChanged;
        }



        private void ShowDataOrder() {
            lstOrderData.Items.Clear();
            foreach (FoodModel food in SingletonClass.Instance.SelectedFoods) {
                var selectedFood = SingletonClass.Instance.SelectedFoods.FirstOrDefault(foodModel => foodModel.Name == food.Name);
                OrderDataUserControl orderData = new OrderDataUserControl();
                orderData.SetData(food);
                orderData.Margin = new Thickness(3);
                orderData.UpdateQuantity(selectedFood.Quantity);
                orderData.UpdatePrice(selectedFood.Price);
                lstOrderData.Items.Add(orderData);

            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e) {
            string texto = txtName.Text;
            Regex regex = new Regex("^[a-zA-Z ]+$");
            if (!string.IsNullOrEmpty(txtName.Text) && regex.IsMatch(texto)) {
                btnConfirm.IsEnabled = true;
            }
            else {
                btnConfirm.IsEnabled = false;
            }
        }

        private int RegisterOrderDetails() {
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
                }
                else {
                    resultRegistryOrderMenuCard = foodController.RegistryOrderMenuCard(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                }
            }
            return (resultRegistryOrderMenu > 0 && resultRegistryOrderMenuCard > 0) ? 1 : 0;
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
                IdFoodOrder = SingletonClass.Instance.NewIdFoodOrder,
                ClientName = txtName.Text
            };
            return foodOrderModel;
        }

        private void ClickGenerateOrder(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            SingletonClass.Instance.NewIdFoodOrder = foodOrderController.MakeIdFoodOrder();

            int resultRegistryOrder = foodOrderController.RegistryOrder(GetInfoOrder());

            if (resultRegistryOrder > 0) {
                int resultRegisterOrderDetails = RegisterOrderDetails();
            }
            else {
                App.ShowMessageError("No se pudo registrar el pedido", "Error al registrar el pedido");
            }
        }
    }
}

