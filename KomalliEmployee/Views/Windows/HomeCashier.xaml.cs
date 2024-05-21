using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Pages;
using Microsoft.Reporting.Map.WebForms.BingMaps;
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

namespace KomalliEmployee.Views.Windows {
    /// <summary>
    /// Lógica de interacción para HomeCashier.xaml
    /// </summary>
    public partial class HomeCashier : Window {
        public HomeCashier() {
            InitializeComponent();
            txbNameUser.Text = SingletonClass.Instance.UserName;
        }

        private void ClickSubMenuCash(object sender, RoutedEventArgs e) {
            if (stpSubMenuCash.Visibility == Visibility.Collapsed) {
                stpSubMenuCash.Visibility = Visibility.Visible;
            } else {
                stpSubMenuCash.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickMakeOrder(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/MakeOrder.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickConsultOrders(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/ConsultFoodOrdersKiosko.xaml", UriKind.RelativeOrAbsolute));

        }

        private void ClickCashRegisterCutting(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/RegisterCashCut.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickSubMenuLogbook(object sender, RoutedEventArgs e) {
            if (stpSubMenuLogbook.Visibility == Visibility.Collapsed) {
                stpSubMenuLogbook.Visibility = Visibility.Visible;
            } else {
                stpSubMenuLogbook.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickAddLogbook(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/Logbook.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickComments(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/BlogCommentsxaml.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickClose(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ClickRestore(object sender, RoutedEventArgs e) {
            if (WindowState == WindowState.Normal) {
                WindowState = WindowState.Maximized;
            } else {
                WindowState = WindowState.Normal;
            }

        }

        private void ClickMinimize(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void MouseDownLogout(object sender, MouseButtonEventArgs e) {
            MessageBoxResult result = App.ShowMessageBoxButton("¿Está seguro de cerrar la sesión", "Confirmación");
            if (result == MessageBoxResult.Yes) {
                Login login = new Login();
                login.Show();
                Close();
            }
        }
    }
}
