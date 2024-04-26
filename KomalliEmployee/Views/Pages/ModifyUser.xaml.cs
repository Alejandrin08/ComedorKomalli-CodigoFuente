using KomalliEmployee.Model.Validations;
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
using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Page {

        bool EmailValid = false;
        bool PersonalNumberValid = false;
        bool NameValid = false;
        bool RoleValid = false;
        bool AvailabilityValid = false;
        public ModifyUser() {
            InitializeComponent();
            DataContext = new EmployeeModel();

            var emailValidation = new EmailValidationRule();
            EmailValidationRule.ErrorTextBlock = txbEmailValidationMessage;

            var personalNumberValidation = new PersonalNumberValidationRule();
            PersonalNumberValidationRule.ErrorTextBlock = txbPersonalNumberValidationMessage;

            var nameValidation = new NameValidationRule();
            NameValidationRule.ErrorTextBlock = txbNameValidationMessage;

            var roleValidation = new RoleValidationRule();
            RoleValidationRule.ErrorTextBlock = txbRoleValidationMessage;

           // var availabilityValidation = new AvailabilityValidationRule();
           // AvailabilityValidationRuleErrorTextBlock = txbAvailabilityValidationMessage;
        }

        private void TextChangedValidateEmail(object sender, TextChangedEventArgs e) {
            EmailValid = ValidateRuleTextBox(txtEmailUser, txbEmailValidationMessage);
            EnableButton();
        }

        private void TextChangedValidatePersonalNumber(object sender, TextChangedEventArgs e) {
            PersonalNumberValid = ValidateRuleTextBox(txtPersonalNumberUser, txbPersonalNumberValidationMessage);
            EnableButton();
        }

        private void TextChangedValidateName(object sender, TextChangedEventArgs e) {
            NameValid = ValidateRuleTextBox(txtNameUser, txbNameValidationMessage);
            EnableButton();
        }
        
        private void TextChangedValidateAvailability(object sender, TextChangedEventArgs e) {
            AvailabilityValid = ValidateRuleTextBox(txtAvailability, txbAvailabilityValidationMessage);
            EnableButton();
        }

        private void TextChangedValidateRole(object sender, TextChangedEventArgs e) {
            RoleValid = ValidateRuleTextBox(txtRoleUser, txbRoleValidationMessage);
            EnableButton();
        }

        private bool ValidateRuleTextBox(TextBox textBox, TextBlock textBlockMessage) {
            bool isValid = false;
            bool isEmpty = !string.IsNullOrEmpty(textBox.Text);
            if (!isEmpty) {
                textBlockMessage.Visibility = Visibility.Visible;
                textBlockMessage.Text = "No puede dejar vacío el campo.";
                isValid = false;
            } else {
                if (!Validation.GetHasError(textBox)) {
                    isValid = true;
                } else {
                    isValid = false;
                }
            }
            return isValid;
        }

        private void EnableButton() {
            if (EmailValid && NameValid && PersonalNumberValid && RoleValid && AvailabilityValid) {
                btnModifyUser.IsEnabled = true;
            } else {
                btnModifyUser.IsEnabled = false;
            }
        }

        private void ClicKModifyUser(object sender, RoutedEventArgs e) {
            btnModifyUser.IsEnabled = false;
            int emailValid = ValidateEmailDuplicity();
            int noPersonalValid = ValidateNoPersonalDuplicity();
            if (emailValid != 0 && noPersonalValid != 0) {
                EmployeeModel employeeModel = new EmployeeModel {
                    Name = txtNameUser.Text,
                    Email = txtEmailUser.Text,
                    Role = GetRole(txtRoleUser.Text),
                    PersonalNumber = txtPersonalNumberUser.Text,
                    Password = txtPersonalNumberUser.Text + "komalli",
                };
                EmployeeController employeeController = new EmployeeController();
                int resultAdduser = employeeController.AddUser(employeeModel);
                int resultAddEmployee = employeeController.AddEmployee(employeeModel);
                if (resultAdduser > 0 && resultAddEmployee > 0) {
                    App.ShowMessageInformation("Registro exitoso", "Registro de empleado");
                    CleanTextBoxes();
                } else {
                    App.ShowMessageWarning("Error al registrar el empleado", "Registro de empleado");
                }
            }
        }

        private void CleanTextBoxes() {

            ResetTextBoxes(txtNameUser, TextChangedValidateName);
            ResetTextBoxes(txtEmailUser, TextChangedValidateEmail);
            ResetTextBoxes(txtRoleUser, TextChangedValidateRole);
            ResetTextBoxes(txtPersonalNumberUser, TextChangedValidatePersonalNumber);

        }

        private void ResetTextBoxes(TextBox textBox, TextChangedEventHandler textChange) {
            textBox.TextChanged -= textChange;
            textBox.Text = "";
            textBox.TextChanged += textChange;
        }

        private UserRole GetRole(string roleName) {
            UserRole userRole = new UserRole();
            if (roleName == "Cajero") {
                userRole = UserRole.Cajero;
            }
            if (roleName == "Personal de cocina") {
                userRole = UserRole.PersonalCocina;
            }
            if (roleName == "Jefe de cocina") {
                userRole = UserRole.JefeCocina;
            }
            return userRole;
        }

        private int ValidateEmailDuplicity() {
            int result = ValidateDuplicity(txtEmailUser, txbEmailValidationMessage, "Email");
            EmailValid = result != 0;
            return result;
        }

        private int ValidateNoPersonalDuplicity() {
            int result = ValidateDuplicity(txtPersonalNumberUser, txbPersonalNumberValidationMessage, "PersonalNumber");
            PersonalNumberValid = result != 0;
            return result;
        }

        private int ValidateDuplicity(TextBox textBox, TextBlock textBlockMessage, string type) {
            int isValidData = 0;
            int result = 1;
            EmployeeController employeeController = new EmployeeController();

            if (type == "Email") {
                isValidData = employeeController.ValidateEmail(textBox.Text);
            } else if (type == "PersonalNumber") {
                isValidData = employeeController.ValidatePersonalNumber(textBox.Text);
            }
            if (isValidData == 1) {
                textBlockMessage.Visibility = Visibility.Visible;
                textBlockMessage.Text = "Dato duplicado en la base de datos.";
                result = 0;
            }
            return result;
        }

        private void KeyDownModifyUser(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ClicKModifyUser(sender, e);
            }
        }
    }
}
