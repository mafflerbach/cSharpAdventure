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


namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_OnInitialized(object sender, EventArgs e)
        {
            List<Gw.ItemList> items = new List<Gw.ItemList>();
            items.Add(new Gw.ItemList() { Title = "Complete this WPF tutorial" , Image = "Images/Bandage.png" });
            items.Add(new Gw.ItemList() { Title = "Learn C#", Image = "Images/Bandage.png" });
            items.Add(new Gw.ItemList() { Title = "Wash the car", Image = "Images/Bandage.png" });

            ListBox.ItemsSource = items;
            
        }
    }


}
