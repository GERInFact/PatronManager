using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpressPlayAround.Export.Interfaces;
using DevExpressPlayAround.Log.Interfaces;

namespace DevExpressPlayAround.Export
{
  internal class ExportManager : IExportManager
  {
    #region Declarations

    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public ExportManager(ILogger logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region IExportManager Members

    public void ExportData(Action<Stream> exportMethod, ExportFormats format, string path = null)
    {
      try
      {
        using (FileStream stream = new FileStream(path ?? Path.Combine(Application.StartupPath, $"data.{format.ToString().ToLower()}"), FileMode.OpenOrCreate))
        {
          exportMethod?.Invoke(stream);
          stream.Close();
        }
      }
      catch (Exception e)
      {
        _logger.Log(e.Message);
      }
    }


    public async Task ExportDataAsync(Action<Stream> exportMethod, ExportFormats format, string path = null)
    {
      try
      {
        if(path == null)
          path = XtraInputBox.Show($"File location: {Application.StartupPath}", "File Name", "file name") ?? "data";

        using (FileStream stream = new FileStream(Path.Combine(Application.StartupPath, $"{path}.{format.ToString().ToLower()}"), FileMode.OpenOrCreate))
        {
          await Task.Run(() => exportMethod?.Invoke(stream));
          stream.Close();
        }
      }
      catch (Exception e)
      {
        _logger.Log(e.Message);
      }
    }


    public void ImportData(string path = null)
    {
      throw new NotImplementedException(nameof(ImportData));
    }


    public Task ImportDataAsync(string path = null)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}