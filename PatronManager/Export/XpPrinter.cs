using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting.Preview;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.Export.Interfaces;
using DevExpressPlayAround.Helper;

namespace DevExpressPlayAround.Export
{
  internal class XpPrinter : IPrinter
  {
    #region IPrinter Members

    public void Print(GridControl control)
    {
      PrintDialogRunner.Instance = Factory.CreatePrintDialog();

      control.ShowRibbonPrintPreview();
    }

    #endregion
  }
}