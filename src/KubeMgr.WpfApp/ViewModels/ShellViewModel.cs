using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using KubeMgr.WpfApp.Helpers;
using KubeMgr.WpfApp.ViewModels.Settings;

namespace KubeMgr.WpfApp.ViewModels
{
  public class ShellViewModel : Conductor<TabBaseViewModel>.Collection.AllActive
  {

    private TabBaseViewModel _selectedTab;
    public TabBaseViewModel SelectedTab
    {
      get => _selectedTab;
      set
      {
        _selectedTab = value;
        NotifyOfPropertyChange();
        NotifyOfPropertyChange(nameof(CanOpenNodes));
        NotifyOfPropertyChange(nameof(CanOpenNamespaces));
        NotifyOfPropertyChange(nameof(CanOpenPods));
      }
    }

    private string _name;
    public string Name
    {
      get { return _name; }
      set
      {
        _name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }



    public async void OpenCluster()
    {
      var settings = new ClusterConnectionsViewModel();

      dynamic windowSettings = new ExpandoObject();
      //System.Windows.Window settings;
      windowSettings.WindowState = WindowState.Maximized;
      windowSettings.SizeToContent = SizeToContent.Manual;

      var windowManager = IoC.Get<IWindowManager>();
      if (await windowManager.ShowDialogAsync(settings, null, windowSettings) != true)
        return;

      var connection = settings.SelectedConnection;
      if (connection == null)
        return;

      var tab = new ClusterViewModel(connection.Clone());
      await OpenTab(tab);
    }



    public bool CanOpenNodes => SelectedTab?.Cluster != null;
    public async void OpenNodes()
    {
      var tab = new NodesViewModel(SelectedTab.Cluster);
      await OpenTab(tab);
    }


    public bool CanOpenNamespaces => SelectedTab?.Cluster != null;
    public async void OpenNamespaces()
    {
      var tab = new NamespacesViewModel(SelectedTab.Cluster);
      await OpenTab(tab);
    }


    public bool CanOpenPods => SelectedTab?.Cluster != null;
    public async void OpenPods()
    {
      var tab = new PodsViewModel(SelectedTab?.Cluster, SelectedTab?.Namespace, SelectedTab?.LabelFilter);
      await OpenTab(tab);
    }



    public async void OpenTest()
    {
      var testViewModel = new TestViewModel();
      await OpenTab(testViewModel);
    }





    public ObservableCollection<string> DataBindingErrors { get; private set; }

    public ShellViewModel(WpfTraceListener traceListener)
    {
      DisplayName = "Lakerfield's KubeMgr";

      DataBindingErrors = new ObservableCollection<string>();
      if (traceListener != null)
        traceListener.TraceCatched += s =>
        {
          if (DataBindingErrors == null) return;
          DataBindingErrors.Insert(0, s);
          while (DataBindingErrors.Count > 5)
            DataBindingErrors.RemoveAt(DataBindingErrors.Count - 1);
        };

      TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    public void ClearDataBindingErrors()
    {
      DataBindingErrors.Clear();
    }

    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
    {
      var exception = unhandledExceptionEventArgs.ExceptionObject as Exception;
      if (exception != null)
      {
        MessageBox.Show(
          string.Format(@"Exeption:{2}{0}{2}{2}Stacktrace:{2}{1}",
            exception.Message,
            exception.StackTrace,
            Environment.NewLine));
      }
    }

    void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      var exception = e.Exception;
      MessageBox.Show(
        string.Format(@"Exeption:{2}{0}{2}{2}Stacktrace:{2}{1}",
          exception.Message,
          exception.StackTrace,
          Environment.NewLine));
      e.SetObserved();
    }





    public async Task OpenTab(TabBaseViewModel tab)
    {
      await ActivateItemAsync(tab, CancellationToken.None);
      SelectedTab = tab;
    }

    public async void CloseItem(TabBaseViewModel vm)
    {
      await DeactivateItemAsync(vm, true, CancellationToken.None);
    }

    protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
      await base.OnDeactivateAsync(close, cancellationToken);
    }

  }
}
