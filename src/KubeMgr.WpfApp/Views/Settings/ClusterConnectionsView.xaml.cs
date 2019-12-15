using System.Windows;
using System.Windows.Input;
using KubeMgr.WpfApp.ViewModels.Settings;

namespace KubeMgr.WpfApp.Views.Settings
{
  /// <summary>
  /// Interaction logic for ClusterConnectionsView.xaml
  /// </summary>
  public partial class ClusterConnectionsView : Window
  {

    public ClusterConnectionsViewModel ViewModel
    {
      get { return DataContext as ClusterConnectionsViewModel; }
      set { DataContext = value; }
    }


    public ClusterConnectionsView()
    {
      InitializeComponent();
    }

    private void ConnectionDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      ViewModel.Select();
    }
  }
}
