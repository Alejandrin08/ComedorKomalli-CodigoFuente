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

namespace KomalliEmployee.Views {
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page {
        public ChangePassword() {
            InitializeComponent();
        }

        private void ClickUpdatePassword(object sender, RoutedEventArgs e) {
            string newPassword = pssNewPassword.Password;
            string confirmPassword = pssConfirmPassword.Password;

            if (newPassword.Equals(confirmPassword)) {
                EmployeeController employeeController = new EmployeeController();
                int result = employeeController.UpdatePassword(SingletonClass.Instance.Email, newPassword);
                if (result > 0) {
                    MessageBox.Show("Password updated successfully");
                    NavigationService.Navigate(new Login());
                } else {
                    MessageBox.Show("Error updating password");
                }
            } else {
                MessageBox.Show("Passwords do not match");
            }
        }

        private void KeyDownChangePasswordBox(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                pssConfirmPassword.Focus();
            }
        }

        private void KeyDownChangePasswordBoxConfirm(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ClickUpdatePassword(sender, e);
            }
        }
    }
}
