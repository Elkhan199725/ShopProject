using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IInvoiceItemService
{
    Task<InvoiceItem> CreateInvoiceItemAsync(int quantity, decimal price, int invoiceId, int productId);
    Task<List<InvoiceItem>> GetInvoiceItemsAsync(int invoiceId);
    Task<decimal> CalculateTotalAsync(int invoiceId);
}