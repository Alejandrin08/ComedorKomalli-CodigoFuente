using KomalliEmployee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Contracts {
    public interface ILogbook {

        public int AddCommentary(LogbookModel logbookModel);
        public List<LogbookModel> GetEmployeeComments(string noPersonal);
        public int GetCommentId(DateTime date, string comment);
        public int DeleteCommentary(int idComment);
    }
}
