using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomRibbon;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.Export;
using DevExpressPlayAround.Export.Interfaces;
using DevExpressPlayAround.Helper;
using DevExpressPlayAround.Log.Interfaces;
using FrmSqlConnectionAppender;
using FrmSqlConnectionAppender.Helper;
using ExportHelper = DevExpressPlayAround.Export.ExportHelper;
using Factory = DevExpressPlayAround.Helper.Factory;

namespace DevExpressPlayAround
{
  public partial class FrmPatronManager : Form
  {
    #region Declarations

    private readonly IDataAccessLayer<XpClient> _accessLayer;

    private Action<Stream> _exportHandler;
    private readonly IExportManager _exportManager;
    private readonly GridHelper _gridHelper;
    private readonly ILogger _logger;
    private readonly IPrinter _printer;

    #endregion

    #region Public properties and indexer

    public GridControl GridControl1
    {
      get => this.gridControl1;
      set => this.gridControl1 = value;
    }

    public GridView GridView1
    {
      get => this.gridView1;
      set => this.gridView1 = value;
    }

    public ListBoxControl LstGridProperties
    {
      get => this.lstGridProperties;
      set => this.lstGridProperties = value;
    }

    #endregion

    #region Constructors

    public FrmPatronManager(IDataAccessLayer<XpClient> accessLayer, IExportManager exportManager, ILogger logger, IPrinter printer)
    {
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
      this._exportManager = exportManager ?? throw new ArgumentException(nameof(exportManager));
      this._accessLayer = accessLayer ?? throw new ArgumentNullException(nameof(accessLayer));
      this._printer = printer ?? throw new ArgumentNullException(nameof(printer));
      this._gridHelper = Factory.CreateGridHelper(this);

      this.InitializeComponent();
      SqlConnectionHelper.LoadSqlConnectionsFromConfig(this.lstSqlConnections.Items);
      this.lstGridProperties.Items.Clear();
    }

    #endregion

    #region Private, internal and protected methods

    private async Task AddDataAsync()
    {
      await this._accessLayer.CreateAsync();
      this._gridHelper.BindGridData(await this._accessLayer.ReadAsync());
      this._gridHelper.AddToDataSource<ExternalCommand>();
      this._gridHelper.UpdateGrid();
      this.SetGridViewOptions();
    }


    private async void BtnAddDataClickAsync(object sender, EventArgs e)
    {
      try
      {
        await this.AddDataAsync();
      }
      catch (Exception exception)
      {
        this._logger.Log(exception.Message);
      }
    }


    private void BtnAddSqlConnectionClick(object sender, EventArgs e)
    {
      var sqlAppender = new SqlAppender(() => SqlConnectionHelper.LoadSqlConnectionsFromConfig(this.lstSqlConnections.Items));
      sqlAppender.Show(this);
    }


    private async void BtnConnectClick(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        await this._accessLayer.ConnectToDataBaseAsync(SqlConnectionHelper.GetSqlConnectionString(this.lstSqlConnections.SelectedItem.ToString()));
        await this.ReadDataFromDbAsync();
        this.Cursor = Cursors.Arrow;
      }
      catch (Exception)
      {
        this._logger.Log("Connection failed. Data source invalid.");
      }
    }


    private void BtnDeleteClick(object sender, EventArgs e)
    {
      try
      {
        this.EraseSelectedDataFromDb();
      }
      catch (Exception exception)
      {
        this._logger.Log(exception.Message);
      }
    }


    private async void BtnExportClick(object sender, EventArgs e)
    {
      await this.ExportDataAsync();
    }


    private void BtnImportClick(object sender, EventArgs e)
    {
      this._exportManager.ImportData();
    }


    private void BtnPrintClick(object sender, EventArgs e)
    {
      this._printer.Print(this.GridControl1);
    }


    private async void BtnReadFromDBClick(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        await this.ReadDataFromDbAsync();
        this.Cursor = Cursors.Arrow;
      }
      catch (Exception exception)
      {
        this._logger.Log(exception.Message);

        this.Cursor = Cursors.Arrow;
      }
    }


    private void BtnRefreshGridClick(object sender, EventArgs e)
    {
      this._gridHelper.DisplaySelectedColumn();
      this._gridHelper.UpdateGrid();
      this.SetGridViewOptions();
    }


    private async void BtnSaveClick(object sender, EventArgs e)
    {
      try
      {
        this.SetGridViewOptions();
        await this._accessLayer.UpdateAsync();
      }
      catch (Exception exception)
      {
        this._logger.Log(exception.Message);
      }
    }


    private void EraseSelectedDataFromDb()
    {
      if(this.gridView1.GetRow(this.gridView1.FocusedRowHandle) is XpClient selectedObject)
        this._accessLayer.Delete(selectedObject);

      this._gridHelper.BindGridData(this._accessLayer.Read());
      this._gridHelper.UpdateGrid();
    }


    private async Task ExportDataAsync()
    {
      var selection = this.lstExportFormats.SelectedItems.ToArray();

      for (int i = 0; i < selection.Length; i++)
      {
        Enum.TryParse(selection[i].ToString(), out ExportFormats format);
        this._exportHandler = ExportHelper.GetExportHandler(this.GridView1, format);

        await this._exportManager.ExportDataAsync(this._exportHandler, format);
      }
    }


    private async Task ReadDataFromDbAsync()
    {
      var result = await this._accessLayer.ReadAsync();

      this._gridHelper.BindGridData(result);
      this._gridHelper.UpdateGrid();
      this.SetGridViewOptions();
    }


    private void SelectSearchedItem(ListBoxItemCollection items, string searchText)
    {
      this.lstGridProperties.UnSelectAll();
      this.lstGridProperties.SetSelected(items.IndexOf(searchText), true);
      this.txtGridProperty.Text = string.Empty;
    }


    private void SetGridViewOptions()
    {
      this.GridView1.OptionsView.ColumnAutoWidth = false;
      this.GridView1.BestFitColumns();
    }


    private void SimpleButton2Click(object sender, EventArgs e)
    {
      ListBoxControl items = this.lstSqlConnections;
      ConfigHelper.RemoveConnectionString(items.SelectedItem.ToString());
      SqlConnectionHelper.RemoveSqlConnectionFromConfig(items.SelectedItem.ToString(), this.lstSqlConnections.Items);
    }


    private void TxtGridPropertyKeyDown(object sender, KeyEventArgs e)
    {
      string searchText = this.txtGridProperty.Text;
      ListBoxItemCollection items = this.lstGridProperties.Items;

      if(e.KeyCode != Keys.Enter)
        return;

      if(!items.Contains(searchText))
      {
        this._logger.Log($"{searchText} is NOT a specified column. Check spelling.");
        return;
      }

      this.SelectSearchedItem(items, searchText);
    }

    #endregion
  }
}