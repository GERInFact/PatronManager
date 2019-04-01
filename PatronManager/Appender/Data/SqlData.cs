using FrmSqlConnectionAppender.Data.Interfaces;

namespace FrmSqlConnectionAppender.Data
{
  public class SqlData : ISqlData
  {
    #region Public properties and indexer

    public string ConnectionString { get; set; }
    public string Name { get; set; }

    #endregion
  }
}