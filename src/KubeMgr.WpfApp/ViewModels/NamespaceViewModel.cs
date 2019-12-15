using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KubeMgr.Models;
using KubeMgr.WpfApp.Controls;
using KubeMgr.WpfApp.Views;

using Entity = KubeClient.Models.NamespaceV1;

namespace KubeMgr.WpfApp.ViewModels
{
  public class NamespaceViewModel : TabBaseViewModel
  {
    public Entity Entity { get; }

    public NamespaceViewModel(Cluster cluster, Entity entity) : base(cluster, entity.Metadata.Name, entity.Metadata.Name)
    {
      Entity = entity;
    }

    private ObjectViewModelHierarchy _objectHierarchy;
    public ObjectViewModelHierarchy ObjectHierarchy
    {
      get
      {
        if (_objectHierarchy == null)
          _objectHierarchy = new ObjectViewModelHierarchy(Entity);
        return _objectHierarchy;
      }
    }

  }
}
