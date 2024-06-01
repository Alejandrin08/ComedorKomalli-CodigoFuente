using KomalliEmployee.Controller;
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

namespace KomalliEmployee.Views.Windows {
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window {
        public ChangePassword() {
            InitializeComponent();
        }

        private void ClickUpdatePassword(object sender, RoutedEventArgs e) {
            string newPassword = psbNewPassword.Password;
            string confirmPassword = psbConfirmPassword.Password;

            if (newPassword.Equals(confirmPassword)) {
                EmployeeController employeeController = new EmployeeController();
                int result = employeeController.UpdatePassword(SingletonClass.Instance.Email, newPassword);
                if (result > 0) {
                    App.ShowMessageInformation("Contraseña actualizada correctamente", "Actualización de contraseña");
                    Login login = new Login();
                    login.Show();
                    this.Close();
                } else {
                    App.ShowMessageError("Error al actualizar la contraseña", "Actualización de contraseña");
                    LoggerManager.Instance.LogInfo("Error al actualizar la contraseña");
                }
            } else {
                App.ShowMessageWarning("Las contraseñas no coinciden", "Actualización de contraseña");
            }
        }

        private void KeyDownChangePasswordBox(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter && btnChangePassword.IsEnabled) {
                ClickUpdatePassword(sender, e);
            }
        }

        private void KeyDownChangePasswordBoxConfirm(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter && btnChangePassword.IsEnabled) {
                ClickUpdatePassword(sender, e);
            }
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

        private void PasswordChangedValidatePasswordConfirm(object sender, RoutedEventArgs e) {
            PasswordBox passwordBox = (PasswordBox)sender;

            ValidationResult validationResult = new PasswordValidationRule().Validate(passwordBox.Password, null);

            if (!validationResult.IsValid) {
                tbkConfirmPassword.Visibility = Visibility.Visible;
                tbkConfirmPasswordText.Visibility = Visibility.Visible;
                btnChangePassword.IsEnabled = false;
                tbkConfirmPasswordText.Text = "Contraseña invalida, debe de ser máximo 15 caracteres con\n" +
                    " al menos una mayúscula, un número y un caracter especial";
            } else {
                tbkConfirmPassword.Visibility = Visibility.Collapsed;
                tbkConfirmPasswordText.Visibility = Visibility.Collapsed;
                btnChangePassword.IsEnabled = true;
                tbkConfirmPasswordText.Text = string.Empty;
            }
        }

        private void PasswordChangedValidatePassword(object sender, RoutedEventArgs e) {
            PasswordBox passwordBox = (PasswordBox)sender;

            ValidationResult validationResult = new PasswordValidationRule().Validate(passwordBox.Password, null);

            if (!validationResult.IsValid) {
                tbkNewPassword.Visibility = Visibility.Visible;
                tbkNewPasswordText.Visibility = Visibility.Visible;
                btnChangePassword.IsEnabled = false;
                tbkNewPasswordText.Text = "Contraseña invalida, debe de ser máximo 15 caracteres con\n" +
                    " al menos una mayúscula, un número y un caracter especial";
            } else {
                tbkNewPassword.Visibility = Visibility.Collapsed;
                tbkNewPasswordText.Visibility = Visibility.Collapsed;
                btnChangePassword.IsEnabled = true;
                tbkNewPasswordText.Text = string.Empty;
            }
        }
    }
}
