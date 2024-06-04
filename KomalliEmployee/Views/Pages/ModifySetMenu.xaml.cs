using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    /// Interaction logic for ModifySetMenu.xaml
    /// </summary>
    public partial class ModifySetMenu : Page {
        public ModifySetMenu() {
            InitializeComponent();
            dpDate.DisplayDateStart = DateTime.Today;
            dpDate.SelectedDateChanged += DpDate_SelectedDateChanged;
            txbDrink.TextChanged += FieldsChanged;
            txbMainFood.TextChanged += FieldsChanged;
            cbxTypeMenu.SelectionChanged += FieldsChanged;
            dpDate.SelectedDateChanged += FieldsChanged;
            txbStock.TextChanged += FieldsChanged;
            InitializeFields();
        }

        private void FieldsChanged(object sender, RoutedEventArgs e) {
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(txbDrink.Text) &&
                                   !string.IsNullOrWhiteSpace(txbMainFood.Text) &&
                                   !string.IsNullOrWhiteSpace(txbStock.Text) &&
                                   cbxTypeMenu.SelectedItem != null &&
                                   dpDate.SelectedDate != null;

            btnModifySetMenu.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            DayOfWeek selectedDayOfWeek = dpDate.SelectedDate?.DayOfWeek ?? DateTime.Today.DayOfWeek;
            if (selectedDayOfWeek == DayOfWeek.Saturday || selectedDayOfWeek == DayOfWeek.Sunday) {
                App.ShowMessageWarning("No se pueden seleccionar fines de semana.", "Fecha invalida");
                dpDate.SelectedDate = DateTime.Today;
            }
        }

        private void PreviewTextInputOnlyNumberAndLetters(object sender, TextCompositionEventArgs e) {
            TextBox textBox = sender as TextBox;
            if (textBox != null) {
                if (textBox.Text.Length + e.Text.Length > 20) {
                    e.Handled = true;
                    return;
                }
                foreach (char character in e.Text) {
                    if (!char.IsLetter(character) && character != '.' && !char.IsWhiteSpace(character)) {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void PreviewTextInputOnlyNumber(object sender, TextCompositionEventArgs e) {
            TextBox textBox = sender as TextBox;
            if (textBox != null) {
                if (textBox.Text.Length + e.Text.Length > 3) {
                    e.Handled = true;
                    return;
                }
                foreach (char character in e.Text) {
                    if (!char.IsNumber(character) && !char.IsWhiteSpace(character)) {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void ClickGoBack(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        private void ClicKModifySetMenu(object sender, RoutedEventArgs e) {
            SetMenuModel setMenu = generateSetMenu();
            SetMenuModel setMenuModel = new SetMenuModel();
            setMenuModel = SingletonClass.Instance.SetMenuModel;
            SetMenuController setMenuController = new SetMenuController();
            int result = 0;
            if (setMenu.DateMenu != setMenuModel.DateMenu || setMenu.Type != setMenuModel.Type) {
                result = setMenuController.ExistingTypeMenu(setMenu);
                if (result != 0) {
                    App.ShowMessageWarning("Ya existe ese tipo de menu para la fecha indicada.", "Fecha invalida");
                    return;
                }
            }
            result = setMenuController.ModifyMenu(setMenu);
            if (result == 1) {
                App.ShowMessageInformation("Menú actualizado con exito", "Menu actualizado");
                SingletonClass.Instance.SetMenuModel = setMenu;
                InitializeFields();
                this.NavigationService.GoBack();
            } else {
                App.ShowMessageError("El menú no se pudo actualizar, intentelo más tarde", "Error del sistema");
            }
        }

        private SetMenuModel generateSetMenu() {
            SetMenuModel setMenuModel = new SetMenuModel();
            setMenuModel = SingletonClass.Instance.SetMenuModel;
            SetMenuModel setMenu = new SetMenuModel();
            setMenu.MainFood = txbMainFood.Text;
            setMenu.DateMenu = dpDate.SelectedDate ?? DateTime.Today;
            setMenu.Drink = txbDrink.Text;
            if (!string.IsNullOrWhiteSpace(txbStarter.Text)) {
                setMenu.Starter = txbStarter.Text;
            }
            if (!string.IsNullOrWhiteSpace(txbSideDish.Text)) {
                setMenu.SideDish = txbSideDish.Text;
            }
            if (!string.IsNullOrWhiteSpace(txbSalad.Text)) {
                setMenu.Salad = txbSalad.Text;
            }
            setMenu.PriceStudent = 30;
            setMenu.Pricegeneral = 50;
            int stockValue;
            if (int.TryParse(txbStock.Text, out stockValue)) {
                setMenu.Stock = stockValue;
            } else {
                setMenu.Stock = 0;
                MessageBox.Show("Invalid stock value. Please enter a valid number.");
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cbxTypeMenu.SelectedItem;
            string selectedContent = selectedItem.Content.ToString();
            setMenu.Type = selectedContent == "Desayuno" ? TypeMenu.Desayuno : TypeMenu.Comida;
            setMenu.KeySetMenu = setMenuModel.KeySetMenu;
            return setMenu;
        }

        private void InitializeFields() {
            SetMenuModel setMenuModel = SingletonClass.Instance.SetMenuModel;
            string selectedType = setMenuModel.Type.ToString();
            cbxTypeMenu.Text = selectedType;
            txbDrink.Text = setMenuModel.Drink;
            txbMainFood.Text = setMenuModel.MainFood;
            txbSalad.Text = setMenuModel.Salad;
            txbSideDish.Text = setMenuModel.SideDish;
            txbStarter.Text = setMenuModel.Starter;
            dpDate.SelectedDate = setMenuModel.DateMenu;
            txbStock.Text = setMenuModel.Stock.ToString();
        }
    }
}
