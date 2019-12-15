using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using KubeMgr.WpfApp.Settings;

namespace KubeMgr.WpfApp.ViewModels.Settings
{
  public class ClusterConnectionsViewModel : Screen
  {
    public override string DisplayName
    {
      get { return "Clusters"; }
      set { }
    }

    public ObservableCollection<ClusterConnection> Connections { get; set; }

    private ClusterConnection _selectedConnection;
    public ClusterConnection SelectedConnection
    {
      get { return _selectedConnection; }
      set
      {
        if (Equals(value, _selectedConnection)) return;
        _selectedConnection = value;
        NotifyOfPropertyChange();
        NotifyOfPropertyChange(() => CanEdit);
        NotifyOfPropertyChange(() => CanCopy);
        NotifyOfPropertyChange(() => CanDelete);
        NotifyOfPropertyChange(() => CanSelect);
      }
    }

    public ClusterConnectionsViewModel()
    {
      Connections = DataManager.Current.Root.ClusterConnections;
    }

    public async void New()
    {
      var connection = new ClusterConnection();
      if (await Edit(connection))
        Connections.Add(connection);
    }



    public async Task<bool> Edit(ClusterConnection connection = null)
    {
      connection = connection ?? SelectedConnection;
      if (connection == null)
        return false;

      var viewmodel = new ClusterConnectionViewModel(connection);

      var windowManager = IoC.Get<IWindowManager>();
      if (await windowManager.ShowDialogAsync(viewmodel) == true)
        return true;
      return false;
    }

    public bool CanEdit
    {
      get { return SelectedConnection != null; }
    }



    public async Task<bool> Copy()
    {
      var original = SelectedConnection;
      if (original == null)
        return false;

      var copy = original.Clone();
      if (await Edit(copy))
        Connections.Add(copy);
      else
        return false;
      return true;
    }

    public bool CanCopy
    {
      get { return SelectedConnection != null; }
    }



    public void Delete()
    {
      var connection = SelectedConnection;
      if (connection != null)
        Connections.Remove(connection);
    }

    public bool CanDelete
    {
      get { return SelectedConnection != null; }
    }



    public async void Select()
    {
      await TryCloseAsync(true);
    }

    public bool CanSelect
    {
      get { return SelectedConnection != null; }
    }

    protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
      DataManager.Current.Save();

      await base.OnDeactivateAsync(close, cancellationToken);
    }
  }
}
