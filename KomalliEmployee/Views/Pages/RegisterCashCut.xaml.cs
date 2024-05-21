using KomalliEmployee.Controller;
using KomalliEmployee.Model;
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
    /// Lógica de interacción para RegisterCashCut.xaml
    /// </summary>
    public partial class RegisterCashCut : Page {

        public RegisterCashCut() {
            InitializeComponent();
            InitializeFoodOrder();

            CashCutController cashCutController = new CashCutController();
            FoodOrderController foodOrderController = new FoodOrderController();

            tbkCashCutDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            int? initialBalance = cashCutController.GetLastInitialBalance();
            int totalEntries = foodOrderController.CalculateTotalDailyEntries();
            int totalDepartures = foodOrderController.CalculateTotalDailyChange();
            int balance = initialBalance.Value + totalEntries - totalDepartures;

            tbkInitialBalance.Text = initialBalance.ToString();
            tbkTotalEntries.Text = totalEntries.ToString();
            tbkTotalDepartures.Text = totalDepartures.ToString();
            
            CashCutModel cashCutModel = new CashCutModel() {
                InitialBalance = (int)initialBalance,
                TotalEntries = totalEntries,
                TotalExits = totalDepartures,
                Balance = balance
            };
            int resultUpdate = cashCutController.UpdateDailyCashCut(cashCutModel);
            if (resultUpdate < 0) {
                App.ShowMessageError("Error al actualizar el corte de caja", "Corte de caja");
            }

            CalculateBalance();
        }

        private void ClicRegisterCashCut(object sender, RoutedEventArgs e) {
            CashCutController cashCutController = new CashCutController();
            int minimumInitialBalance = int.Parse(txtInitialBalance.Text);
            int balance = int.Parse(tbkBalance.Text);
          
            if (minimumInitialBalance >= 400 && minimumInitialBalance < balance) {
                if (cashCutController.RegisterCashCutNextDay(int.Parse(txtInitialBalance.Text)) > 0) {
                    App.ShowMessageInformation("Corte de caja registrado correctamente", "Corte de caja");
                } else {
                    App.ShowMessageWarning("Error al registrar el corte de caja", "Corte de caja");
                }
            } else {
               App.ShowMessageWarning("Por favor, ingrese un saldo inicial valido y mayor o igual a $400", "Saldo inicial no valido");
            } 
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e) {
            foreach (char character in e.Text) {
                if (!char.IsDigit(character)) {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InitializeFoodOrder() {
            List<FoodOrderModel> foodOrders;
            FoodOrderController foodOrderController = new FoodOrderController();
            foodOrders = foodOrderController.DailyFoodOrders();
            dgFoodOrders.ItemsSource = foodOrders;
        }

        private void CalculateBalance() {
            int initialBalance = int.Parse(tbkInitialBalance.Text);
            int totalEntries = int.Parse(tbkTotalEntries.Text);
            int totalDepartures = int.Parse(tbkTotalDepartures.Text);
            int balance = initialBalance + totalEntries - totalDepartures;
            tbkBalance.Text = balance.ToString();     
        }

        private void TextChangedValidateInitialBalance(object sender, TextChangedEventArgs e) {
            if (string.IsNullOrEmpty(txtInitialBalance.Text)) {
                btnMakeCashCut.IsEnabled = false;
            } else {
                btnMakeCashCut.IsEnabled = true;
            }
        }

        private void KeyDownRegisterCashCut(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter && btnMakeCashCut.IsEnabled) {
                ClicRegisterCashCut(sender, e);
            }
        }
    }
}
