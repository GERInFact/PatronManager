using System;
using DevExpress.Xpo;

namespace DevExpressPlayAround.Data
{
  public class XpClient : XPObject
  {
    #region Declarations

    private string _address;

    private DateTime _birthDay;

    private string _clientId;

    private DateTime _dateCreated;

    private string _emailAddress;

    private string _firstName;


    private int _id;

    private string _lastName;

    private string _phoneNumber;

    #endregion

    #region Public properties and indexer

    public string Address
    {
      get => _address;
      set => SetPropertyValue($"{nameof(Address)}", ref _address, value);
    }

    public DateTime Birthday
    {
      get => _birthDay;
      set => SetPropertyValue($"{nameof(Birthday)}", ref _birthDay, value);
    }

    public string ClientId
    {
      get => _clientId;
      set => SetPropertyValue($"{nameof(ClientId)}", ref _clientId, value);
    }


    public DateTime DateCreated
    {
      get => _dateCreated;
      set => SetPropertyValue($"{nameof(DateCreated)}", ref _dateCreated, value);
    }

    public string EmailAddress
    {
      get => _emailAddress;
      set => SetPropertyValue($"{nameof(EmailAddress)}", ref _emailAddress, value);
    }

    public string FirstName
    {
      get => _firstName;
      set => SetPropertyValue($"{nameof(FirstName)}", ref _firstName, value);
    }

    public int ID
    {
      get => _id;
      set => SetPropertyValue($"{nameof(ID)}", ref _id, value);
    }

    public string LastName
    {
      get => _lastName;
      set => SetPropertyValue($"{nameof(LastName)}", ref _lastName, value);
    }

    public string PhoneNumber
    {
      get => _phoneNumber;
      set => SetPropertyValue($"{nameof(PhoneNumber)}", ref _phoneNumber, value);
    }

    #endregion

    #region Constructors

    public XpClient(Session session) : base(session)
    {
    }


    public XpClient()
    {
    }

    #endregion
  }
}