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

    public async Task<bool> CreateInvoiceItem(InvoiceItem newItem)
    {
        try
        {
            // Find the product associated with the item
            var product = await _dbContext.Products.FindAsync(newItem.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product not found with ID {newItem.ProductId}.");
            }

            // Check if the product has enough quantity
            if (product.QuantityAvailable < newItem.Quantity)
            {
                throw new InsufficientQuantityException($"Insufficient quantity for product with ID {newItem.ProductId}.");
            }

            // Calculate the total price
            newItem.TotalPrice = product.Price * newItem.Quantity;

            // Decrease the product quantity
            product.QuantityAvailable -= newItem.Quantity;

            // Add the item to the invoice
            await _dbContext.InvoiceItems.AddAsync(newItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            // Log the error
            return false;
        }
    }
}