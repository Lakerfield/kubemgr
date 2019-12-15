using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using KubeClient.Models;
using KubeMgr.Models;

namespace KubeMgr.WpfApp.ViewModels
{
  public abstract class TabBaseViewModel : Screen
  {
    private string _tabTitle;
    public string TabTitle
    {
      get => _tabTitle;
      set
      {
        _tabTitle = value;
        NotifyOfPropertyChange();
      }
    }

    public Models.Cluster Cluster { get; protected set; }

    private string _namespace;
    public string Namespace
    {
      get => _namespace;
      set
      {
        _namespace = value;
        NotifyOfPropertyChange();
      }
    }

    private string _labelFilter;
    public string LabelFilter
    {
      get => _labelFilter;
      set
      {
        _labelFilter = value;
        NotifyOfPropertyChange();
      }
    }

    protected TabBaseViewModel(Cluster cluster, string tabTitle, string @namespace = null, string labelFilter = null)
    {
      Cluster = cluster;
      TabTitle = tabTitle;
      Namespace = @namespace;
      LabelFilter = labelFilter;
    }
  }

  public abstract class TabBaseWithViewViewModel<T> : TabBaseViewModel where T : KubeResourceV1
  {

    private View<T> _view;
    public View<T> View
    {
      get => _view;
      set
      {
        _view = value;
        NotifyOfPropertyChange();
      }
    }

    protected TabBaseWithViewViewModel(Cluster cluster, string tabTitle, string @namespace = null, string labelFilter = null)
      : base(cluster, tabTitle, @namespace, labelFilter)
    {
    }

    protected override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
      await base.OnActivateAsync(cancellationToken);

      View = await GetView();
    }

    protected abstract Task<View<T>> GetView();

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
      View.Dispose();

      return base.OnDeactivateAsync(close, cancellationToken);
    }

  }
}
