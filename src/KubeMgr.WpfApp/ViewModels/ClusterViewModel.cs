using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KubeClient;
using KubeMgr.Models;
using KubeMgr.WpfApp.Controls;
using KubeMgr.WpfApp.Helpers;
using KubeMgr.WpfApp.Settings;
using Microsoft.Extensions.Logging;

namespace KubeMgr.WpfApp.ViewModels
{
  public class ClusterViewModel : TabBaseViewModel
  {
    public ObservableCollection<string> Log { get; set; }
    public ClusterConnection Settings { get; }
    public object View => null;

    public ClusterViewModel(ClusterConnection settings)
      : base(null, "cluster")
    {
      Settings = settings;
      Log = new ObservableCollection<string>();
      Log.Add("Init cluster");

      ILoggerFactory loggers = new LoggerFactory();
      loggers.AddProvider(new StringLoggerProvider(Log));

      var options = GetKubeClientOptions(settings);
      options.LoggerFactory = loggers;
      Cluster = new Cluster(loggers, options, System.Reactive.Concurrency.DispatcherScheduler.Current);
      TabTitle = Cluster.Options?.ApiEndPoint?.ToString() ?? "Cluster xxx";
      Namespace = settings.DefaultNamespace;
    }

    private KubeClientOptions GetKubeClientOptions(ClusterConnection settings)
    {
      switch (settings.Kind)
      {
        case ConfigKind.NoAuthentication:
          return new KubeClientOptions
          {
            ApiEndPoint = new Uri(settings.Url),
            AuthStrategy = KubeAuthStrategy.None,
            AllowInsecure = settings.AllowInsecure, // Don't validate server certificate
            KubeNamespace = settings.DefaultNamespace,
          };

        case ConfigKind.KubeConfigFile:
          return K8sConfig.Load(settings.KubeConfigFile).ToKubeClientOptions(
            kubeContextName: settings.DefaultContext,
            defaultKubeNamespace: settings.DefaultNamespace
          );

        case ConfigKind.BearerToken:
          return new KubeClientOptions
          {
            ApiEndPoint = new Uri(settings.Url),
            AuthStrategy = KubeAuthStrategy.BearerToken,
            AccessToken = settings.AccessToken,
            AllowInsecure = settings.AllowInsecure, // Don't validate server certificate
            KubeNamespace = settings.DefaultNamespace,
          };

        default:
          throw new ArgumentOutOfRangeException();
      }
    }


    public bool CanStart => !Cluster.Active;
    public void Start()
    {
      Cluster.Start();
      NotifyOfPropertyChange(nameof(CanStart));
      NotifyOfPropertyChange(nameof(CanStop));
    }

    public bool CanStop => Cluster.Active;
    public void Stop()
    {
      Cluster.Stop();
      NotifyOfPropertyChange(nameof(CanStart));
      NotifyOfPropertyChange(nameof(CanStop));
    }

    private ObjectViewModelHierarchy _objectHierarchy;
    public ObjectViewModelHierarchy ObjectHierarchy
    {
      get
      {
        if (_objectHierarchy == null)
          _objectHierarchy = new ObjectViewModelHierarchy(Cluster);
        return _objectHierarchy;
      }
    }


    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
      Cluster.Stop();

      return base.OnDeactivateAsync(close, cancellationToken);
    }
  }
}
