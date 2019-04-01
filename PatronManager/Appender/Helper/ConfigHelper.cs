using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmSqlConnectionAppender.Helper
{
  public static class ConfigHelper
  {
    public static void AddConnectionString(string name, string connectionString)
    {
      if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("'Name' and 'SQL Connection' need to be in a valid format. Check with https://www.connectionstrings.com/sql-server/ for further details.");

      var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      config.ConnectionStrings?.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString, "System.Data.SqlClient"));
      config.Save(ConfigurationSaveMode.Modified);
    }
  }
}
