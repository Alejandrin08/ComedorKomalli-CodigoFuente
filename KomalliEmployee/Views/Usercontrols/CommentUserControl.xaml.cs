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

namespace KomalliEmployee.Views.Usercontrols
{
    /// <summary>
    /// Interaction logic for CommentUserControl.xaml
    /// </summary>
    public partial class CommentUserControl : UserControl {
        public event EventHandler<int> DeleteRequested;
        public LogbookModel Logbook { get; set; }
        public CommentUserControl() {
            InitializeComponent();
        }

        public void SetData(LogbookModel logbook) {
            Logbook = logbook;

            if (logbook != null) {
                lblContentComment.Content = logbook.Commentary;
                string employeeName = SingletonClass.Instance.UserName;
                lblUserName.Content = employeeName;
                lblDate.Content = logbook.Date.ToString("dd/MM/yyyy");
                lblSection.Content = logbook.Section;

                if (logbook.Date.Date == DateTime.Now.Date) {
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
                else {
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                }

            }

        }

        public int IdComment(LogbookModel logbook) {
            int idCommentary = 0;
            Logbook = logbook;
            if (logbook != null) {
                LogbookController logbookController = new LogbookController();
                lblContentComment.Content = logbook.Commentary;
                lblDate.Content = logbook.Date.ToString("dd/MM/yyyy");
                idCommentary = logbookController.GetCommentId(logbook.Date, logbook.Commentary);
            }
            return idCommentary;
        }

        private void RemoveComment(int idCommentary) {
            LogbookController logbookController = new LogbookController();
            logbookController.DeleteCommentary(idCommentary);
        }


        public void ClickEdit(object sender, RoutedEventArgs e) {


        }

        public void ClickDelete(object sender, RoutedEventArgs e) {
             
                if (Logbook != null) {
                int idCommentary = IdComment(Logbook); 
                        DeleteRequested?.Invoke(this, idCommentary);
                        if (idCommentary != 0) {

                            RemoveComment(idCommentary);

                        }

                    
                }          
            

        }
    }
}
