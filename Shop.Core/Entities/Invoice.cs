﻿using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Invoice:AbstractClass
{
    public int? Id { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? PaymantMethod { get; set; }
    public int? UserId { get; set; }
    public int? WalletId { get; set; }
    public User? User { get; set; }
    public Wallet? Wallet { get; set; }
    public ICollection<InvoiceItem>? InvoiceItems { get; set; }
}