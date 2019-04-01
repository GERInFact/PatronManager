using System;

namespace DevExpressPlayAround.Data
{
  internal class Client
  {
    public int ID { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime Birthday { get; set; } = DateTime.Now;
    public string Address { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string EmailAddress { get; set; } = String.Empty;
    public string ClientId { get; set; } = String.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
  }
}
