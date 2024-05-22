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

        

        private void ClickGenerateOrder(object sender, RoutedEventArgs e) {
            FoodOrderController foodOrderController = new FoodOrderController();
            SingletonClass.Instance.NewIdFoodOrder = foodOrderController.MakeIdFoodOrder();
            var order = SingletonClass.Instance.SelectedFoods;
            int quantity = 0;
            int total = 0;
            foreach (var food in order)
            {
                quantity += food.Quantity;
                total += food.Quantity * food.Price;

            }

            FoodOrderModel foodOrderModel = new FoodOrderModel()
            {
                Total = total,
                NumberDishes = quantity,
                IdFoodOrder = SingletonClass.Instance.NewIdFoodOrder,
                ClientName = txtName.Text
            };
            int result = foodOrderController.RegistryOrder(foodOrderModel);

            if (result == 1)
            {
                FoodController foodController = new FoodController();
                int result2 = 1;
                int result3 = 1;
                foreach (var food in order)
                {
                    quantity += food.Quantity;
                    total += food.Subtotal;
                    FoodModel foodModel = new FoodModel()
                    {
                        KeyCard = food.KeyCard,
                        Quantity = food.Quantity,
                        Price = food.Price,
                        Subtotal = food.Quantity * food.Price
                    };

                    if (food.KeyCard.StartsWith("Des") || food.KeyCard.StartsWith("Com"))
                    {
                        result3 = foodController.RegistryOrderMenu(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    }
                    else
                    {
                        result2 = foodController.RegistryOrderMenuCard(foodModel, SingletonClass.Instance.NewIdFoodOrder);
                    }
                }
            }
            else
            {
                App.ShowMessageError("MURIO", "DIE");
            }
        }
    }
}

