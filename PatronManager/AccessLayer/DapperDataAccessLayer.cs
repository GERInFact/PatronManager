using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevExpress.Utils.Extensions;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Helper;

namespace DevExpressPlayAround.AccessLayer
{
  internal class DapperDataAccessLayer<T> : IDataAccessLayer<T> where T : class, new()
  {
    #region Public properties and indexer

    public string ConnectionString { get; set; }

    #endregion

    #region IDataAccessLayer<T> Members

    public void ConnectToDataBase(string connectionString)
    {
      throw new NotImplementedException();
    }


    public void Create()
    {
      using (IDbConnection connection = new SqlConnection(this.ConnectionString))
      {
        connection.Execute($"INSERT INTO {SqlHelper.GetSqlTableFromClass<T>()} VALUES ({SqlHelper.GetSqlAttributesFromClass<T>().TrimEnd(',', ' ')})", new T());
      }
    }


    public async Task CreateAsync()
    {
      await Task.Run(() => this.Create());
    }


    public void Delete(T obj)
    {
      throw new NotImplementedException();
    }


    public IEnumerable<T> Read()
    {
      using (IDbConnection connection = new SqlConnection(this.ConnectionString))
      {
        return connection.Query<T>($"select * from {SqlHelper.GetSqlTableFromClass<T>()}");
      }
    }


    public async Task<IEnumerable<T>> ReadAsync()
    {
      return await Task.Run(() => this.Read());
    }


    public void Update()
    {
      throw new NotImplementedException();
    }


    public Task UpdateAsync()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Public methods

    public Task ConnectToDataBaseAsync(string connectionString)
    {
      throw new NotImplementedException();
    }

    #endregion

 }
}