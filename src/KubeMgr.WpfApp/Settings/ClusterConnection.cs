using System;
using Caliburn.Micro;

namespace KubeMgr.WpfApp.Settings
{
  public class ClusterConnection : PropertyChangedBase
  {

    private string _group;
    public string Group
    {
      get { return _group; }
      set
      {
        _group = value;
        NotifyOfPropertyChange();
      }
    }

    private string _description;
    public string Description
    {
      get { return _description; }
      set
      {
        _description = value;
        NotifyOfPropertyChange();
      }
    }

    private string _comments;
    public string Comments
    {
      get { return _comments; }
      set
      {
        _comments = value;
        NotifyOfPropertyChange();
      }
    }

    private bool _autoStart = true;
    public bool AutoStart
    {
      get { return _autoStart; }
      set
      {
        _autoStart = value;
        NotifyOfPropertyChange();
      }
    }

    private ConfigKind _kind;
    public ConfigKind Kind
    {
      get { return _kind; }
      set
      {
        _kind = value;
        NotifyOfPropertyChange();
        NotifyOfPropertyChange(nameof(IsNoAuthentication));
        NotifyOfPropertyChange(nameof(IsKubeConfig));
        NotifyOfPropertyChange(nameof(IsBearerToken));
      }
    }

    public bool IsNoAuthentication => Kind == ConfigKind.NoAuthentication;
    public bool IsKubeConfig => Kind == ConfigKind.KubeConfigFile;
    public bool IsBearerToken => Kind == ConfigKind.BearerToken;

    #region KubeConfig

    private string _kubeConfigFile;
    public string KubeConfigFile
    {
      get { return _kubeConfigFile; }
      set
      {
        _kubeConfigFile = value;
        NotifyOfPropertyChange();
      }
    }

    private string _defaultContext = "default";
    public string DefaultContext
    {
      get { return _defaultContext; }
      set
      {
        _defaultContext = value;
        NotifyOfPropertyChange();
      }
    }

    #endregion

    #region NoAuthentication / BearerToken

    private bool _allowInsecure = false;
    public bool AllowInsecure
    {
      get { return _autoStart; }
      set
      {
        _allowInsecure = value;
        NotifyOfPropertyChange();
      }
    }

    private string _url;
    public string Url
    {
      get { return _url; }
      set
      {
        _url = value;
        NotifyOfPropertyChange();
      }
    }
    
    #endregion

    #region BearerToken

    private string _accessToken;
    public string AccessToken
    {
      get { return _accessToken; }
      set
      {
        _accessToken = value;
        NotifyOfPropertyChange();
      }
    }

    #endregion

    private string _defaultNamespace = "default";
    public string DefaultNamespace
    {
      get { return _defaultNamespace; }
      set
      {
        _defaultNamespace = value;
        NotifyOfPropertyChange();
      }
    }



    public ClusterConnection Clone()
    {
      return (ClusterConnection)MemberwiseClone();
    }

    public bool IsValid()
    {
      var result = true;
      result &= !string.IsNullOrWhiteSpace(Description);
      switch (Kind)
      {
        case ConfigKind.NoAuthentication:
          result &= !string.IsNullOrWhiteSpace(Url);
          break;
        case ConfigKind.KubeConfigFile:
          result &= !string.IsNullOrWhiteSpace(KubeConfigFile);
          result &= !string.IsNullOrWhiteSpace(DefaultContext);
          break;
        case ConfigKind.BearerToken:
          result &= !string.IsNullOrWhiteSpace(Url);
          result &= !string.IsNullOrWhiteSpace(AccessToken);
          break;
        default:
          return false;
      }
      return result;
    }
  }
}
