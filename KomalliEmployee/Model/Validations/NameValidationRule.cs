using KomalliEmployee.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KomalliEmployee.Model.Validations {
    public class NameValidationRule : ValidationRule {

        public static TextBlock ErrorTextBlock { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result = ValidationResult.ValidResult;
            const int TIMEOUT = 10;
            try {
                Regex regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$", RegexOptions.None, TimeSpan.FromSeconds(TIMEOUT));
                if (!regex.IsMatch(value.ToString())) {
                    result = new ValidationResult(false, "Nombre inválido");
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                        ErrorTextBlock.Text = "Nombre inválido.";
                    }
                } else {
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                        ErrorTextBlock.Text = string.Empty;
                    }
                }
            } catch (RegexMatchTimeoutException ex) {
                App.ShowMessageError("Error al validar el nombre de usuario", "Validación de nombre de usuario");
                LoggerManager.Instance.LogError("Error en el regex al validar el nombre de usuario", ex);
            }
            return result;
        }
    }
}