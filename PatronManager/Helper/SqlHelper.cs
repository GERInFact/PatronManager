using DevExpress.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpressPlayAround.Helper
{
  public static class SqlHelper
  {
    public static string GetSqlTableFromClass<T>()
    {
      return typeof(T).Name.ToUpper();
    }

    public static string GetSqlAttributesFromClass<T>()
    {
      var builder = new StringBuilder();
      typeof(T).GetProperties().ForEach(p =>
      {
        if (p.Name != "ID")
          builder.Append($"@{p.Name}, ");
      });
      return builder.ToString();
    }
  }
}
