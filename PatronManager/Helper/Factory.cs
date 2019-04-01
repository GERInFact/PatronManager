using System;
using CustomRibbon;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraPrinting.Preview;
using DevExpressPlayAround.AccessLayer;
using DevExpressPlayAround.AccessLayer.Interfaces;
using DevExpressPlayAround.Data;
using DevExpressPlayAround.Export;
using DevExpressPlayAround.Export.Interfaces;
using DevExpressPlayAround.Log;
using DevExpressPlayAround.Log.Interfaces;
using Unity;

namespace DevExpressPlayAround.Helper
{
  public static class Factory
  {
    #region Public methods

    public static IDataAccessLayer<T> CreateAccessLayer<T>() where T : XPObject, new()
    {
      return new XpoDataAccessLayer<T>(CreateDataBaseInformation(AutoCreateOption.DatabaseAndSchema));
    }


    public static IDatabaseInformation CreateDataBaseInformation(AutoCreateOption option)
    {
      return new XpoDatabaseInformation(option);
    }


    public static IExportManager CreateExportManager()
    {
      return new ExportManager(CreateLogger());
    }


    public static GridHelper CreateGridHelper(FrmPatronManager patronManager)
    {
      return new GridHelper(patronManager);
    }


    public static ILogger CreateLogger()
    {
      return new Logger();
    }


    public static FrmPatronManager CreatePatronManager()
    {
      return new FrmPatronManager(CreateAccessLayer<XpClient>(), CreateExportManager(), CreateLogger(), CreatePrinter());
    }


    public static SystemPrintDialogRunner CreatePrintDialog()
    {
      return new SystemPrintDialogRunner();
    }


    public static IPrinter CreatePrinter()
    {
      return new XpPrinter();
    }

    public static IUnityContainer CreateUnityContainer()
    {
      return new UnityContainer();
    }


    public static UnitOfWork CreateWorkUnit()
    {
      return new UnitOfWork();
    }


    public static UnitOfWork CreateWorkUnit(IDataLayer dataLayer)
    {
      return new UnitOfWork(dataLayer);
    }


    public static XpClient CreateXpClient(Session session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));

      return new XpClient(session);
    }


    public static ExternalCommand CreateXpCommand(Session session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));

      return new ExternalCommand(session);
    }


    public static XpTicket CreateXpTicket(Session session)
    {
      if(session == null)
        throw new ArgumentNullException(nameof(session));

      return new XpTicket(session);
    }

    #endregion
  }
}