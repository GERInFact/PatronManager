using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DevExpressPlayAround.Grid
{
  public interface IToolTipManager
  {
    void SetToolTips();
    void DisplayToolTip(Point p);
  }

  public class ToolTipManager : IToolTipManager
  {
    private readonly ToolTipController _controller = new ToolTipController() { ToolTipAnchor = ToolTipAnchor.Cursor };
    private readonly Dictionary<int, string> _toolTipContainer = new Dictionary<int, string>();
    private readonly GridControl _control;


    public ToolTipManager(GridControl control)
    {
      _control = control ?? throw new ArgumentNullException($"Failure: {nameof(control)} can not be NULL.");
    }
    public void SetToolTips()
    {
      /*var index = 0;
      _toolTipContainer.ClearPropertyView();
      _control.DataSource. .ForEach(txt =>
      {
        _toolTipContainer.Add(index, $@"{index}: {txt}");
        index++;
      });*/
    }

    public void DisplayToolTip(Point p)
    {
      var info = GetGridHitInfo(p);

      if (info != null && _toolTipContainer.ContainsKey(info.RowHandle))
      {
        _controller.SetToolTip(_control, _toolTipContainer[info.RowHandle]);
      }

    }
    private GridHitInfo GetGridHitInfo(Point p)
    {
      var view = _control.GetViewAt(p);
      return view?.CalcHitInfo(p) as GridHitInfo;
    }
  }
}