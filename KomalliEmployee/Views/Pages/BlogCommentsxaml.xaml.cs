using KomalliEmployee.Controller;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Usercontrols;
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
    /// Interaction logic for BlogCommentsxaml.xaml
    /// </summary>
    public partial class BlogCommentsxaml : Page {
        public BlogCommentsxaml() {
            InitializeComponent();
            SelectedItem();
        }

        private void AddComments(LogbookModel logbook) {
            CommentCard commentUser = new CommentCard();
            commentUser.setDataCard(logbook);
            lstComments.Items.Add(commentUser);

        }



        private void ShowComments() {
            lstComments.Items.Clear();

            LogbookController logbookController = new LogbookController();
            List<LogbookModel> comments = logbookController.GetAllComments();

            if (comments.Any())
            {
                foreach (LogbookModel logbook in comments)
                {
                    AddComments(logbook);
                }
            }
        }

        private void SelectedItem() {
            List<string> items = new List<string>() {
                "Merma",
                "Incidencia",
                "General",
                "Otro",
                "Todos"
            };
            cmbSection.ItemsSource = items;
            int selectedIndex = 4;
            cmbSection.SelectedIndex = selectedIndex;
            if (selectedIndex >= 0 && selectedIndex < cmbSection.Items.Count) {
                string selectedItem = cmbSection.Items[selectedIndex].ToString();
                cmbSection.SelectedItem = selectedItem;
            }
        }

        private void ShowFilteredComments(string section)
        {
            lstComments.Items.Clear();

            LogbookController logbookController = new LogbookController();
            List<LogbookModel> comments = logbookController.GetAllComments();

            if (section == "Todos")
            {
                if (comments.Any())
                {
                    foreach (LogbookModel logbook in comments)
                    {
                        AddComments(logbook);
                    }
                }
               
            }
            else
            {
                var filteredComments = comments.Where(c => c.Section == section).ToList();
                if (filteredComments.Any())
                {
                    foreach (LogbookModel logbook in filteredComments)
                    {
                        AddComments(logbook);
                    }
                }
                
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSection.SelectedItem != null)
            {
                string selectedSection = cmbSection.SelectedItem.ToString();
                ShowFilteredComments(selectedSection);
            }
        }
    }
}
