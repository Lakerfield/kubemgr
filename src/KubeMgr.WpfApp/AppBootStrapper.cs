using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Caliburn.Micro;
using KubeMgr.WpfApp.Helpers;
using KubeMgr.WpfApp.ViewModels;

namespace KubeMgr.WpfApp
{
  public class AppBootstrapper : BootstrapperBase
  {
    SimpleContainer _container;

    public AppBootstrapper()
    {
      Initialize();
    }

    protected override void Configure()
    {
      var traceListener = new WpfTraceListener();
#if DEBUG
      //PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
      PresentationTraceSources.DataBindingSource.Listeners.Add(traceListener);
#endif

      _container = new SimpleContainer();

      _container.Singleton<IWindowManager, WindowManager>();
      _container.Singleton<IEventAggregator, EventAggregator>();
      _container.RegisterInstance(typeof(ShellViewModel), null, new ShellViewModel(traceListener));

      System.Net.ServicePointManager.DefaultConnectionLimit = 20;
      System.Net.ServicePointManager.Expect100Continue = false;
      System.Net.ServicePointManager.ReusePort = true;
      System.Net.ServicePointManager.UseNagleAlgorithm = false;
    }

    protected override object GetInstance(Type service, string key)
    {
      var instance = _container.GetInstance(service, key);
      if (instance != null)
        return instance;

      throw new InvalidOperationException("Could not locate any instances.");
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      _container.BuildUp(instance);
    }

    protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
    {
      DisplayRootViewFor<ShellViewModel>();
    }
  }
}
