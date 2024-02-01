using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class DeliveryAddress:AbstractClass
{
    public int? Id { get; set;}
    public string? Address { get; set; } = null!;
    public string? PostalCode { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }

}
