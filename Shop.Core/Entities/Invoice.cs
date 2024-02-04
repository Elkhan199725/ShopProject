using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Invoice : AbstractClass
{
    public Invoice()
    {
        InvoiceItems = new List<InvoiceItem>(); // Ensure collection is initialized
    }

    public Invoice(decimal? totalPrice, DateTime? invoiceDate, string? paymentMethod, User? user, Wallet? wallet)
        : this()
    {
        TotalPrice = totalPrice;
        InvoiceDate = invoiceDate;
        PaymentMethod = paymentMethod;
        User = user;
        Wallet = wallet;
    }

    public int? Id { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? PaymentMethod { get; set; }
    public int? UserId { get; set; }
    public int? WalletId { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Wallet? Wallet { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}