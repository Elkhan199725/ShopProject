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

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _dbContext;
    private readonly ICardService _cardService;
    private readonly InvoiceItemService _invoiceItemService;

    public InvoiceService(AppDbContext dbContext, ICardService cardService, InvoiceItemService invoiceItemService)
    {
        _dbContext = dbContext;
        _cardService = cardService;
        _invoiceItemService = invoiceItemService;
    }

    public async Task<bool> CreateInvoice(List<int> invoiceItemIds, int cardId, int userId)
    {
        try
        {
            // Get the invoice items
            var invoiceItems = await _dbContext.InvoiceItems
                .Where(item => invoiceItemIds.Contains((int)item.Id))
                .ToListAsync();

            // Calculate the total price of the invoice
            decimal totalPrice = invoiceItems.Sum(item => item.TotalPrice ?? 0);

            // Retrieve the card associated with the provided ID
            var card = await _dbContext.Cards.FindAsync(cardId);
            if (card == null)
            {
                throw new NotFoundException($"Card not found with ID {cardId}.");
            }

            // Ensure the card has sufficient balance
            if (card.Balance < totalPrice)
            {
                throw new InsufficientFundsException($"Insufficient funds in the card with ID {cardId}.");
            }

            // Create the invoice
            var invoice = new Invoice
            {
                InvoiceDate = DateTime.UtcNow,
                Status = InvoiceStatus.Paid,
                UserId = userId
            };

            // Update the database
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();

            // Update the card balance
            card.Balance -= totalPrice;
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            // Log the error
            throw new Exception("Failed to create invoice.", ex);
        }
    }
}

