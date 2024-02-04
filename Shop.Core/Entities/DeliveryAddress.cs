using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class DeliveryAddress : AbstractClass
{
    public DeliveryAddress()
    {
        User = new User();
    }
    public DeliveryAddress(string? address, string? postalCode, User? user)
    {
        Address = address;
        PostalCode = postalCode;
        User = user ?? new User();
    }

    public int? Id { get; set; }
    public string? Address { get; set; } = null!;
    public string? PostalCode { get; set; }
    public User User { get; set; } = new User();
    public int? UserId { get; set; }
}