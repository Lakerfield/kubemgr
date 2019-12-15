using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using KubeClient.Models;

namespace KubeMgr.WpfApp.Converters
{
  public class NodeAddressListToAddressConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var type = (string) parameter;
      var list = (List<NodeAddressV1>)value;
      var nodeAddress = list?.FirstOrDefault(e => e.Type == type);
      return nodeAddress?.Address ?? "- none -";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
