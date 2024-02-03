using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Product : AbstractClass
{
    public Product()
    {
    }

    public Product(string? name, string? description, decimal? price, int? quantityAvailable, Category? category, Brand? brand, Discount? discount, ICollection<Basket>? baskets, ICollection<InvoiceItem>? invoiceItems)
    {
        Name = name;
        Description = description;
        Price = price;
        QuantityAvailable = quantityAvailable;
        Category = category;
        Brand = brand;
        Discount = discount;
        Baskets = baskets;
        InvoiceItems = invoiceItems;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? QuantityAvailable { get; set; }
    public Category? Category { get; set; }
    public int? CategoryId { get; set; }
    public Brand? Brand { get; set; }
    public int? BrandId { get; set; }
    public Discount? Discount { get; set; }
    public int? DiscountId { get; set; }
    public ICollection<Basket>? Baskets { get; set; }
    public ICollection<InvoiceItem>? InvoiceItems { get; set; }
}
