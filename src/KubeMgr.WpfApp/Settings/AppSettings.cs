using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace KubeMgr.WpfApp.Settings
{
  public class AppSettings : PropertyChangedBase
  {
    public const string DefaultApiBaseUrl = @"";

    private string _apiBaseUrl = DefaultApiBaseUrl;
    public string ApiBaseUrl
    {
      get { return _apiBaseUrl; }
      set
      {
        _apiBaseUrl = value;
        NotifyOfPropertyChange();
      }
    }
  }
}
