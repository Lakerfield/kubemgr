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
  /// Interaction logic for NamespacesView.xaml
  /// </summary>
  public partial class NamespacesView : UserControl
  {
    public NamespacesView()
    {
      InitializeComponent();
    }

    private void DgRows_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var shell = IoC.Get<ShellViewModel>();

      var viewmodel = DataContext as NamespacesViewModel;
      var entity = viewmodel.Selected.Entity;
      if (entity == null)
        return;

      var tab = new NamespaceViewModel(viewmodel.Cluster, entity);
      shell.OpenTab(tab);
    }
  }
}
