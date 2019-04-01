using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpressPlayAround.AccessLayer;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Export;
using DevExpressPlayAround.Export.Interfaces;
using DevExpressPlayAround.Log;
using DevExpressPlayAround.Log.Interfaces;
using FrmSqlConnectionAppender.Data;
using FrmSqlConnectionAppender.Data.Interfaces;
using Unity;
using Unity.Injection;

namespace DevExpressPlayAround.DI
{
  internal class DependencyManager<T> where T : XPObject, new()
  {
    #region Declarations

    private readonly IUnityContainer _container;

    #endregion

    #region Constructors

    public DependencyManager(IUnityContainer container)
    {
      this._container = container ?? throw new ArgumentNullException(nameof(container));
    }

    #endregion

    #region Public methods

    public FrmPatronManager GetPatronManagerResolved()
    {
      this.RegisterAll();
      return this._container.Resolve<FrmPatronManager>();
    }

    #endregion

    #region Private, internal and protected methods

    private void RegisterAll()
    {
      this._container.RegisterType<IDataAccessLayer<T>, XpoDataAccessLayer<T>>();
      this._container.RegisterType<IDatabaseInformation, XpoDatabaseInformation>(new InjectionConstructor(AutoCreateOption.DatabaseAndSchema));
      this._container.RegisterType<IExportManager, ExportManager>();
      this._container.RegisterType<ILogger, Logger>();
      this._container.RegisterType<IPrinter, XpPrinter>();
      this._container.RegisterType<ISqlData, SqlData>();
    }

    #endregion
  }
}