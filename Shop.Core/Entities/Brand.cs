using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Brand:AbstractClass
{
    public Brand(string? name, ICollection<Product>? products)
    {
        Name = name;
        Products = products;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}
