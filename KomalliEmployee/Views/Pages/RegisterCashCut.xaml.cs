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
            txtbCashCutDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtbInitialBalance.Text = new CashCutController().GetLastInitialBalance().ToString();
            txtbTotalEntries.Text = new FoodOrderController().CalculateTotalDailyEntries().ToString();
            txtbTotalDepartures.Text = new FoodOrderController().CalculateTotalDailyChange().ToString();
            CalculateBalance();
        }

        private void ClicRegisterCashCut(object sender, RoutedEventArgs e) {
            CashCutController cashCutController = new CashCutController();
            if (txtbxInitialBalance.Text != "") {
                if (cashCutController.RegisterCashCutNextDay(int.Parse(txtbInitialBalance.Text)) > 0) {
                    App.ShowMessageInformation("Corte de caja registrado correctamente", "Corte de caja");
                }
            } else {
                App.ShowMessageWarning("Por favor, ingrese el saldo inicial", "Ingrese saldo");
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
            if (txtbTotalEntries.Text != "0" || txtbTotalDepartures.Text != "0") {
                int initialBalance = int.Parse(txtbInitialBalance.Text);
                int totalEntries = int.Parse(txtbTotalEntries.Text);
                int totalDepartures = int.Parse(txtbTotalDepartures.Text);
                int balance = initialBalance + totalEntries - totalDepartures;
                txtbBalance.Text = "$" + balance.ToString();
            }
        }
    }
}
