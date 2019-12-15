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
  public class PodsViewModel : TabBaseWithViewViewModel<Entity>
  {
    private ViewItem<Entity> _selected;
    public ViewItem<Entity> Selected
    {
      get => _selected;
      set
      {
        _selected = value;
        NotifyOfPropertyChange();
      }
    }

    public PodsViewModel(Cluster cluster, string @namespace = null, string labelFilter = null)
      : base(cluster, "Pods", @namespace, labelFilter)
    {
      if (string.IsNullOrWhiteSpace(@namespace))
        TabTitle = $"Pods in {cluster.Options?.ApiEndPoint}";
      else
        TabTitle = $"Pods in {@namespace}";

    }

    protected override Task<View<Entity>> GetView()
    {
      return Cluster.GetPodsView(LabelFilter, Namespace);
    }



    private ObjectViewModelHierarchy _objectHierarchy;
    public ObjectViewModelHierarchy ObjectHierarchy
    {
      get
      {
        if (_objectHierarchy == null)
          _objectHierarchy = new ObjectViewModelHierarchy(View);
        return _objectHierarchy;
      }
    }
  }
}
