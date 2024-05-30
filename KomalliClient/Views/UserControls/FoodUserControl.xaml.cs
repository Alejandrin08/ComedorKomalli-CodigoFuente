using KomalliClient.Model;
using KomalliClient.Model.Utilities;
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

namespace KomalliClient.Views.UserControls {
    /// <summary>
    /// Lógica de interacción para FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl {
        public static Dictionary<string, bool> SelectedStates { get; set; } = new Dictionary<string, bool>();
        public FoodModel Food { get; set; } 
        public FoodUserControl() {
            InitializeComponent();
            Loaded += LoadesFoodUserControll;
        }
        private void LoadesFoodUserControll(object sender, RoutedEventArgs e) {
            if (Food != null && SelectedStates.ContainsKey(Food.Name)) {
                IsSelected = SelectedStates[Food.Name];
            }
        }

        private bool _isSelected = false;
        public bool IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                if (Food != null) {
                    SelectedStates[Food.Name] = value;
                }
                if (value) {
                    this.IsEnabled = false;
                    this.Opacity = 0.5;
                } else {
                    this.IsEnabled = true;
                    this.Opacity = 1.0;
                }
            }
        }

        public void BindData() {
            if (Food != null) {
                tbkFoodName.Text = Food.Name;
                tbkFoodPrice.Text = "$" + Food.Price.ToString();
                imgFoodImage.Source = new BitmapImage(new Uri(Food.Image));
            }
        }

        private void ClickAddFood(object sender, RoutedEventArgs e) {
            int price = int.Parse(tbkFoodPrice.Text.TrimStart('$')); 
            FoodModel foodModel = new FoodModel() {
                Name = tbkFoodName.Text,
                KeyCard = Food.KeyCard,
                Price = price,
                IsSelected = true,
                Section = Food.Section,
                Quantity = 1,
                Subtotal = price
            };
            SingletonClass.Instance.SelectedFoods.Add(foodModel);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.btnContinue.IsEnabled = true;
            mainWindow.btnCancel.IsEnabled = true;
        }
    }
}
