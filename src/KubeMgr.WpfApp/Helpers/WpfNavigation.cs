using System.Windows;
using System.Windows.Media;

namespace KubeMgr.WpfApp.Helpers
{
  public static class WpfNavigation
  {

    public static T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
      //get parent item
      DependencyObject parentObject = VisualTreeHelper.GetParent(child);

      //we've reached the end of the tree
      if (parentObject == null) return null;

      //check if the parent matches the type we're looking for
      T parent = parentObject as T;
      if (parent != null)
        return parent;

      return FindParent<T>(parentObject);
    }

    public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
    {
      var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

      for (int i = 0; i < childrenCount; i++)
      {
        var childObject = VisualTreeHelper.GetChild(parent, i);
        if (childObject == null) 
          continue;

        T child = childObject as T;
        if (child != null)
          return child;

        child = FindChild<T>(childObject);
        if (child != null)
          return child;
      }
      return null;
    }




  }
}
