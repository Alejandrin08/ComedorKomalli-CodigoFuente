using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;
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

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Interaction logic for SearchSetMenuToDelete.xaml
    /// </summary>
    public partial class SearchSetMenuToDelete : Page {
        public SearchSetMenuToDelete() {
            InitializeComponent();
            dpDate.SelectedDateChanged += DpDate_SelectedDateChanged;
            cbxTypeMenu.SelectionChanged += FieldsChanged;
            dpDate.SelectedDateChanged += FieldsChanged;
        }

        private void FieldsChanged(object sender, RoutedEventArgs e) {
            bool allFieldsFilled = cbxTypeMenu.SelectedItem != null &&
                                   dpDate.SelectedDate != null;

            btnSearchSetMenu.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            DayOfWeek selectedDayOfWeek = dpDate.SelectedDate?.DayOfWeek ?? DateTime.Today.DayOfWeek;
            if (selectedDayOfWeek == DayOfWeek.Saturday || selectedDayOfWeek == DayOfWeek.Sunday) {
                App.ShowMessageWarning("No se pueden seleccionar fines de semana.", "Fecha invalida");
                dpDate.SelectedDate = DateTime.Today;
            }
        }

        private void ClicKSearchSetMenu(object sender, RoutedEventArgs e) {
            SetMenuModel setMenuModel = new SetMenuModel();
            SetMenuController setMenuController = new SetMenuController();
            ComboBoxItem selectedItem = (ComboBoxItem)cbxTypeMenu.SelectedItem;
            string type = selectedItem.Content.ToString();
            DateTime date = (DateTime)dpDate.SelectedDate;
            setMenuModel = setMenuController.ExistingMenu(date, type);
            if (setMenuModel != null) {
                setMenuModel.Type = type == "Desayuno" ? TypeMenu.Desayuno : TypeMenu.Comida;
                SingletonClass.Instance.SetMenuModel = setMenuModel;
                DeleteSetMenu deleteSetMenu = new DeleteSetMenu();
                this.NavigationService.Navigate(deleteSetMenu);
            } else {
                App.ShowMessageWarning("No existe un menu para la fecha y tipo seleccionada", "Menu Inexistente");
            }
        }
    }
}
