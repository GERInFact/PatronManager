using System.Linq;
using System.Windows.Forms;
using DevExpressPlayAround.Log.Interfaces;

namespace DevExpressPlayAround.Log
{
  public class Logger : ILogger
  {
    #region ILogger Members

    public void Log(string message)
    {
      MessageBox.Show($"Message Log: {message}");
    }

    #endregion
  }
}