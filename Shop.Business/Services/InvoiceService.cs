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

    public InvoiceService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateInvoice(Invoice invoice, List<InvoiceItem> invoiceItems)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var wallet = await _dbContext.Wallets
                    .Where(w => w.UserId == invoice.UserId)
                    .FirstOrDefaultAsync();

                if (wallet == null)
                {
                    throw new WalletNotFoundException("Wallet not found for the user.");
                }

                decimal totalPrice = invoiceItems.Sum(item => (item.Price ?? 0) * (item.Quantity ?? 1));

                if (wallet.Balance < totalPrice)
                {
                    throw new InsufficientFundsException("Insufficient funds in the wallet.");
                }

                wallet.Balance -= totalPrice;
                wallet.Updated = DateTime.UtcNow;

                _dbContext.Wallets.Update(wallet);

                Console.WriteLine("Enter payment method: ");
                string? paymentMethod = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(paymentMethod))
                {
                    throw new ArgumentException("Payment method cannot be null or empty.");
                }

                invoice.PaymentMethod = paymentMethod;
                invoice.TotalPrice = totalPrice;
                invoice.User = wallet.User;
                invoice.Wallet = wallet;

                invoice.Created = DateTime.UtcNow;

                await _dbContext.Invoices.AddAsync(invoice);
                await _dbContext.InvoiceItems.AddRangeAsync(invoiceItems);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (WalletNotFoundException ex)
            {

                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task DeleteInvoice(int invoiceId)
    {
        try
        {
            var invoiceToDelete = await _dbContext.Invoices.FindAsync(invoiceId);

            if (invoiceToDelete != null)
            {
                _dbContext.Invoices.Remove(invoiceToDelete);
                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Invoice with ID {invoiceId} deleted successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Invoice with ID {invoiceId} not found. Unable to delete.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    public async Task<Invoice?> GetInvoiceById(int invoiceId)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        return invoice;
    }
    public async Task<List<Invoice>> GetAllInvoices()
    {
        var allInvoices = await _dbContext.Invoices
            .Include(i => i.InvoiceItems)
            .ToListAsync();

        return allInvoices;
    }

    public async Task<List<Invoice>> GetInvoicesByUser(int userId)
    {
        var invoices = await _dbContext.Invoices
            .Include(i => i.InvoiceItems)
            .Where(i => i.UserId == userId)
            .ToListAsync();

        return invoices;
    }

    public async Task UpdateInvoice(Invoice updatedInvoice, List<InvoiceItem> updatedItems)
    {
        try
        {
            var existingInvoice = await _dbContext.Invoices.Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.Id == updatedInvoice.Id);

            if (existingInvoice != null)
            {
                var createdTime = existingInvoice.Created;

                _dbContext.Entry(existingInvoice).CurrentValues.SetValues(updatedInvoice);

                existingInvoice.Created = createdTime;

                if (updatedItems != null && updatedItems.Any())
                {
                    foreach (var updatedItem in updatedItems)
                    {
                        var existingItem = existingInvoice.InvoiceItems.FirstOrDefault(i => i.Id == updatedItem.Id);

                        if (existingItem != null)
                        {
                            _dbContext.Entry(existingItem).CurrentValues.SetValues(updatedItem);
                        }
                        else
                        {
                            existingInvoice.InvoiceItems.Add(updatedItem);
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Invoice with ID {updatedInvoice.Id} updated successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Invoice with ID {updatedInvoice.Id} not found. Unable to update.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }
}

