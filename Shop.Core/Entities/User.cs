using System.Collections.ObjectModel;
using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class User : AbstractClass
{
    public User()
    {
        DeliveryAddresses = new List<DeliveryAddress>();
        Wallets = new List<Wallet>();
        Invoices = new List<Invoice>();
        Baskets = new List<Basket>();
        Cards = new List<Card>();
    }

    public User(string? name, string userName, string password, string email, string? phone, bool isAdmin)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Email = email;
        Phone = phone;
        IsAdmin = isAdmin;

        DeliveryAddresses = new List<DeliveryAddress>();
        Wallets = new List<Wallet>();
        Invoices = new List<Invoice>();
        Baskets = new List<Basket>();
        TransactionLogs = new List<TransactionLog>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public bool IsAdmin { get; set; } = false;
    public ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
    public ICollection<Wallet>? Wallets { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<Basket>? Baskets { get; set; }
    public ICollection<Card>? Cards { get; set; }
    public ICollection<TransactionLog> TransactionLogs { get; set; }
}
