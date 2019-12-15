using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubeMgr.WpfApp.Settings
{
  [Serializable]
  public class DataRoot
  {
    private AppSettings _appSettings;
    public AppSettings AppSettings
    {
      get => _appSettings ??= new AppSettings();
      set => _appSettings = value;
    }

    private ObservableCollection<ClusterConnection> _clusterConnections;
    public ObservableCollection<ClusterConnection> ClusterConnections
    {
      get => _clusterConnections ??= new ObservableCollection<ClusterConnection>();
      set => _clusterConnections = value;
    }


  }
}
