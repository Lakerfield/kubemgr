using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caliburn.Micro;
using KubeMgr.WpfApp.ViewModels;

namespace KubeMgr.WpfApp.Views
{
  /// <summary>
  /// Interaction logic for NodesView.xaml
  /// </summary>
  public partial class NodesView : UserControl
  {
    public NodesView()
    {
      InitializeComponent();
    }

    private void DgRows_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var shell = IoC.Get<ShellViewModel>();

      var viewmodel = DataContext as NodesViewModel;
      var viewItem = viewmodel.Selected;
      if (viewItem == null)
        return;

      var tab = new NodeViewModel(viewmodel.Cluster, viewItem);
      shell.OpenTab(tab);
    }
  }
}
