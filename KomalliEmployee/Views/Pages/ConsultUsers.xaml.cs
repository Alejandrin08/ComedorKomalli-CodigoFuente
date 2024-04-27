using KomalliEmployee.Controller;
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

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para ConsultUsers.xaml
    /// </summary>
    public partial class ConsultUsers : Page {
        public ConsultUsers() {
            InitializeComponent();
            InitializeListOfUsers();            
        }

        private void InitializeListOfUsers() {
            List<EmployeeModel> users;
            EmployeeController employeeControler = new EmployeeController();
            users = employeeControler.ConsultUsers();
            dgUsers.ItemsSource = users;
        }

        private void MouseDownOptions(object sender, MouseButtonEventArgs e) {
            var dataContext = (sender as FrameworkElement)?.DataContext;
            if (dataContext != null) {              
                var rowObject = (EmployeeModel)dataContext; 
                var personalNumber = rowObject.PersonalNumber;
                SingletonClass.Instance.PersonalNumberUserSelected = personalNumber;
            }
            ModifyUser modifyUser = new ModifyUser();
            this.NavigationService.Navigate(modifyUser);
        }

        private void TextChangedSearchUser (object sender, TextChangedEventArgs e) {
            string searchTerm = txbSearchUser.Text.ToLower();

            List<EmployeeModel> users;
            EmployeeController employeeControler = new EmployeeController();
            users = employeeControler.ConsultUsers();

            if (string.IsNullOrWhiteSpace(searchTerm)) {                
                dgUsers.ItemsSource = users;
            } else {               
                var filteredList = users.Where(item => item.Name.ToLower().Contains(searchTerm)).ToList();
                dgUsers.ItemsSource = filteredList;
            }
        }

        
    }
}
