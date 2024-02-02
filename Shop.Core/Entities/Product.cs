using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Product:AbstractClass
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? QuantityAvailable { get; set; }
    public ICollection<Basket>? Baskets { get; set; }
    public ICollection<InvoiceItem>? InvoiceItems { get; set; }
}
