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

namespace KomalliEmployee.Views.Pages
{
    /// <summary>
    /// Interaction logic for DeleteSetMenu.xaml
    /// </summary>
    public partial class DeleteSetMenu : Page
    {
        public DeleteSetMenu()
        {
            InitializeComponent();
            InitializeFields();
        }

        private void ClickGoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }   

        private void InitializeFields()
        {
            SetMenuModel setMenuModel = SingletonClass.Instance.SetMenuModel;
            string selectedType = setMenuModel.Type.ToString();
            cbxTypeMenu.Text = selectedType;
            txbDrink.Text = setMenuModel.Drink;
            txbMainFood.Text = setMenuModel.MainFood;
            txbSalad.Text = setMenuModel.Salad;
            txbSideDish.Text = setMenuModel.SideDish;
            txbStarter.Text = setMenuModel.Starter;
            dpDate.SelectedDate = setMenuModel.DateMenu;
        }

        private void ClicKDeleteSetMenu(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el menú?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SetMenuModel setMenuModel = SingletonClass.Instance.SetMenuModel;
                SetMenuController setMenuController = new SetMenuController();
                int  resultDelete = setMenuController.DeleteMenu(setMenuModel.KeySetMenu);
                if (resultDelete == 1)
                {
                    App.ShowMessageInformation("El menu fue eliminado con exito", "Menu Eliminado");
                    this.NavigationService.GoBack();
                }
                else
                {
                    App.ShowMessageError("El menú no se pudo eliminar, intentelo más tarde", "Error del sistema");
                }
            }
            
        }
    }
}
