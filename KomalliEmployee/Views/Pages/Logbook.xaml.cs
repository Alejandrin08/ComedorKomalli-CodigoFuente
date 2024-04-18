using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Usercontrols;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Interaction logic for Logbook.xaml
    /// </summary>
    public partial class Logbook : Page {
        private int rowsAdded = 0;

        public Logbook() {
            InitializeComponent();
            ShowComments();
        }

        private void ClickPublish(object sender, RoutedEventArgs e) {
            try {
                EmployeeController employeeController = new EmployeeController();
                DateTime currentDateTime = DateTime.Now;
                LogbookController logbookController = new LogbookController();
                
                LogbookModel logbookModel = new LogbookModel {
                    Date = currentDateTime,
                    Commentary = txtAddCommentary.Text,
                    NoPersonalEmployee = employeeController.GetNoPersonalEmployee(SingletonClass.Instance.Email).ToString()
                };

                int result = logbookController.AddCommentary(logbookModel);

                if (result == -1) {
                    App.ShowMessageWarning("No se pudo realizar el registro. Por favor, inténtelo nuevamente.", "Error al agregar el comentario");
                } else {
                    App.ShowMessageInformation(Name + "El comentario se ha publicado correctamente.", "Comentario añadido");
                }
            } catch (Exception ex) {
                App.ShowMessageError("Error al agregar el comentario", "Error al agregar el comentario");
                LoggerManager.Instance.LogError("Error al agregar el comentario", ex);
            }
        }

        private void AddComments(LogbookModel logbook) {
            CommentCard commentCard = new CommentCard();
            Grid.SetRow(commentCard, rowsAdded);
            commentCard.setDataCard(logbook);
            CommentCardGrid.Children.Add(commentCard);
            rowsAdded++;

            RowDefinition rowDefinition = new RowDefinition();
            CommentCardGrid.RowDefinitions.Add(rowDefinition);
        }

        private void ShowComments() {
             rowsAdded = 0;
             CommentCardGrid.Children.Clear();
             CommentCardGrid.RowDefinitions.Clear();

            LogbookController logbookController = new LogbookController();
            EmployeeController employeeController = new EmployeeController();
            int noPersonal = employeeController.GetNoPersonalEmployee(SingletonClass.Instance.Email);
            List<LogbookModel> comments = logbookController.GetEmployeeComments(noPersonal.ToString());

             if (comments.Any()) {
                 foreach (LogbookModel logbook in comments) {
                     AddComments(logbook);
                 }
             }
             else {
                 lblWithoutResults.Content = "Sin resultados";
             }
         }
    }
}