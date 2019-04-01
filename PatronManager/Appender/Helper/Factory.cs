using System;
using System.Linq;
using FrmSqlConnectionAppender.Data;
using FrmSqlConnectionAppender.Data.Interfaces;

namespace FrmSqlConnectionAppender.Helper
{
  public static class Factory
  {
    #region Public methods

    public static ISqlData CreateSqlData()
    {
      return new SqlData();
    }


    public static ISqlData CreateSqlData(string name, string sqlConnection)
    {
      if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sqlConnection))
        throw new InvalidOperationException("'Name' and 'SQL Connection' need to be in a valid format. Check with https://www.connectionstrings.com/sql-server/ for further details.");

      return new SqlData {Name = name, ConnectionString = sqlConnection};
      ;
    }

    #endregion
  }
}