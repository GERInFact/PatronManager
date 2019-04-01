using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpressPlayAround.Data;

namespace DevExpressPlayAround.Helper
{
  public static class TypeRegistrar
  {
    public static void RegisterXpObjectToUoW<T>(UnitOfWork uOw)
    {
      if (uOw == null)
        throw new ArgumentNullException(nameof(uOw));

      if (typeof(T) == typeof(XpClient))
        Factory.CreateXpClient(uOw);
      else if (typeof(T) == typeof(XpTicket))
        Factory.CreateXpTicket(uOw);
      else if (typeof(T) == typeof(CustomRibbon.ExternalCommand))
        Factory.CreateXpCommand(uOw);
    }
  }
}
