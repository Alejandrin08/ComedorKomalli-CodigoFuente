using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model {
    public class SingletonClass {

        private static SingletonClass _instance;
        public static SingletonClass Instance {
            get {
                if (_instance == null) {
                    _instance = new SingletonClass();
                }
                return _instance;
            }
        }

        public string Email { get; set; }
    }
}
