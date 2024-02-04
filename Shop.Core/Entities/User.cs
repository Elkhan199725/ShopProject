using System.Collections.ObjectModel;
using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class User : AbstractClass
{
    // Empty constructor for database
    public User()
    {
        // Initialize collections
        DeliveryAddresses = new List<DeliveryAddress>();
        Wallets = new List<Wallet>();
        Invoices = new List<Invoice>();
        Baskets = new List<Basket>();
    }

    // Constructor for creating a User with specified properties
    public User(string? name, string userName, string password, string email, string? phone)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Email = email;
        Phone = phone;

        // Initialize collections
        DeliveryAddresses = new List<DeliveryAddress>();
        Wallets = new List<Wallet>();
        Invoices = new List<Invoice>();
        Baskets = new List<Basket>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
    public ICollection<Wallet>? Wallets { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<Basket>? Baskets { get; set; }
}
