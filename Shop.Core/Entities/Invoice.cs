using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Invoice : AbstractClass
{
    public Invoice()
    {
        InvoiceItems = new List<InvoiceItem>();
        Users = new List<User>();
        Cards = new List<Card>();
    }

    public int? Id { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public int? UserId { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;

    public User? User { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Card> Cards { get; set; }
}

public enum InvoiceStatus
{
    Unpaid,
    Paid
}