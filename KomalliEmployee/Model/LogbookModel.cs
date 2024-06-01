using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class LogbookModel {
        public int IdCommentary { get; set; }
        public DateTime Date { get; set; }
        public string Section { get; set; }
        public string Commentary { get; set; }
        public string NoPersonalEmployee { get; set; }
    }
}
