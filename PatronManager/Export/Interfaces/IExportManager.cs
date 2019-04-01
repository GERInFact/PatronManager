using System;
using System.IO;
using System.Threading.Tasks;

namespace DevExpressPlayAround.Export.Interfaces
{
  public interface IExportManager
  {
    #region Public methods

    void ExportData(Action<Stream> exportMethod, ExportFormats format, string path = null);

    Task ExportDataAsync(Action<Stream> exportMethod, ExportFormats format, string path = null);

    void ImportData(string path = null);
    Task ImportDataAsync(string path = null);

    #endregion
  }
}