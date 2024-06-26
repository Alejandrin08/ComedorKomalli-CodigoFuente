﻿using KomalliEmployee.Model.Validations;
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
using System.ComponentModel.DataAnnotations;

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
            InitializeValidations();
            SetInfo();
        }

        private void InitializeValidations() {
            var emailValidation = new EmailValidationRule();
            EmailValidationRule.ErrorTextBlock = txbEmailValidationMessage;

            var personalNumberValidation = new PersonalNumberValidationRule();
            PersonalNumberValidationRule.ErrorTextBlock = txbPersonalNumberValidationMessage;

            var nameValidation = new NameValidationRule();
            NameValidationRule.ErrorTextBlock = txbNameValidationMessage;

            var roleValidation = new RoleValidationRule();
            RoleValidationRule.ErrorTextBlock = txbRoleValidationMessage;

            var availabilityValidation = new AvailabilityValidationRule();
            AvailabilityValidationRule.ErrorTextBlock = txbAvailabilityValidationMessage;
        }

        public void SetInfo() {
            EmployeeController employeeController = new EmployeeController();
            EmployeeModel employeeModel = employeeController.GetUserInfo(SingletonClass.Instance.PersonalNumberUserSelected);
            SingletonClass.Instance.EmailUserSelected = employeeModel.Email;
            SingletonClass.Instance.AvailabilityUserSelected = employeeModel.Availability;
            txtNameUser.Text = employeeModel.Name;
            txtPersonalNumberUser.Text = employeeModel.PersonalNumber;
            txtEmailUser.Text = employeeModel.Email;
            txtAvailability.Text = employeeModel.Availability;           
            txtRoleUser.Text = employeeModel.RoleUser;
            this.DataContext = employeeModel;
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

            EmployeeController employeeController = new EmployeeController();
            EmployeeModel employeeModel = employeeController.GetUserInfo(SingletonClass.Instance.PersonalNumberUserSelected);
            if (txtNameUser.Text == employeeModel.Name &&
                txtPersonalNumberUser.Text == employeeModel.PersonalNumber &&
                txtEmailUser.Text == employeeModel.Email &&
                txtAvailability.Text == employeeModel.Availability &&
                txtRoleUser.Text == employeeModel.RoleUser) {

                btnModifyUser.IsEnabled = false;

            } else {
                if (EmailValid && NameValid && PersonalNumberValid && RoleValid && AvailabilityValid) {
                    btnModifyUser.IsEnabled = true;
                } else {
                    btnModifyUser.IsEnabled = false;
                }
            }
        }

        private EmployeeModel GetInfoEmployee() {
            EmployeeController employeeController = new EmployeeController();
            EmployeeModel employeeModel = employeeController.GetUserInfo(SingletonClass.Instance.PersonalNumberUserSelected);
            return employeeModel;
        }

        private bool ValidateEmailWasModified() {
            bool result;
            EmployeeModel employeeModel = GetInfoEmployee();
            if (txtEmailUser.Text != SingletonClass.Instance.EmailUserSelected && txtNameUser.Text == employeeModel.Name && txtPersonalNumberUser.Text == employeeModel.PersonalNumber && txtRoleUser.Text == employeeModel.RoleUser && txtAvailability.Text == SingletonClass.Instance.AvailabilityUserSelected) {
                result = true;
            } else {
                result = false;
            }
            return result;
        }

        private bool ValidateUserWasModified() {
            bool result;
            EmployeeModel employeeModel = GetInfoEmployee();
            if (txtEmailUser.Text != SingletonClass.Instance.EmailUserSelected && txtNameUser.Text == employeeModel.Name && txtPersonalNumberUser.Text == employeeModel.PersonalNumber && txtRoleUser.Text == employeeModel.RoleUser && txtAvailability.Text != SingletonClass.Instance.AvailabilityUserSelected) {
                result = true;
            } else {
                result = false;
            }
            return result;
        }

        private bool ValidateAvailabilitylWasModified() {
            bool result;
            EmployeeModel employeeModel = GetInfoEmployee();
            if (txtAvailability.Text != SingletonClass.Instance.AvailabilityUserSelected && txtNameUser.Text == employeeModel.Name && txtPersonalNumberUser.Text == employeeModel.RoleUser && txtRoleUser.Text == employeeModel.RoleUser && txtEmailUser.Text == SingletonClass.Instance.EmailUserSelected) {
                result = true;
            } else {
                result = false;
            }
            return result;
        }

        private void ClicKModifyUser(object sender, RoutedEventArgs e) {
            btnModifyUser.IsEnabled = false;
            int emailValid = ValidateEmailDuplicity();
            int noPersonalValid = ValidateNoPersonalDuplicity();
            if (emailValid != 0 && noPersonalValid != 0) {
                EmployeeModel employeeModel = GetInfo();
                EmployeeController employeeController = new EmployeeController();
                int resultUpdateUser = 1;
                int resultUpdateEmployee = 1;
                if (txtEmailUser.Text == SingletonClass.Instance.EmailUserSelected && txtAvailability.Text == SingletonClass.Instance.AvailabilityUserSelected) {
                    resultUpdateEmployee = employeeController.UpdateEmployeeInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                } else {
                    if (ValidateEmailWasModified()) {
                        resultUpdateUser = employeeController.UpdateUserInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                    } else if (ValidateAvailabilitylWasModified()) {
                        resultUpdateUser = employeeController.UpdateUserInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                    } else if (ValidateUserWasModified()) {
                        resultUpdateUser = employeeController.UpdateUserInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                    } else if (txtEmailUser.Text == SingletonClass.Instance.EmailUserSelected) {
                        resultUpdateUser = employeeController.UpdateUserInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                        resultUpdateEmployee = employeeController.UpdateEmployeeInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                    } else {
                        resultUpdateUser = employeeController.UpdateUserInfo(employeeModel, SingletonClass.Instance.EmailUserSelected);
                        resultUpdateEmployee = employeeController.UpdateEmployeeInfo(employeeModel, txtEmailUser.Text);
                    }
                }

                if (resultUpdateUser > 0 && resultUpdateEmployee > 0) {
                    App.ShowMessageInformation("Actualización exitosa", "Modificación de empleado");                    
                } else {
                    App.ShowMessageWarning("Error al actualizar los datos del empleado", "Modificación de empleado");
                }
                this.NavigationService.GoBack();
            }
        }

        private EmployeeModel GetInfo() {
            EmployeeModel employeeModel = new EmployeeModel {
                Name = txtNameUser.Text,
                Email = txtEmailUser.Text,
                RoleUser = txtRoleUser.Text,
                PersonalNumber = txtPersonalNumberUser.Text,
                Availability = txtAvailability.Text,
            };
            return employeeModel;
        }

        private int ValidateEmailDuplicity() {
            int result = -0; 
            if(txtEmailUser.Text == SingletonClass.Instance.EmailUserSelected) {
                result = 1;
            } else {
                result = ValidateDuplicity(txtEmailUser, txbEmailValidationMessage, "Email");
                EmailValid = result != 0;
            }            
            return result;
        }

        private int ValidateNoPersonalDuplicity() {
            int result = -0;
            if (txtPersonalNumberUser.Text == SingletonClass.Instance.PersonalNumberUserSelected) {
                result = 1;
            } else {
                result = ValidateDuplicity(txtPersonalNumberUser, txbPersonalNumberValidationMessage, "PersonalNumber");
                PersonalNumberValid = result != 0;
            }
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

        private void MouseDownGoBack(object sender, MouseButtonEventArgs e) {
            this.NavigationService.GoBack();
        }
    }
}
