using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
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

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl {
        public MenuUserControl() {
            InitializeComponent();
        }

        public void BindData(string typeMenu) {
            
                tbkFoodName.Text = typeMenu;
            
        }

        private void ClickAddMenu(object sender, RoutedEventArgs e) {
            FoodModel foodModel = new FoodModel();
            if (rdbPublic.IsChecked == true) {
                foodModel = new FoodModel() {
                    Name = tbkFoodName.Text + " Público G.",
                    Price = 50,
                    IsSelected = true
                };
            }else if (rdbStudent.IsChecked==true) {
                foodModel = new FoodModel() {
                    Name = tbkFoodName.Text + " Estudiante",
                    Price = 30,
                    IsSelected = true
                };
            }
            
            SingletonClass.Instance.SelectedFoods.Add(foodModel);
            rdbStudent.IsChecked = false;
            rdbPublic.IsChecked = false;
            btnAddMenu.IsEnabled = false;
        }

        private void CheckedRadioButton(object sender, RoutedEventArgs e) {
            if (rdbStudent.IsChecked == true || rdbPublic.IsChecked == true) {
                btnAddMenu.IsEnabled = true;
                
            } else {
                btnAddMenu.IsEnabled = false;
            }
        }

    }
}
