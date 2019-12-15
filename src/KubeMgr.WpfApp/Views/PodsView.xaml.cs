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
  /// Interaction logic for PodsView.xaml
  /// </summary>
  public partial class PodsView : UserControl
  {
    public PodsView()
    {
      InitializeComponent();
    }

    private void DgRows_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var shell = IoC.Get<ShellViewModel>();

      var viewmodel = DataContext as PodsViewModel;
      var viewitem = viewmodel.Selected;
      if (viewitem == null)
        return;

      var tab = new PodViewModel(viewmodel.Cluster, viewitem);
      shell.OpenTab(tab);
    }
  }
}
