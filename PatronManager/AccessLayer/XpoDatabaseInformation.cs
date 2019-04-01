using System;
using System.Data.SqlClient;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.Helper;

namespace DevExpressPlayAround.AccessLayer
{
  internal class XpoDatabaseInformation : IDatabaseInformation
  {
    #region Declarations

    private string _connectionString;
    private string _oldConnectionString;

    #endregion

    #region Public properties and indexer

    public string ConnectionString
    {
      get => this._connectionString;

      private set
      {
        if(string.IsNullOrWhiteSpace(value))
          throw new ArgumentNullException();

        this._connectionString = value;
      }
    }

    public AutoCreateOption Option { get; set; }
    public UnitOfWork Work { get; set; }

    #endregion

    #region Constructors

    public XpoDatabaseInformation(AutoCreateOption option)
    {
      this._oldConnectionString = string.Empty;
      this.Option = option;
      this.Work = Factory.CreateWorkUnit();
    }

    #endregion

    #region IDatabaseInformation Members

    public void ConnectToDB(string connectionString)
    {
      this.ConnectionString = connectionString;

      if(this.IsValidConnectionExistent())
        return;
      
      this.Work.Connect(XpoDefault.GetDataLayer(new SqlConnection(connectionString), this.Option));
    }


    #endregion

    #region Private, internal and protected methods

    private bool IsConnectionChanged()
    {
      if(this._oldConnectionString != this.ConnectionString)
      {
        this.UpdateConnectionString();
        return true;
      }

      return false;
    }


    private bool IsValidConnectionExistent()
    {
      return this.Work.IsConnected && this.IsConnectionChanged();
    }


    private void UpdateConnectionString()
    {
      this._oldConnectionString = this.ConnectionString;
    }

    #endregion
  }
}