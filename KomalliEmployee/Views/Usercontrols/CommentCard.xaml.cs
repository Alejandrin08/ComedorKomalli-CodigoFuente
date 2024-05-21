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

namespace KomalliEmployee.Views.Usercontrols {
    /// <summary>
    /// Lógica de interacción para CommentCard.xaml
    /// </summary>
    public partial class CommentCard : UserControl {
        public CommentCard() {
            InitializeComponent();
        }

        public void setDataCard(LogbookModel logbook) {
            lblContentComment.Content = logbook.Commentary;
            string employeeName = SingletonClass.Instance.UserName;
            lblUserName.Content = employeeName;
            lblUserName.Content = logbook.NoPersonalEmployee;
            lblDate.Content = logbook.Date;
            lblSection.Content = logbook.Section;
        }
    }
}