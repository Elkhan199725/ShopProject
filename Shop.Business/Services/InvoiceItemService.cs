using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Services;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly AppDbContext _dbContext;

    public InvoiceItemService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InvoiceItem> CreateInvoiceItemAsync(int quantity, decimal price, int invoiceId, int productId)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }

        if (price <= 0)
        {
            throw new ArgumentException("Price must be a positive decimal.");
        }

        var invoice = await _dbContext.Invoices.FindAsync(invoiceId);
        var product = await _dbContext.Products.FindAsync(productId);

        if (invoice == null || product == null)
        {
            throw new NotFoundException("Invoice or Product not found.");
        }

        var invoiceItem = new InvoiceItem
        {
            Quantity = quantity,
            Price = price,
            InvoiceId = invoiceId,
            ProductId = productId
        };

        await _dbContext.InvoiceItems.AddAsync(invoiceItem);
        await _dbContext.SaveChangesAsync();

        return invoiceItem;
    }

    public async Task<List<InvoiceItem>> GetInvoiceItemsAsync(int invoiceId)
    {
        var invoice = await _dbContext.Invoices.FindAsync(invoiceId);

        if (invoice == null)
        {
            throw new NotFoundException($"Invoice with ID {invoiceId} not found.");
        }

        return await _dbContext.InvoiceItems
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceId == invoiceId)
            .ToListAsync();
    }

    public async Task<decimal> CalculateTotalAsync(int invoiceId)
    {
        var invoice = await _dbContext.Invoices.FindAsync(invoiceId);

        if (invoice == null)
        {
            throw new NotFoundException($"Invoice with ID {invoiceId} not found.");
        }

        var invoiceItems = await _dbContext.InvoiceItems
            .Where(ii => ii.InvoiceId == invoiceId)
            .ToListAsync();

        return invoiceItems.Sum(ii => ii.Quantity * (ii.Product?.Price ?? 0)).GetValueOrDefault();
    }
}