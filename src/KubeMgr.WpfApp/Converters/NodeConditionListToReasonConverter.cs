using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using KubeClient.Models;

namespace KubeMgr.WpfApp.Converters
{
  public class NodeConditionListToReasonConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var type = (string) parameter;
      var list = (List<NodeConditionV1>)value;
      var nodeCondition = list?.FirstOrDefault(e => e.Type == type);
      return nodeCondition?.Reason ?? "- none -";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
