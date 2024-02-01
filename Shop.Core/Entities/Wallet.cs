using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Wallet:AbstractClass
{
    public int Id { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public int? Balance { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}
