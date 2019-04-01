using System;
using System.Windows.Forms;
using CustomRibbon;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.DI;
using DevExpressPlayAround.Helper;

namespace DevExpressPlayAround
{
  internal static class Program
  {
    #region Private, internal and protected methods

    /// <summary>
    ///   The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      try
      {
        var dependencyManager = new DependencyManager<XpClient>(Factory.CreateUnityContainer());
        
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(dependencyManager.GetPatronManagerResolved());
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }
    }
    #endregion
  }
}