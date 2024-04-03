using KomalliEmployee.Controller;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RegisterIngredient.xaml
    /// </summary>
    public partial class RegisterIngredient : Page
    {
        public RegisterIngredient()
        {
            InitializeComponent();
            txbNameIngredient.TextChanged += FieldsChanged;
            txbQuotaIngredient.TextChanged += FieldsChanged;
            txbBarCode.TextChanged += FieldsChanged;
            cbxTipeQuota.SelectionChanged += FieldsChanged;
            btnRegisterIngredient.Visibility = Visibility.Collapsed;
        }

        private void FieldsChanged(object sender, RoutedEventArgs e)
        {
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(txbNameIngredient.Text) &&
                                   !string.IsNullOrWhiteSpace(txbQuotaIngredient.Text) &&
                                   cbxTipeQuota.SelectedItem != null;

            btnRegisterIngredient.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ClickGoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ClicKRegisterIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = CreateIngredient();
            IngredientController ingredientControler = new IngredientController();
            if (!VerifyNameIngredient(ingredient.NameIngredient))
            {
                if (ingredient.Barcode == null || !VerifyBarCode(ingredient.Barcode))
                {
                    switch (ingredientControler.AddIngredient(ingredient))
                    {
                        case 0:
                            MessageBox.Show("Ocurrio un error al agregar un ingrediente porfavor intenta más tarde");
                            break;
                        case 1:
                            MessageBox.Show("El ingrediente fue agregado correctamente");
                            FoodInventory foodInventoryView = new FoodInventory();
                            this.NavigationService.Navigate(foodInventoryView);
                            break;
                        case 2:
                            MessageBox.Show("Lo sentimos, ocurrio un problema con la conexion a la base de datos, favor de intentarlo más tarde");
                            break;
                    }
                }
            }
        }

        private Ingredient CreateIngredient()
        {
            Ingredient ingredient = new Ingredient();
            ingredient.NameIngredient = txbNameIngredient.Text;
            ingredient.Quantity = txbQuotaIngredient.Text;
            if (string.IsNullOrEmpty(txbBarCode.Text))
            {
                ingredient.Barcode = null;
            }
            else
            {
                ingredient.Barcode = txbBarCode.Text;
            }
            ingredient.Barcode = txbBarCode.Text;
            ingredient.KeyIngredient = GenerateKey(txbNameIngredient.Text);
            ComboBoxItem selectedItem = (ComboBoxItem)cbxTipeQuota.SelectedItem;
            string selectedContent = selectedItem.Content.ToString();
            switch (selectedContent)
            {
                case "Kg":
                    ingredient.Measurement = TypeQuantity.Kg.ToString();
                    break;
                case "Lts":
                    ingredient.Measurement = TypeQuantity.Lts.ToString();
                    break;
                case "Unidades":
                    ingredient.Measurement = TypeQuantity.Unidades.ToString();
                    break;
            }
            return ingredient;
        }

        private string GenerateKey(string name)
        {
            string namePrefix = name.Length >= 4 ?
                                name.Substring(0, 4).ToUpper() :
                                name.ToUpper();
            Random random = new Random();
            string randomNumber = "";
            for (int i = 0; i < 4; i++)
            {
                randomNumber += random.Next(0, 10);
            }
            string keyIngredient = namePrefix + randomNumber;
            return keyIngredient;
        }

        private bool VerifyNameIngredient(string nameIngredient)
        {
            bool result = true;
            IngredientController ingredientControler = new IngredientController();
            switch (ingredientControler.IsNameIngredientExisting(nameIngredient))
            {
                case 0:
                    MessageBox.Show("Ya existe un ingrediente con ese nombre dentro del sistema verifica tus datos");
                    break;
                case 1:
                    result = false;
                    break;
                case 2:
                    MessageBox.Show("Lo sentimos, ocurrio un problema con la conexion a la base de datos, favor de intentarlo más tarde");
                    break;
            }
            return result;
        }

        private bool VerifyBarCode(string barCodeIngredient)
        {
            bool result = true;
            IngredientController ingredientControler = new IngredientController();
            switch (ingredientControler.IsBarCodeExisting(barCodeIngredient))
            {
                case 0:
                    MessageBox.Show("Ya existe un ingrediente con ese codigo de barras dentro del sistema verifica tus datos");
                    break;
                case 1:
                    result = false;
                    break;
                case 2:
                    MessageBox.Show("Lo sentimos, ocurrio un problema con la conexion a la base de datos, favor de intentarlo más tarde");
                    break;
            }
            return result;
        }
    }
}