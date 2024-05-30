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
    /// Lógica de interacción para HomeKichenStaff.xaml
    /// </summary>
    public partial class HomeKichenStaff : Window {
        public HomeKichenStaff() {
            InitializeComponent();
            txbNameUser.Text = SingletonClass.Instance.UserName;
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

        private void ClickOrders(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/Order.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickSubMenu(object sender, RoutedEventArgs e) {
            if (stpSubMenu.Visibility == Visibility.Collapsed) {
                stpSubMenu.Visibility = Visibility.Visible;
            } else {
                stpSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickPublish(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/PublishSetMenu.xaml", UriKind.RelativeOrAbsolute));
        }        

        private void ClickDelete(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/SearchSetMenuToDelete.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickShowMenu(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/ConsultMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ClickModify(object sender, RoutedEventArgs e) {
            fraPages.Navigate(new System.Uri("/Views/Pages/SearchSetMenuToModify.xaml", UriKind.RelativeOrAbsolute));
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
