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

namespace KomalliClient.Views.Pages {
    /// <summary>
    /// Lógica de interacción para HomeMenu.xaml
    /// </summary>
    public partial class HomeMenu : Page {
        public HomeMenu() {
            InitializeComponent();
        }

        private void MouseDownPackages(object sender, MouseButtonEventArgs e) {
            Packages packagesView = new Packages();
            this.NavigationService.Navigate(packagesView);
        }
    }
}
