using Caliburn.Micro;
using KubeMgr.WpfApp.Settings;

namespace KubeMgr.WpfApp.ViewModels.Settings
{
  public class ClusterConnectionViewModel : Screen
  {
    public override string DisplayName
    {
      get { return "Cluster settings"; }
      set { }
    }

    private ClusterConnection _connection;
    public ClusterConnection Connection
    {
      get { return _connection; }
      set
      {
        if (_connection != null)
          _connection.PropertyChanged -= ClusterConnectionPropertyChanged;
        _connection = value;
        NotifyOfPropertyChange();
        if (_connection != null)
          _connection.PropertyChanged += ClusterConnectionPropertyChanged;
      }
    }

    public ClusterConnectionViewModel(ClusterConnection connection)
    {
      Connection = connection;
    }

    void ClusterConnectionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      NotifyOfPropertyChange(() => CanOk);
    }




    public async void Ok()
    {
      if (!CanOk)
        return;
      await TryCloseAsync(true);
    }

    public bool CanOk
    {
      get { return Connection.IsValid(); }
    }

    public async void Cancel()
    {
      await TryCloseAsync(false);
    }

  }
}
