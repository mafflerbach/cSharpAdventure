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
using System.Windows.Shapes;

namespace WpfApplication1 {
  /// <summary>
  /// Interaction logic for ConfigurationWindow.xaml
  /// </summary>
  public partial class ConfigurationWindow : Window {
    public ConfigurationWindow() {
      InitializeComponent();
    }

    private void cancel_Click(object sender, RoutedEventArgs e) {
      Close();
    }

    private void save_Click(object sender, RoutedEventArgs e) {
      Properties.Settings.Default.API_KEY = ApiTextBox.Text;
      Console.WriteLine(Properties.Settings.Default.API_KEY);
      Close();
    }
  }
}
