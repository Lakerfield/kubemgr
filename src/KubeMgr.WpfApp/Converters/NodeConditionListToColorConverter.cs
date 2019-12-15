using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using KubeClient.Models;

namespace KubeMgr.WpfApp.Converters
{
  public class NodeConditionListToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var type = (string)parameter;
      var list = (List<NodeConditionV1>)value;
      var nodeCondition = list?.FirstOrDefault(e => e.Type == type);
      var ready = nodeCondition?.Status;
      if (string.IsNullOrWhiteSpace(ready))
        return Brushes.Transparent;
      if (string.Equals(ready, "false", StringComparison.OrdinalIgnoreCase))
        return Brushes.Green;
      if (string.Equals(ready, "true", StringComparison.OrdinalIgnoreCase))
        return Brushes.Red;
      return Brushes.Orange;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

  }
}
