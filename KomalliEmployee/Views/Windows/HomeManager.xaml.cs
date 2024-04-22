using KomalliEmployee.Model.Utilities;
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
using System.Windows.Shapes;

namespace KomalliEmployee.Views.Windows {
    /// <summary>
    /// Lógica de interacción para HomeManager.xaml
    /// </summary>
    public partial class HomeManager : Window {
        public HomeManager() {
            InitializeComponent();
            txbNameUser.Text = SingletonClass.Instance.UserName;
        }

        private void ClickSubMenuUsers(object sender, RoutedEventArgs e) {
            if (stpSubMenuUsers.Visibility == Visibility.Collapsed) {
                stpSubMenuUsers.Visibility = Visibility.Visible;
            } else {
                stpSubMenuUsers.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickConsultUsers(object sender, RoutedEventArgs e) {

        }

        private void ClickRegisterUser(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/RegisterUser.xaml", UriKind.RelativeOrAbsolute));

        }

        private void ClickSubMenuReports(object sender, RoutedEventArgs e) {
            if (stpSubMenuReports.Visibility == Visibility.Collapsed) {
                stpSubMenuReports.Visibility = Visibility.Visible;
            } else {
                stpSubMenuReports.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickInventoryReport(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/InventoryReport.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickLogbookReport(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/LogbookReport.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickDailyTransactions(object sender, RoutedEventArgs e) {

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
    }
}
