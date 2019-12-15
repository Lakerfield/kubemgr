using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using KubeClient.Models;

namespace KubeMgr.WpfApp.Converters
{
  public class LabelsDictionaryToRoleConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var type = (string) parameter;
      var list = (Dictionary<string,string>)value;
      var workerLabel = list?.FirstOrDefault(e => e.Key == "node-role.kubernetes.io/worker");
      if (string.Equals(workerLabel?.Value, "true", StringComparison.OrdinalIgnoreCase))
        return "worker";
      var masterLabel = list?.FirstOrDefault(e => e.Key == "node-role.kubernetes.io/master");
      if (string.Equals(masterLabel?.Value, "true", StringComparison.OrdinalIgnoreCase))
        return "master";
      return "- unknown -";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
