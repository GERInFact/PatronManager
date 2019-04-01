using System;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace DevExpressPlayAround.Export
{
  public enum ExportFormats
  {
    PDF,
    XLS,
    XLSX,
    RTF,
    CSV,
    DOCX
  }

  internal static class ExportHelper
  {
    #region Public methods

    public static Action<Stream> GetExportHandler(GridView view, ExportFormats format)
    {
      switch (format)
      {
        case ExportFormats.PDF:
          return view.ExportToPdf;
        case ExportFormats.XLS:
          return view.ExportToXls;
        case ExportFormats.XLSX:
          return view.ExportToXlsx;
        case ExportFormats.RTF:
          return view.ExportToRtf;
        case ExportFormats.CSV:
          return view.ExportToCsv;
        default:
          return view.ExportToDocx;
      }
    }

    #endregion
  }
}