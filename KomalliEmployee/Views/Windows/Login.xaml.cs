using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;
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

namespace KomalliEmployee.Views.Windows {
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login() {
            InitializeComponent();
            DataContext = new EmployeeModel();
        }

        private void ClickLogin(object sender, RoutedEventArgs e) {
            EmployeeModel employeeModel = new EmployeeModel {
                Email = txtEmail.Text,
                Password = pssUser.Password
            };

            EmployeeController employeeController = new EmployeeController();

            Dictionary<UserRole, int> userValidation = employeeController.ValidateUser(employeeModel.Email, employeeModel.Password);

            if (userValidation.Count == 0) {
                MessageBox.Show("Usuario no encontrado");
            } else {
                if (userValidation.Values.First() == 0) {
                    SingletonClass.Instance.Email = employeeModel.Email;
                    ChangePassword changePassword = new ChangePassword();
                    changePassword.Show();
                    this.Close();
                } else if (userValidation.Values.First() == 1) {
                    switch (userValidation.Keys.First()) {
                        case UserRole.Cajero:
                            HomeCashier homeCashier = new HomeCashier();
                            homeCashier.Show();
                            this.Close();
                            break;
                        case UserRole.PersonalCocina:
                            HomeKichenStaff homeKichenStaff = new HomeKichenStaff();
                            homeKichenStaff.Show();
                            this.Close();
                            break;
                        case UserRole.JefeCocina:
                            HomeHeadChef homeHeadChef = new HomeHeadChef();
                            homeHeadChef.Show();
                            this.Close();
                            break;
                        case UserRole.Gerente:
                            HomeManager homeManager = new HomeManager();
                            homeManager.Show();
                            this.Close();
                            break;
                    }
                } else {
                    MessageBox.Show("Usuario bloqueado");
                }
            }
        }
        private void TextChangedValidateTextBox(object sender, TextChangedEventArgs e) {
            bool isEmailValid = !Validation.GetHasError(txtEmail) && !string.IsNullOrEmpty(txtEmail.Text);
            if (isEmailValid) {
                btnLogin.IsEnabled = true;
            } else {
                btnLogin.IsEnabled = false;
            }
        }
        private void KeyDownLoginPasswordBox(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ClickLogin(sender, e);
            }
        }
        private void KeyDownLoginTextBox(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ClickLogin(sender, e);
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
    }
}
