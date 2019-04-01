using System.Linq;
using DevExpress.XtraGrid;

namespace DevExpressPlayAround.Export.Interfaces
{
  public interface IPrinter
  {
    #region Public methods

    void Print(GridControl control);

    #endregion
  }
}