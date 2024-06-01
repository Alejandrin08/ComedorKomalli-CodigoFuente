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

    public class PersonalNumberValidationRule : ValidationRule {

        public static TextBlock ErrorTextBlock { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result = ValidationResult.ValidResult;
            const int TIMEOUT = 10;
            try {
                Regex regex = new Regex(@"^(?=.*\d)\d{0,5}$|^$", RegexOptions.None, TimeSpan.FromSeconds(TIMEOUT));
                if (!regex.IsMatch(value.ToString())) {
                    result = new ValidationResult(false, "Numero de personal inválido");
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                        ErrorTextBlock.Text = "N. personal inválido, solo pueden ser números.";
                    }
                } else {
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                        ErrorTextBlock.Text = string.Empty;
                    }
                }
            } catch (RegexMatchTimeoutException ex) {
                App.ShowMessageError("Error al validar el numero de personal", "Validación de numero de personal");
                LoggerManager.Instance.LogError("Error en el regex al validar el número de personal", ex);
            }
            return result;
        }
    }
}