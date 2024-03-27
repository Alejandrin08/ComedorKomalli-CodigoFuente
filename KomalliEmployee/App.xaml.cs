using KomalliEmployee.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KomalliEmployee {
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application {

        public App() {
            Login login = new Login();
            login.Show();
        }
        public static void ShowMessageError(string message, string title) {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowMessageInformation(string message, string title) {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowMessageWarning(string message, string title) {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
