using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KubeMgr.Models;
using KubeMgr.WpfApp.Controls;
using KubeMgr.WpfApp.Views;

using Entity = KubeClient.Models.NodeV1;

namespace KubeMgr.WpfApp.ViewModels
{
  public class NodeViewModel : TabBaseViewModel
  {
    public ViewItem<Entity> ViewItem { get; }

    public NodeViewModel(Cluster cluster, ViewItem<Entity> viewItem) : base(cluster, viewItem.Entity.Metadata.Name)
    {
      ViewItem = viewItem;
    }

    private ObjectViewModelHierarchy _objectHierarchy;
    public ObjectViewModelHierarchy ObjectHierarchy
    {
      get
      {
        if (_objectHierarchy == null)
          _objectHierarchy = new ObjectViewModelHierarchy(ViewItem);
        return _objectHierarchy;
      }
    }

  }
}
