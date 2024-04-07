using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Profile;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;

namespace KomalliEmployee.Views.Pages{
    /// <summary>
    /// Interaction logic for FoodInventory.xaml
    /// </summary>
    public partial class FoodInventory : Page{
        public FoodInventory(){
            InitializeComponent();
            InitializeInventary();
        }

        private void ClickAddIngredient(object sender, RoutedEventArgs e){
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
    }
}


