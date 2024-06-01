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
    public class RoleValidationRule : ValidationRule {

        public static TextBlock ErrorTextBlock { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result = ValidationResult.ValidResult;
            const int TIMEOUT = 10;
            try {
                Regex regex = new Regex(@"^(Cajero|Personal de cocina|Jefe de cocina|\s*)$", RegexOptions.None, TimeSpan.FromSeconds(TIMEOUT));
                if (!regex.IsMatch(value.ToString())) {
                    result = new ValidationResult(false, "Rol inválido");
                    if (ErrorTextBlock != null) {
                        ErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                        ErrorTextBlock.Text = "Rol inválido, debe ser alguno de estos: Cajero, Personal de cocina, Jefe de cocina";
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