using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace KomalliEmployee.Views.Pages
{
    /// <summary>
    /// Interaction logic for FoodInventory.xaml
    /// </summary>
    public partial class FoodInventory : Page
    {
        public ObservableCollection<string> MeasurementOptions { get; set; }
        public ObservableCollection<string> CategoryOptions { get; set; }
        private List<IngredientModel> listIngredientsModified = new List<IngredientModel>();

        public FoodInventory()
        {
            InitializeComponent();
            InitializeInventary();
            MeasurementOptions = new ObservableCollection<string> { "Lts", "Kg", "Unidades" };
            CategoryOptions = new ObservableCollection<string> { "Abarrotes", "Bebidas", "FrutasyVerduras", "Postres", "CarnesFrias" };
            DataContext = this;
        }

        private void PreviewTextInputOnlyNumberAndLetters(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text.Length + e.Text.Length > 30)
                {
                    e.Handled = true;
                    return;
                }
                foreach (char character in e.Text)
                {
                    if (!char.IsLetterOrDigit(character) && character != '.' && !char.IsWhiteSpace(character))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void PreviewTextInputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text.Length + e.Text.Length > 13)
                {
                    e.Handled = true;
                    return;
                }
                foreach (char character in e.Text)
                {
                    if (!char.IsDigit(character))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }
        private void TxtSearchIngredient(object sender, TextChangedEventArgs e)
        {
            List<IngredientModel> ingredients;
            string searchIngredient = txbSearchIngredient.Text;
            IngredientController ingredientControler = new IngredientController();
            ingredients = ingredientControler.SearchIngredients(searchIngredient);
            dgIngredientInventory.ItemsSource = ingredients;

        }

        private void CbxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cbxCategory.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedCategory = selectedItem.Content.ToString();
                if (selectedCategory != null)
                {
                    if (selectedCategory == "General")
                    {
                        InitializeInventary();
                    }
                    else
                    {
                        List<IngredientModel> ingredients;
                        IngredientController ingredientControler = new IngredientController();
                        ingredients = ingredientControler.SearchIngredientsByCategory(selectedCategory);
                        dgIngredientInventory.ItemsSource = ingredients;
                    }
                }
            }
        }

        private void ClickAddIngredient(object sender, RoutedEventArgs e)
        {
            RegisterIngredient registerIngredientView = new RegisterIngredient();
            this.NavigationService.Navigate(registerIngredientView);
        }

        private void InitializeInventary()
        {
            List<IngredientModel> ingredients;
            IngredientController ingredientControler = new IngredientController();
            ingredients = ingredientControler.ConsultIngredients();
            dgIngredientInventory.ItemsSource = ingredients;
        }

        private void ClickModifyInventory(object sender, RoutedEventArgs e)
        {
            dgIngredientInventory.IsReadOnly = false;
            dgtNameIngredient.IsReadOnly = false;
            dgtQuantityIngredient.IsReadOnly = false;
            dgtTypeMeasurementIngredient.IsReadOnly = false;
            dgtIngredientCategory.IsReadOnly = false;
            dgtKeyIngredient.IsReadOnly = true;
            dgtUpdateIngredient.IsReadOnly = true;
            btnAddIngredient.Visibility = Visibility.Hidden;
            btnSaveChanges.Visibility = Visibility.Visible;
        }

        private void dgIngredientInventory_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            if (e.EditingElement is TextBox textBox)
            {
                if (e.Column == dgtQuantityIngredient)
                {
                    textBox.PreviewTextInput += PreviewTextInputOnlyNumber;
                }
                else if (e.Column == dgtNameIngredient)
                {
                    textBox.PreviewTextInput += PreviewTextInputOnlyNumberAndLetters;
                }
            }
        }

        private void dgIngredientInventory_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var fila = e.Row.Item as IngredientModel;
                var columna = e.Column as DataGridColumn;

                if (fila != null && columna != null)
                {
                    if (e.EditingElement is TextBox textBox)
                    {
                        if (columna == dgtNameIngredient)
                            fila.NameIngredient = textBox.Text;
                        else if (columna == dgtQuantityIngredient)
                            fila.Quantity = textBox.Text;
                    }
                    else if (e.EditingElement is ComboBox comboBox)
                    {
                        if (columna == dgtTypeMeasurementIngredient)
                        {
                            var selectedContent = comboBox.SelectedItem?.ToString();
                            if (Enum.TryParse(selectedContent, out TypeQuantity measurement))
                                fila.Measurement = measurement;
                        }
                        else if (columna == dgtIngredientCategory)
                        {
                            var selectedContent = comboBox.SelectedItem?.ToString();
                            if (Enum.TryParse(selectedContent, out IngredientCategory category))
                                fila.Category = category;
                        }
                    }

                    var existingIngredient = listIngredientsModified.FirstOrDefault(i => i.KeyIngredient == fila.KeyIngredient);
                    if (existingIngredient != null)
                    {
                        existingIngredient.NameIngredient = fila.NameIngredient;
                        existingIngredient.Quantity = fila.Quantity;
                        existingIngredient.Measurement = fila.Measurement;
                    }
                    else
                    {
                        listIngredientsModified.Add(fila);
                    }
                }
            }
        }

        private void ClickBtnSaveChanges(object sender, RoutedEventArgs e)
        {
            dgIngredientInventory.IsReadOnly = true;
            btnAddIngredient.Visibility = Visibility.Visible;
            btnSaveChanges.Visibility = Visibility.Hidden;
            IngredientController ingredientController = new IngredientController();
            if (ingredientController.ModifyIngredients(listIngredientsModified) == 1)
            {
                App.ShowMessageInformation("La informacion de los ingredientes fue actualizada.", "Inventario actualizado");
                listIngredientsModified.Clear();
                InitializeInventary();
            }
        }
    }
}