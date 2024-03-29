using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubeMgr.WpfApp.Controls
{
  /// <summary>
  /// void DisplayObjectGraph(object graph)
  /// {
  ///   var hierarchy = new ObjectViewModelHierarchy(graph);
  ///   tvObjectGraph.DataContext = hierarchy;
  /// }
  /// </summary>
  public class ObjectViewModelHierarchy
  {
    readonly ReadOnlyCollection<ObjectViewModel> _firstGeneration;
    readonly ObjectViewModel _rootObject;

    public ObjectViewModelHierarchy(object rootObject)
    {
      _rootObject = new ObjectViewModel(rootObject);
      _firstGeneration = new ReadOnlyCollection<ObjectViewModel>(new ObjectViewModel[] { _rootObject });
    }

    public ReadOnlyCollection<ObjectViewModel> FirstGeneration
    {
      get { return _firstGeneration; }
    }
  }
}
