using System.Collections.ObjectModel;
using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class User:AbstractClass
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
    public ICollection<Wallet>? Wallets { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}
