using System.Linq;

namespace FrmSqlConnectionAppender.Data.Interfaces
{
  public interface ISqlData
  {
    #region Public properties and indexer

    string ConnectionString { get; set; }
    string Name { get; set; }

    #endregion
  }
}