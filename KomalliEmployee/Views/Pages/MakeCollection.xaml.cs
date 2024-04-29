using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Model.Validations;
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

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para MakeCollection.xaml
    /// </summary>
    public partial class MakeCollection : Page {
        public MakeCollection() {
            InitializeComponent();
            InitializeListOfUsers();
            InitializeTotal();
            DataContext = new FoodOrderModel();
            var nameValidation = new NameValidationRule();
            NameValidationRule.ErrorTextBlock = txbNameValidationMessage;
        }

        private void InitializeListOfUsers() {
            List<DishOrderModel> users;
            DishOrderController employeeControler = new DishOrderController();
            users = employeeControler.ConsultDishOrder(SingletonClass.Instance.IdFoodOrderSelected);
            dgDishOrder.ItemsSource = users;
        }

        private void InitializeTotal() {
            FoodOrderModel foodOrderModel = new FoodOrderModel();
            FoodOrderController foodOrderController = new FoodOrderController();
            foodOrderModel = foodOrderController.GetTotalAndNameFromOrder(SingletonClass.Instance.IdFoodOrderSelected);
            SingletonClass.Instance.UserNameOrder = foodOrderModel.ClientName;
            SingletonClass.Instance.TotalOrder = foodOrderModel.Total;
            tbkTotal.Text = "$ " + foodOrderModel.Total.ToString() +".00";
            tbkTotalGreen.Text = "$ " +  foodOrderModel.Total.ToString() + ".00";
        }

        private void TextChangedValidateReceive(object sender, TextChangedEventArgs e) {
            if (string.IsNullOrEmpty(txtReceive.Text)) {
                txbReceiveValidationMessage.Visibility = Visibility.Visible;
                btnConfirmOrder.IsEnabled = false;
            } else {
                txbReceiveValidationMessage.Visibility = Visibility.Collapsed;
            }

        }       

        private void PreviewTextInputOnlyNumber(object sender, TextCompositionEventArgs e) {
            foreach (char character in e.Text) {
                if (!char.IsDigit(character)) {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void TextChangedValidateName(object sender, TextChangedEventArgs e) {
            bool isEmpty = !string.IsNullOrEmpty(txtClientName.Text);
            if (!isEmpty) {
                txbNameValidationMessage.Visibility = Visibility.Visible;
                txbNameValidationMessage.Text = "Campo obligatorio.";
                btnConfirmOrder.IsEnabled = false;
            } else {
                if (!Validation.GetHasError(txtClientName)) {
                    btnConfirmOrder.IsEnabled = true;
                } else {
                    btnConfirmOrder.IsEnabled = false;
                }
            }

        }

        private void ClickConfirmOrder(object sender, RoutedEventArgs e) {            
            FoodOrderController foodOrderController = new FoodOrderController();
            FoodOrderModel foodOrderModel = new FoodOrderModel {
                Status = "Pagado",
                Change = CalculateChange(),
                ClientName = txtClientName.Text,
            };
            int resultUpdateFoodOrder = foodOrderController.UpdateFoodOrder(foodOrderModel, SingletonClass.Instance.IdFoodOrderSelected);
            if (resultUpdateFoodOrder > 0) {
                App.ShowMessageInformation("Registro exitoso", "Actualización de pedido");
                this.NavigationService.GoBack();
            } else {
                App.ShowMessageWarning("Error al actualizar los datos del pedido", "Actualización de pedido");
            }
        }

        private int CalculateChange() {
            int receive = int.Parse(txtReceive.Text);
            int total = SingletonClass.Instance.TotalOrder;
            int change = receive - total;
            return change;
        }

        private void KeyDownEnterReceive(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                int receive = int.Parse(txtReceive.Text);
                int total = SingletonClass.Instance.TotalOrder;
                if (receive < total) {
                    txbReceiveValidationMessage.Text = "Campo inválido";
                    txbReceiveValidationMessage.Visibility = Visibility.Visible;
                } else {
                    if (!string.IsNullOrEmpty(txtReceive.Text)) {
                        tbkChange.Visibility = Visibility.Visible;
                        txtChange.Text = CalculateChange().ToString();
                        txtChange.Visibility = Visibility.Visible;
                        tbkMxn.Visibility = Visibility.Visible;
                        EnabledTxtClientName();
                    } else {
                        txbReceiveValidationMessage.Visibility = Visibility.Visible;
                    }

                }                
            }            
        }

        private void EnabledTxtClientName() {
            tbkName.Visibility = Visibility.Visible;
            txtClientName.Visibility = Visibility.Visible;
            if (!SingletonClass.Instance.UserNameOrder.StartsWith("Kio")) {
                txtClientName.Text = SingletonClass.Instance.UserNameOrder;
                txtClientName.IsEnabled = false;
            } else {
                txtClientName.IsEnabled = true;
            }
        }

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
