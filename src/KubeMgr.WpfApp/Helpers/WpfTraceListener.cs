using System;
using System.Diagnostics;
using System.Text;

namespace KubeMgr.WpfApp.Helpers
{
  public sealed class WpfTraceListener : TraceListener
  {
    private readonly StringBuilder buffer = new StringBuilder();

    public override void Write(string message)
    {
      buffer.Append(message);
    }

    [DebuggerStepThrough]
    public override void WriteLine(string message)
    {
      buffer.Append(message);

      if (TraceCatched != null)
      {
        TraceCatched(buffer.ToString());
      }

      buffer.Clear();
    }

    public event Action<string> TraceCatched;
  }
}
