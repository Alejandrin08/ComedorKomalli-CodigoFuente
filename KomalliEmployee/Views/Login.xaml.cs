using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KomalliEmployee.Views {
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page {

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
                    NavigationService.Navigate(new ChangePassword());
                } else if (userValidation.Values.First() == 1) {
                    switch (userValidation.Keys.First()) {
                        case UserRole.Cajero:
                            MessageBox.Show("Welcome Cajero");
                            break;
                        case UserRole.PersonalCocina:
                            MessageBox.Show("Welcome Personal de cocina");
                            break;
                        case UserRole.JefeCocina:
                            MessageBox.Show("Welcome Jefe de cocina");
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
    }
}
