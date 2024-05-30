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
using System.Text.RegularExpressions;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Interaction logic for Logbook.xaml
    /// </summary>
    public partial class Logbook : Page {

        public Logbook() {
            InitializeComponent();
            ShowComments();
            CurrentDateNow();
            SelectedItem();
            
        }

        private void ClickPublish(object sender, RoutedEventArgs e) {
            try {
                EmployeeController employeeController = new EmployeeController();
                DateTime currentDateTime = DateTime.Now;
                LogbookController logbookController = new LogbookController();

                if (txtAddCommentary.Text.Length > 255) {
                    App.ShowMessageWarning("El comentario no puede exceder los 255 caracteres.", "Error al agregar el comentario");
                    txtAddCommentary.Text = "";
                }
                else if (!ValidateText(txtAddCommentary.Text)) {
                    App.ShowMessageWarning("Comentario no valido.", "Error al agregar el comentario");
                    txtAddCommentary.Text = "";
                }
                else { 
                        LogbookModel logbookModel = new LogbookModel {
                                Date = currentDateTime,
                                Section = cmbSection.SelectedItem.ToString(),
                                Commentary = txtAddCommentary.Text,
                                NoPersonalEmployee = employeeController.GetNoPersonalEmployee(SingletonClass.Instance.Email)
                        };

                        int result = logbookController.AddCommentary(logbookModel);

                    if (result == -1) {
                            App.ShowMessageWarning("No se pudo realizar el registro. Por favor, inténtelo nuevamente.", "Error al agregar el comentario");
                        }
                        else {
                            App.ShowMessageInformation(Name + "El comentario se ha publicado correctamente.", "Comentario añadido");
                            CommentUserControl commentUser = new CommentUserControl();
                            commentUser.SetData(logbookModel);
                            lstCommentsOfTheDay.Items.Add(commentUser);

                            txtAddCommentary.Text = "";
                        }
                }
                
            } catch (Exception ex) {
                App.ShowMessageError("Error al agregar el comentario", "Error al agregar el comentario");
                LoggerManager.Instance.LogError("Error al agregar el comentario", ex);
            }
        }

        private void AddComments(LogbookModel logbook) {
            CommentUserControl commentUser = new CommentUserControl();
            commentUser.DeleteRequested += CommentUserControl_DeleteRequested;
            commentUser.SetData(logbook);
            if (logbook.Date.Date == DateTime.Now.Date){
                lstCommentsOfTheDay.Items.Add(commentUser);
            }
            else{
                lstComments.Items.Add(commentUser);
            }
              
        }



        private void ShowComments() {
            lstComments.Items.Clear();
            lstCommentsOfTheDay.Items.Clear();

            LogbookController logbookController = new LogbookController();
            EmployeeController employeeController = new EmployeeController();
            string noPersonal = employeeController.GetNoPersonalEmployee(SingletonClass.Instance.Email);
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


        private void CurrentDateNow() {
             string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            txtbDate.Text = currentDate;
        }

        private void SelectedItem() {
            List<string> items = new List<string>() {
                "Merma",
                "Incidencia",
                "General",
                "Otro"
            };
            cmbSection.ItemsSource = items;
            int selectedIndex = 2;
            cmbSection.SelectedIndex = selectedIndex;
            if (selectedIndex >= 0 && selectedIndex < cmbSection.Items.Count) {
                string selectedItem = cmbSection.Items[selectedIndex].ToString();
                cmbSection.SelectedItem = selectedItem;
            }
        }

        public bool ValidateText(string text) {
            string pattern = @"\S";
            return Regex.IsMatch(text, pattern);
        }

        private void txtAddCommentary_TextChanged(object sender, TextChangedEventArgs e) {
            lblCharacterCount.Content = txtAddCommentary.Text.Length;
        }

        private void CommentUserControl_DeleteRequested(object sender, int idCommentary){
            LogbookModel logbookModel = new LogbookModel();
            foreach (object item in lstCommentsOfTheDay.Items){
                if (item is CommentUserControl commentUserControl && commentUserControl.IdComment(logbookModel) == idCommentary) {

                    lstCommentsOfTheDay.Items.Remove(item);
                    break; 
                }
            }
        }
    }
}