using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using KubeClient;
using KubeClient.Models;
using Microsoft.Extensions.Logging;

namespace KubeMgr.Models
{
  public class Cluster
  {
    public KubeClientOptions Options { get; }
    public IScheduler Scheduler { get; }
    private CompositeDisposable _subscriptions;
    private readonly KubeApiClient _client;
    public bool Active { get; private set; }
    public ILogger ErrorLog { get; }

    public Cluster(ILoggerFactory loggers, KubeClientOptions options, IScheduler scheduler)
    {
      Options = options;
      Scheduler = scheduler;
      _client = KubeApiClient.Create(options);
      _subscriptions = new CompositeDisposable();

      ErrorLog = loggers.CreateLogger("Exception");

      Start();
    }

    public void Start()
    {
      Active = true;
    }

    public void Stop()
    {
      var subscriptions = _subscriptions;
      _subscriptions = new CompositeDisposable();
      subscriptions.Dispose();
      Active = false;
    }

    public Task<View<NodeV1>> GetNodesView(string labelSelector = null)
    {
      var view = new View<NodeV1>();

      var subscription = _client
        .NodesV1()
        .WatchAll(labelSelector)
        .ObserveOn(Scheduler)
        .Subscribe(view);
      
      _subscriptions.Add(subscription);

      view.AddDisposable(Disposable.Create(() =>
      {
        subscription.Dispose();
        _subscriptions.Remove(subscription);
      }));

      return Task.FromResult(view);
    }

    public async Task<View<NamespaceV1>> GetNamespacesView(string labelSelector = null)
    {
      var namespaces = await _client.NamespacesV1().List(labelSelector);
      
      var view = new View<NamespaceV1>(namespaces.Items);
      return view;
    }

    public Task<View<PodV1>> GetPodsView(string labelSelector = null, string @namespace = null)
    {
      var view = new View<PodV1>();

      var subscription = _client
        .PodsV1()
        .WatchAll(labelSelector, @namespace)
        .ObserveOn(Scheduler)
        .Subscribe(view);
      
      _subscriptions.Add(subscription);

      view.AddDisposable(Disposable.Create(() =>
      {
        subscription.Dispose();
        _subscriptions.Remove(subscription);
      }));

      return Task.FromResult(view);
    }

    public async Task Delete(PodV1 entity)
    {
      var result = await _client.PodsV1().Delete(entity.Metadata.Name, entity.Metadata.Namespace, DeletePropagationPolicy.Background);

      return;
    }
  }


}
