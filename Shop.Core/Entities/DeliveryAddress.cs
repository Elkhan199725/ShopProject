using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class DeliveryAddress : AbstractClass
{
    public DeliveryAddress()
    {
    }

    public DeliveryAddress(string? address, string? postalCode, User? user)
    {
        Address = address;
        PostalCode = postalCode;
        User = user;
    }

    public int? Id { get; set; }
    public string? Address { get; set; } = null!;
    public string? PostalCode { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }

}
