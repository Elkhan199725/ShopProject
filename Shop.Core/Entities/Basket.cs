using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Basket : AbstractClass
{
    public Basket(int? quantity, User? user, Product? product)
    {
        Quantity = quantity;
        User = user;
        Product = product;
    }

    public int Id { get; set; }
    public int? Quantity { get; set; }
    public User? User { get; set; }
    public Product? Product { get; set; }
    public int? UserId { get; set; }
    public int? ProductId { get; set; }
}
