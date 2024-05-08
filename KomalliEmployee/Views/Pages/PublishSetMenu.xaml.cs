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
using KomalliEmployee.Model.Utilities;

namespace KomalliEmployee.Views.Pages
{
    /// <summary>
    /// Interaction logic for PublishSetMenu.xaml
    /// </summary>
    public partial class PublishSetMenu : Page
    {
        public PublishSetMenu()
        {
            InitializeComponent();
            dpDate.DisplayDateStart = DateTime.Today; 
            dpDate.SelectedDateChanged += DpDate_SelectedDateChanged;
            txbDrink.TextChanged += FieldsChanged;
            txbMainFood.TextChanged += FieldsChanged;
            cbxTypeMenu.SelectionChanged += FieldsChanged;
            dpDate.SelectedDateChanged += FieldsChanged;
        }

        private void FieldsChanged(object sender, RoutedEventArgs e)
        {
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(txbDrink.Text) &&
                                   !string.IsNullOrWhiteSpace(txbMainFood.Text) &&
                                   cbxTypeMenu.SelectedItem != null &&
                                   dpDate.SelectedDate != null;

            btnPublishSetMenu.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DayOfWeek selectedDayOfWeek = dpDate.SelectedDate?.DayOfWeek ?? DateTime.Today.DayOfWeek;
            if (selectedDayOfWeek == DayOfWeek.Saturday || selectedDayOfWeek == DayOfWeek.Sunday)
            {
                App.ShowMessageWarning("No se pueden seleccionar fines de semana.","Fecha invalida");
                dpDate.SelectedDate = DateTime.Today;
            }
        }

        private void PreviewTextInputOnlyNumberAndLetters(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text.Length + e.Text.Length > 20)
                {
                    e.Handled = true;
                    return;
                }
                foreach (char character in e.Text)
                {
                    if (!char.IsLetter(character) && character != '.' && !char.IsWhiteSpace(character))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void ClicKRegisterSetMenu(object sender, RoutedEventArgs e)
        {
            SetMenuModel setMenu = generateSetMenu();
            SetMenuController setMenuController = new SetMenuController();
            int result = setMenuController.ExistingTypeMenu(setMenu);
            if ( result == 0)
            {
                     result = setMenuController.AddSetMenu(setMenu);
                    if(result == 1)
                    {
                        App.ShowMessageInformation("Menú publicado con exito", "Menu publicado");
                    }
                    else
                    {
                        App.ShowMessageError("El menú no se pudo publicar, intentelo más tarde", "Error del sistema");
                    }
            }
            else
            {
                App.ShowMessageWarning("Ya existe ese tipo de menu para la fecha indicada.", "Fecha invalida");
            }
        }

        private SetMenuModel generateSetMenu()
        {
            SetMenuModel setMenu = new SetMenuModel();
            setMenu.MainFood = txbMainFood.Text;
            setMenu.DateMenu = dpDate.SelectedDate ?? DateTime.Today;
            setMenu.Drink = txbDrink.Text;
            if (!string.IsNullOrWhiteSpace(txbStarter.Text))
            {
                setMenu.Starter = txbStarter.Text;
            }
            if (!string.IsNullOrWhiteSpace(txbSideDish.Text))
            {
                setMenu.SideDish = txbSideDish.Text;
            }
            if (!string.IsNullOrWhiteSpace(txbSalad.Text))
            {
                setMenu.Salad = txbSalad.Text;
            }
            setMenu.PriceStudent = 30;
            setMenu.Pricegeneral = 50;
            ComboBoxItem selectedItem = (ComboBoxItem)cbxTypeMenu.SelectedItem;
            string selectedContent = selectedItem.Content.ToString();
            setMenu.Type = selectedContent == "Desayuno" ? TypeMenu.Desayuno : TypeMenu.Comida;
            setMenu.KeySetMenu = GenerateKeySetmenu(selectedContent);
            MessageBox.Show(setMenu.KeySetMenu);
            return setMenu;
        }

        private string GenerateKeySetmenu(string selectedContent)
        {
            
                string namePrefix = selectedContent.Length >= 3 ?
                                    selectedContent.Substring(0, 3).ToUpper() :
                                    selectedContent.ToUpper();
                Random random = new Random();
                string randomNumber = "";
                for (int i = 0; i < 3; i++)
                {
                    randomNumber += random.Next(0, 10);
                }
                string keyIngredient = namePrefix + randomNumber;
                return keyIngredient;
            
        }
    }
}
