using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClient.Model {
    public class FoodModel : INotifyPropertyChanged {

        private int _price;
        private string _name;
        private bool _isSelected;
        private int _quantity;
        private int _subtotal;

        public string KeyCard { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Section { get; set; }
        public int Price { get { return _price; } set { _price = value; OnPropertyChanged("Price"); } }
        public string Image { get; set; }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; OnPropertyChanged("IsSelected"); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        public int Subtotal { get { return _subtotal; } set { _subtotal = value; OnPropertyChanged("Subtotal"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
