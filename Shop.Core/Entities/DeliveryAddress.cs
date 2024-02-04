using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class DeliveryAddress : AbstractClass
{
    public DeliveryAddress()
    {
        User = new User(); // Initialize to an empty User
    }
    public DeliveryAddress(string? address, string? postalCode, User? user)
    {
        Address = address;
        PostalCode = postalCode;
        User = user ?? new User(); // Initialize to the provided user or an empty User if null
    }

    public int? Id { get; set; }
    public string? Address { get; set; } = null!;
    public string? PostalCode { get; set; }
    public User User { get; set; } = new User(); // Initialize to an empty User
    public int? UserId { get; set; }
}