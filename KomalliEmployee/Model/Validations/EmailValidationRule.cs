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
    public class EmailValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result = new ValidationResult(true, null);
            const int TIMEOUT = 10;
            try {
                Regex regex = new Regex(@"^(?i)([a-z0-9._%+-]+)@((uv\.mx|estudiantes\.uv\.mx|gmail\.com|hotmail\.com|outlook\.com|edu\.mx))$", RegexOptions.None, TimeSpan.FromSeconds(TIMEOUT));
                if (!regex.IsMatch(value.ToString())) {
                    result = new ValidationResult(false, "Email invalido");
                } else {
                    result = ValidationResult.ValidResult;
                }
            } catch (RegexMatchTimeoutException ex) {
                App.ShowMessageError("Error al validar el email", "Validación de correo");
                LoggerManager.Instance.LogError("Error en el regex al validar el email", ex);
            }
            return result;
        }
    }
}
