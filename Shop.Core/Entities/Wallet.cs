using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Wallet : AbstractClass
{
    public Wallet()
    {
        Cards = new List<Card>();
        Invoices = new List<Invoice>();
    }

    public Wallet(decimal? balance, int? userId)
        : this()
    {
        Balance = balance;
        UserId = userId;
    }

    public int? Id { get; set; }
    public decimal? Balance { get; set; }
    public int? UserId { get; set; }

    public User? User { get; set; }
    public ICollection<Card> Cards { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}