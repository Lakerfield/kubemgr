using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KubeMgr.WpfApp.Settings;

namespace KubeMgr.WpfApp.Views.Settings
{
  /// <summary>
  /// Interaction logic for ClusterConnectionView.xaml
  /// </summary>
  public partial class ClusterConnectionView : Window
  {
    public ClusterConnectionView()
    {
      InitializeComponent();

      Loaded += ClusterConnectionViewLoaded;
    }

    void ClusterConnectionViewLoaded(object sender, RoutedEventArgs e)
    {
      AddComboBoxItem(ComboBoxUrlNoAuth,
        @"http://localhost:8001/",
        @"http://localhost:8080/");

      AddComboBoxItem(ComboBoxUrlBearer,
        @"https://localhost:6443/",
        @"http://localhost:8001/",
        @"http://localhost:8080/",
        @"https://10.43.0.1/");

      foreach (var groep in DataManager.Current.Root.ClusterConnections
          .Select(c => c.Group)
          .Where(g => !string.IsNullOrWhiteSpace(g))
          .Distinct())
        AddComboBoxItem(ComboBoxGroep, groep);
    }

    private void AddComboBoxItem(ComboBox comboBox, params string[] urls)
    {
      foreach (var url in urls)
        comboBox.Items.Add(new ComboBoxItem()
        {
          Content = url
        });
    }

  }
}
