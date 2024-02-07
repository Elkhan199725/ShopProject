using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class InvoiceItem : AbstractClass
{
    public InvoiceItem()
    {
    }

    public InvoiceItem(int? quantity, decimal? totalPrice, Invoice? invoice, Product? product)
    {
        Quantity = quantity;
        Invoice = invoice;
        Product = product;
        TotalPrice = totalPrice;
    }

    public int? Id { get; set; }
    public int? InvoiceId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
    public decimal? TotalPrice { get; set; }

    public Invoice? Invoice { get; set; }
    public Product? Product { get; set; }
}
