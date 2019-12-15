using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using KubeClient.Models;

namespace KubeMgr.WpfApp.Converters
{
  public class CapacityDictionaryToValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var key = (string) parameter;
      var list = (Dictionary<string,string>)value;
      var pair = list?.FirstOrDefault(e => e.Key == key);

      var result = pair?.Value ?? "";
      if (result.EndsWith("Ki") && int.TryParse(result.Substring(0, result.Length - 2), out int number) && number > 32768)
      {
        number = number / 1024;
        result = $" {number:n0} MB";
      }

      return " " + result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
