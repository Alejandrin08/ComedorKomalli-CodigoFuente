using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Model.Validations;
using KomalliEmployee.Views.Windows;
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

        bool navigate = false;
        public MakeCollection() {
            InitializeComponent();
            InitializeDishOrder();
            InitializeTotal();
            DataContext = new FoodOrderModel();
            var nameValidation = new NameValidationRule();
            NameValidationRule.ErrorTextBlock = txbNameValidationMessage;
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
                if (e.Uri != allowedNavigationUri && navigate == false) {
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



        private void InitializeDishOrder() {
            List<DishOrderModel> dishOrderModel;
            DishOrderController dishOrderControler = new DishOrderController();
            dishOrderModel = dishOrderControler.ConsultDishOrder(SingletonClass.Instance.IdFoodOrderSelected);
            dgDishOrder.ItemsSource = dishOrderModel;
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
            navigate = true;
            FoodOrderController foodOrderController = new FoodOrderController();
            FoodOrderModel foodOrderModel = new FoodOrderModel {
                Status = "Pagado",
                Change = CalculateChange(),
                ClientName = txtClientName.Text,
            };
            
            int resultUpdateFoodOrder = foodOrderController.UpdateFoodOrder(foodOrderModel, SingletonClass.Instance.IdFoodOrderSelected);
            if (resultUpdateFoodOrder > 0) {
                App.ShowMessageInformation("Registro exitoso", "Actualización de pedido");
                TicketReport ticketReport = new TicketReport();
                ticketReport.Width = 350; 
                ticketReport.Height = 450;
                ticketReport.Closed += (s, args) => { 
                    this.NavigationService.Navigate(new MakeOrder());
                };
                ticketReport.ShowDialog();
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
            if(e.Key == Key.Enter && !string.IsNullOrEmpty(txtReceive.Text)) {
                int receive = int.Parse(txtReceive.Text);
                int total = SingletonClass.Instance.TotalOrder;
                if (receive < total) {
                    txbReceiveValidationMessage.Text = "Campo inválido";
                    txbReceiveValidationMessage.Visibility = Visibility.Visible;
                } else {
                    if (!string.IsNullOrEmpty(txtReceive.Text)) {
                        txtReceive.IsEnabled = false;
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
            if (SingletonClass.Instance.IdFoodOrderSelected.StartsWith("Kio")) {
                txtClientName.Text = SingletonClass.Instance.UserNameOrder;
                txtClientName.IsEnabled = false;
            } else {
                txtClientName.IsEnabled = true;
            }
        }

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            if (SingletonClass.Instance.IdFoodOrderSelected.StartsWith("Kio")) {
                this.NavigationService.GoBack();
            } else {
                MessageBoxResult result = App.ShowMessageBoxButton("¿Está seguro de eliminar la orden?", "Confirmación");
                if (result == MessageBoxResult.Yes) {
                    FoodOrderController foodOrderController = new FoodOrderController();
                    if (foodOrderController.DeleteOrder(SingletonClass.Instance.IdFoodOrderSelected) > 0) {

                        SingletonClass.Instance.SelectedFoods.Clear();
                        this.NavigationService.GoBack();
                    }
                }
            }
        }
    }
}
