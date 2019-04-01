using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpressPlayAround.Helper
{
  public static class SqlConnectionHelper
  {
    public static string GetSqlConnectionString(string name)
    {
      if(string.IsNullOrWhiteSpace(name))
        throw new ArgumentNullException();

      return ConfigurationManager.ConnectionStrings[name]?.ConnectionString ?? string.Empty;
    }
  }
}
