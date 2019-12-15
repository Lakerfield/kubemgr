using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KubeClient.Models;
using KubeMgr.Models;
using KubeMgr.WpfApp.Controls;
using KubeMgr.WpfApp.Views;

using Entity = KubeClient.Models.NamespaceV1;

namespace KubeMgr.WpfApp.ViewModels
{
  public class NamespacesViewModel : TabBaseWithViewViewModel<Entity>
  {
    private ViewItem<Entity> _selected;
    public ViewItem<Entity> Selected
    {
      get => _selected;
      set
      {
        _selected = value;
        NotifyOfPropertyChange();

        Namespace = _selected?.Name;
      }
    }

    public NamespacesViewModel(Cluster cluster, string labelFilter = null)
      : base(cluster, "Namespaces", labelFilter: labelFilter)
    {
    }

    protected override Task<View<Entity>> GetView()
    {
      return Cluster.GetNamespacesView(LabelFilter);
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
