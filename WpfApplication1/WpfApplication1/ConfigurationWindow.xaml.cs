using System;
using System.Windows;

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
