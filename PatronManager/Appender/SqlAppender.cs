using System;
using System.Windows.Forms;
using FrmSqlConnectionAppender.Data.Interfaces;
using FrmSqlConnectionAppender.Helper;

namespace FrmSqlConnectionAppender
{
  public partial class SqlAppender : Form
  {
    #region Constructors

    public SqlAppender()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Private, internal and protected methods

    private void BtnAddSqlConnectionClick(object sender, EventArgs e)
    {
      try
      {
        ISqlData sqlData = Factory.CreateSqlData(this.txtSqlConnectionName.Text, this.txtSqlConnectionString.Text);
        this.ClearInputFields();

        ConfigHelper.AddConnectionString(sqlData.Name, sqlData.ConnectionString);
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }


    private void BtnExitClick(object sender, EventArgs e)
    {
      this.Close();
    }


    private void ClearInputFields()
    {
      this.txtSqlConnectionName.Text = "";
      this.txtSqlConnectionString.Text = "";
    }

    #endregion
  }
}