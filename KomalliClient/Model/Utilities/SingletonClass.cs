using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Model.Utilities {
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

        public int Price { get; set; }
        public ObservableCollection<FoodModel> SelectedFoods { get; } = new ObservableCollection<FoodModel>();
    }
}
