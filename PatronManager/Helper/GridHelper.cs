using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace DevExpressPlayAround.Helper
{
  public class GridHelper
  {
    #region Declarations

    private readonly FrmPatronManager _frmGridBuilder;

    #endregion

    #region Constructors

    public GridHelper(FrmPatronManager frmGridBuilder)
    {
      this._frmGridBuilder = frmGridBuilder ?? throw new ArgumentNullException(nameof(frmGridBuilder));
    }

    #endregion

    #region Public methods

    public void AddToDataSource<T>() where T : class, new()
    {
      var dataSet = this._frmGridBuilder.GridControl1.DataSource as List<T>;
      dataSet?.Add(new T());
      this.UpdateGrid();
    }


    public void BindGridData<T>(IEnumerable<T> data) where T : class, new()
    {
      this.GenerateColumnPropertiesFromType<T>();

      this._frmGridBuilder.GridControl1.DataSource = data ?? throw new ArgumentNullException(nameof(data));
    }


    public void ClearPropertyView()
    {
      this._frmGridBuilder.LstGridProperties.Items.Clear();
      this._frmGridBuilder.GridView1.Columns.Clear();
      this._frmGridBuilder.GridControl1.DataSource = new object();
    }


    public void DisplaySelectedColumn()
    {
      GridColumnCollection columns = this._frmGridBuilder.GridView1.Columns;

      columns.Clear();

      this._frmGridBuilder.LstGridProperties.SelectedItems.ForEach(gP =>
      {
        string columnName = gP as string ?? string.Empty;
        columns.AddVisible(columnName, columnName);
      });

      this.SetColumnEdits();
    }


    public void RemoveSelectedProperty()
    {
      ListBoxControl columnProperties = this._frmGridBuilder.LstGridProperties;
      columnProperties.Items.Remove(columnProperties.SelectedItem);
    }


    public void UpdateGrid()
    {
      GridControl gridControl = this._frmGridBuilder.GridControl1;
      gridControl.RefreshDataSource();
      gridControl.Update();
      gridControl.Refresh();
    }

    #endregion

    #region Private, internal and protected methods

    private void GenerateColumnPropertiesFromType<T>() where T : class
    {
      const BindingFlags PROPERTYFILTER = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static;

      ListBoxItemCollection items = this._frmGridBuilder.LstGridProperties.Items;

      typeof(T).GetProperties(PROPERTYFILTER).ForEach(p =>
      {
        if(!items.Contains(p.Name))
          items.Add(p.Name);
      });
    }


    private void SetColumnEdits(IEnumerable<RepositoryItem> editItems = null)
    {
      GridColumnCollection columns = this._frmGridBuilder.GridView1.Columns;

      if(columns == null || editItems == null)
        return;

      var repositoryItems = editItems as RepositoryItem[] ?? editItems.ToArray();

      int maxVal = repositoryItems.Count() <= columns.Count ? repositoryItems.Count() : columns.Count;

      for (int i = 0; i < maxVal; i++)
        columns[i].ColumnEdit = repositoryItems[i];
    }

    #endregion
  }
}