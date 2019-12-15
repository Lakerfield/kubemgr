using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using KubeClient.Models;
using KubeMgr.Annotations;

namespace KubeMgr.Models
{
  public class View<T> : IObserver<IResourceEventV1<T>>, INotifyPropertyChanged, IDisposable where T : KubeResourceV1
  {
    private CompositeDisposable _disposable = new CompositeDisposable();
    public ObservableCollection<ViewItem<T>> Collection { get; }

    private string _status;
    public string Status
    {
      get => _status;
      private set
      {
        if (value == _status) return;
        _status = value;
        OnPropertyChanged();
      }
    }

    public View()
    {
      Collection = new ObservableCollection<ViewItem<T>>();
      Status = "waiting for data...";
    }

    public View(IEnumerable<T> items) : this()
    {
      foreach (var item in items)
        Collection.Add(new ViewItem<T>(item));
      Status = "loaded";
    }

    public void AddDisposable(IDisposable disposable)
    {
      _disposable.Add(disposable);
    }

    public void OnNext(IResourceEventV1<T> value)
    {
      Status = "connected";

      switch (value.EventType)
      {
        case ResourceEventType.Added:
          var newItem = new ViewItem<T>(value.Resource);
          Collection.Add(newItem);
          break;

        case ResourceEventType.Modified:
          var uidUpdate = value.Resource.Metadata.Uid;

          var updateItem = Collection.FirstOrDefault(e => e.Uid == uidUpdate);
          if (updateItem != null)
            updateItem.Update(value.Resource);
          else
            Collection.Add(new ViewItem<T>(value.Resource));
          break;

        case ResourceEventType.Deleted:
          var uidDelete = value.Resource.Metadata.Uid;

          var deleteItem = Collection.FirstOrDefault(e => e.Uid == uidDelete);
          if (deleteItem != null)
          {
            deleteItem.Update(value.Resource);
            Collection.Remove(deleteItem);
          }
          break;

        case ResourceEventType.Bookmark:
          //todo
          break;

        case ResourceEventType.Error:
          //todo
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void OnError(Exception error)
    {
      Status = $"error: {error.Message}";
      //Collection.Clear();
    }

    public void OnCompleted()
    {
      Status = "completed";
      //Collection.Clear();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
      _disposable.Dispose();
    }
  }


  public class ViewItem<T>:INotifyPropertyChanged where T : KubeResourceV1
  {
    public string Uid => Entity.Metadata.Uid;
    public string Name => Entity.Metadata.Name;

    private T _entity;
    public T Entity
    {
      get => _entity;
      private set
      {
        if (Equals(value, _entity)) return;
        _entity = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Name));
      }
    }

    public ViewItem(T entity)
    {
      Entity = entity;
    }

    internal void Update(T entity)
    {
      Entity = entity;
    }


    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
