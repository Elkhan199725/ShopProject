using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Wallet : AbstractClass
{
    public Wallet()
    {
        Invoices = new List<Invoice>();
    }

    public Wallet(string? cardHolderName, string? cardNumber, int? balance, User? user)
        : this()
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        Balance = balance;
        User = user;
    }

    public int? Id { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public decimal? Balance { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}