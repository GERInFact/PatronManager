using System;
using System.Linq;
using DevExpress.Xpo;

namespace DevExpressPlayAround.Data
{
  public class XpTicket : XPObject
  {
    #region Declarations

    private string _consultant;
    private DateTime _dateCreated;
    private DateTime _dateFixed;
    private string _description;
    private bool _isBug;
    private string _name;
    private string _status;
    private string _ticketId;

    #endregion

    #region Public properties and indexer

    public string Consultant
    {
      get => _consultant;
      set => SetPropertyValue($"{nameof(Consultant)}", ref _consultant, value);
    }

    public DateTime DateCreated
    {
      get => _dateCreated;
      set => SetPropertyValue($"{nameof(DateCreated)}", ref _dateCreated, value);
    }

    public DateTime DateFixed
    {
      get => _dateFixed;
      set => SetPropertyValue($"{nameof(DateFixed)}", ref _dateFixed, value);
    }

    public string Description
    {
      get => _description;
      set => SetPropertyValue($"{nameof(Description)}", ref _description, value);
    }

    public bool IsBug
    {
      get => _isBug;
      set => SetPropertyValue($"{nameof(IsBug)}", ref _isBug, value);
    }

    public string Name
    {
      get => _name;
      set => SetPropertyValue($"{nameof(Name)}", ref _name, value);
    }

    public string Status
    {
      get => _status;
      set => SetPropertyValue($"{nameof(Status)}", ref _status, value);
    }

    public string TicketId
    {
      get => _ticketId;
      set => SetPropertyValue($"{nameof(TicketId)}", ref _ticketId, value);
    }

    #endregion

    #region Constructors

    public XpTicket(Session session) : base(session)
    {
    }

    public XpTicket()
    {
    }

    #endregion
  }
}