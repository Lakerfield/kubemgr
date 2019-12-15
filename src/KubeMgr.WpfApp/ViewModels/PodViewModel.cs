using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KubeMgr.Models;
using KubeMgr.WpfApp.Controls;
using KubeMgr.WpfApp.Views;

using Entity = KubeClient.Models.PodV1;

namespace KubeMgr.WpfApp.ViewModels
{
  public class PodViewModel : TabBaseViewModel
  {
    public ViewItem<Entity> ViewItem { get; }

    public PodViewModel(Cluster cluster, ViewItem<Entity> viewItem) : base(cluster, viewItem.Entity.Metadata.Name)
    {
      ViewItem = viewItem;
    }

    public async Task Delete()
    {
      await Cluster.Delete(ViewItem.Entity);
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
