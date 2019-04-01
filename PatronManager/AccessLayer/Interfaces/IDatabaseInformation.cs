using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpressPlayAround.AccessLayer.Interfaces
{
  public interface IDatabaseInformation
  {
    #region Public properties and indexer

    string ConnectionString { get; }
    AutoCreateOption Option { get; set; }
    UnitOfWork Work { get; set; }

    #endregion

    #region Public methods

    void ConnectToDB(string connectionString);

    #endregion
  }
}