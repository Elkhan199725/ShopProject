﻿using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class InvoiceItem:AbstractClass
{
    public InvoiceItem(int? quantity, decimal? price, Invoice? invoice, Product? product)
    {
        Quantity = quantity;
        Price = price;
        Invoice = invoice;
        Product = product;
    }

    public int Id { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public int? InvoiceId { get; set; }
    public int? ProductId { get; set; }
    public Invoice? Invoice { get; set; }
    public Product? Product { get; set; }
}
