using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.Helper;

namespace DevExpressPlayAround.AccessLayer
{
  internal class XpoDataAccessLayer<T> : IDataAccessLayer<T> where T : XPObject, new()
  {
    #region Declarations

    private readonly IDatabaseInformation _dbInfo;

    #endregion

    #region Constructors

    public XpoDataAccessLayer(IDatabaseInformation dbInfo)
    {
      this._dbInfo = dbInfo ?? throw new ArgumentNullException(nameof(dbInfo));
    }

    #endregion

    #region IDataAccessLayer<T> Members

    public void ConnectToDataBase(string connectionString)
    {
      this._dbInfo.ConnectToDB(connectionString);
    }


    public async Task ConnectToDataBaseAsync(string connectionString)
    {
      await Task.Run(()=>this.ConnectToDataBase(connectionString));
    }


    public void Create()
    {
      UnitOfWork uOw = this._dbInfo.Work;

      TypeRegistrar.RegisterXpObjectToUoW<T>(uOw);
      uOw?.CommitChanges();
    }


    public async Task CreateAsync()
    {
      await Task.Run(() => this.Create());
    }


    public void Delete(T obj)
    {
      if(obj == null)
        throw new ArgumentNullException();

      this._dbInfo.Work?.Delete(obj);
      this._dbInfo.Work?.CommitChanges();
    }


    public IEnumerable<T> Read()
    {
      return this._dbInfo.Work?.Query<T>();
    }


    public async Task<IEnumerable<T>> ReadAsync()
    {
      return await Task.Run(() => this.Read());
    }


    public void Update()
    {
      this._dbInfo.Work?.CommitChanges();
    }


    public async Task UpdateAsync()
    {
      await Task.Run(() => this.Update());
    }

    #endregion

  
    ~XpoDataAccessLayer()
    {
      this._dbInfo.Work?.PurgeDeletedObjects();
      this._dbInfo.Work?.ExecuteNonQuery($"DBCC CHECKIDENT('{typeof(T).Name}', RESEED, {this._dbInfo.Work.Query<T>().Count()})");
      this._dbInfo.Work?.Dispose();
    }
  }
}