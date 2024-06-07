using KomalliEmployee.Controller;
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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para ConsultMenu.xaml
    /// </summary>
    public partial class ConsultMenu : Page {

        private MenuController _menuController;
        public ConsultMenu() {
            InitializeComponent();

            _menuController = new MenuController();

            MenuModel menuBreakfast = _menuController.GetBreakfastOfTheDay(DateTime.Today);
            if (menuBreakfast != null) {
                lstBreakfast.ItemsSource = new List<MenuModel> { menuBreakfast };
            }

            MenuModel menuFood = _menuController.GetFoodOfTheDay(DateTime.Today);
            if (menuFood != null) {
                lstFood.ItemsSource = new List<MenuModel> { menuFood };
            }

            dtpSelectDate.SetValue(DatePicker.SelectedDateProperty, DateTime.Today);
        }

        private void SelectedDateChangedConsultMenu(object sender, SelectionChangedEventArgs e) {
            DateTime selectedDate = (DateTime)dtpSelectDate.SelectedDate;
            MenuModel menuBreakfast = _menuController.GetBreakfastOfTheDay(selectedDate);
            if (menuBreakfast != null) {
                lstBreakfast.ItemsSource = new List<MenuModel> { menuBreakfast };
            } else {
                App.ShowMessageWarning("No se pudo recuperar el menú del desayuno", "Error");
            }

            MenuModel menuFood = _menuController.GetFoodOfTheDay(selectedDate);
            if (menuFood != null) {
                lstFood.ItemsSource = new List<MenuModel> { menuFood };
            } else {
                App.ShowMessageWarning("No se pudo recuperar el menú de comida", "Error");
            }
        }
    }
}
