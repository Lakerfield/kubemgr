using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Extensions.Logging;

namespace KubeMgr.WpfApp.Helpers
{
  public class StringLoggerProvider : ILoggerProvider
  {
    private readonly ObservableCollection<string> _log;

    public StringLoggerProvider(ObservableCollection<string> log)
    {
      _log = log;
    }

    public ILogger CreateLogger(string categoryName)
    {
      return new StringLogger(_log);
    }

    public void Dispose()
    {
      //throw new NotImplementedException();
    }
  }

  public class StringLogger : ILogger
  {
    private readonly ObservableCollection<string> _log;

    public StringLogger(ObservableCollection<string> log)
    {
      _log = log;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      _log.Add($"{logLevel} {eventId} {formatter(state, exception)}");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return true;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      throw new NotImplementedException();
    }
  }
}
