using KomalliEmployee.Model.Utilities;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace KomalliEmployee.Model.Validations {
    public class EmailValidation : ValidationRule {
        public static TextBlock ErrorTextBlock { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result = ValidationResult.ValidResult;
            const int TIMEOUT = 10;
            try {
                Regex regex = new Regex(@"^(?i)([a-z0-9._%+-]+)@((uv\.mx|estudiantes\.uv\.mx|gmail\.com|hotmail\.com|outlook\.com|edu\.mx))$", RegexOptions.None, TimeSpan.FromSeconds(TIMEOUT));
                if (!regex.IsMatch(value.ToString())) {
                    result = new ValidationResult(false, "Email invalido");
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                        ErrorTextBlock.Text = "Email invalido";
                    }
                } else {
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                        ErrorTextBlock.Text = string.Empty;
                    }
                }
            } catch (RegexMatchTimeoutException ex) {
                App.ShowMessageError("Error al validar el email", "Validación de correo");
                LoggerManager.Instance.LogError("Error en el regex al validar el email", ex);
            }
            return result;
        }
    }
}