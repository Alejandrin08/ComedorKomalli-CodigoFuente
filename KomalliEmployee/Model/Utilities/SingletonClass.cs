using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model.Utilities {
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
        public string UserName { get; set; }
        public string PersonalNumberUserSelected { get; set; }
        public string EmailUserSelected { get; set; }
        public string AvailabilityUserSelected { get; set; }
        public string UserNameOrder {  get; set; }
        public int TotalOrder { get; set; }
        public string IdFoodOrderSelected {  get; set; }
        public int Price { get; set; }
        public ObservableCollection<FoodModel> SelectedFoods { get; } = new ObservableCollection<FoodModel>();
    }
}
