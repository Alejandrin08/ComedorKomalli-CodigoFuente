using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Usercontrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Collections.Specialized.BitVector32;

namespace KomalliEmployee.Views.Pages
{
    /// <summary>
    /// Interaction logic for EditLogbook.xaml
    /// </summary>
    public partial class EditLogbook : Page{
        int _idComment;
        public EditLogbook(int idComment, string section, string commentary) {
            InitializeComponent();
            SelectedItem();
            ShowComments();
            CurrentDateNow();
            _idComment = idComment;
            cmbSection.SelectedItem = section;
            txtAddCommentary.Text = commentary;
        }

        private void ClickModify(object sender, RoutedEventArgs e) {
            try {
                string newSection = cmbSection.SelectedItem.ToString();
                string newCommentary = txtAddCommentary.Text;

                if (txtAddCommentary.Text.Length > 255) {
                    App.ShowMessageWarning("El comentario no puede exceder los 255 caracteres.", "Error al actualizar el comentario");
                    txtAddCommentary.Text = "";
                }
                else if (!ValidateText(txtAddCommentary.Text)) {
                    App.ShowMessageWarning("Comentario no valido.", "Error al actualizar el comentario");
                    txtAddCommentary.Text = "";
                }
                else {
                    LogbookModel logbook = new LogbookModel {
                        Section = newSection,
                        Commentary = newCommentary
                    };

                    LogbookController logbookController = new LogbookController();
                    int result = logbookController.UpdateComment(logbook, _idComment);

                    if (result == -1) {
                        App.ShowMessageWarning("No se pudo actualizar el comentario. Por favor, inténtelo nuevamente.", "Error al actualizar el comentario");
                    }
                    else {
                        App.ShowMessageInformation(Name + "El comentario se ha actualizado correctamente.", "Comentario actualizado");


                        if (NavigationService.GetNavigationService(this) is NavigationService navigationService) {
                            if (navigationService.CanGoBack) {
                                navigationService.GoBack();
                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                App.ShowMessageError("Error al agregar el comentario", "Error al agregar el comentario");
                LoggerManager.Instance.LogError("Error al agregar el comentario", ex);
            }
        }

        private void ClickCancel(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show(
                "¿Está seguro de que desea cancelar la modificación?",
                "Confirmar cancelación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes) {
                if (NavigationService.GetNavigationService(this) is NavigationService navigationService) {
                    if (navigationService.CanGoBack) {
                        navigationService.GoBack();
                    }
                }
            }
        }

        private void AddComments(LogbookModel logbook) {
            CommentUserControl commentUser = new CommentUserControl();
            commentUser.SetData(logbook);
            if (logbook.Date.Date == DateTime.Now.Date) {
                lstCommentsOfTheDay.Items.Add(commentUser);
            }
            else {
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
        }

        public bool ValidateText(string text) {
            string pattern = @"\S";
            return Regex.IsMatch(text, pattern);
        }

        private void txtAddCommentary_TextChanged(object sender, TextChangedEventArgs e) {
            lblCharacterCount.Content = txtAddCommentary.Text.Length;
        }

    }
}
