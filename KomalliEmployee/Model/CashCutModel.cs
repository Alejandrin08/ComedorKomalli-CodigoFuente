using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class CashCutModel {

        public DateTime Date { get; set; }
        public int InitialBalance { get; set; }
        public int TotalEntries { get; set; }   
        public int TotalExits { get; set; } 
        public int Balance { get; set; }    
    }
}
