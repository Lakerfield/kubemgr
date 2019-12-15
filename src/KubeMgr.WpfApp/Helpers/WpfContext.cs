﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace KubeMgr.WpfApp.Helpers
{
  public sealed class WpfContext
  {
    private readonly Dispatcher _dispatcher;

    public bool IsSynchronized
    {
      get
      {
        return this._dispatcher.Thread == Thread.CurrentThread;
      }
    }

    public WpfContext()
      : this(Dispatcher.CurrentDispatcher)
    {
    }

    public WpfContext(Dispatcher dispatcher)
    {
      Debug.Assert(dispatcher != null);

      this._dispatcher = dispatcher;
    }

    public void Invoke(Action action)
    {
      Debug.Assert(action != null);

      this._dispatcher.Invoke(action);
    }

    public void BeginInvoke(Action action)
    {
      Debug.Assert(action != null);

      this._dispatcher.BeginInvoke(action);
    }
  }
}
