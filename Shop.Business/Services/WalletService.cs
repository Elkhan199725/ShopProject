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

public class WalletService : IWalletService
{
    private readonly AppDbContext _dbContext;
    private readonly ICardService _cardService;

    public WalletService(AppDbContext dbContext, ICardService cardService)
    {
        _dbContext = dbContext;
        _cardService = cardService;
    }

    public async Task<Wallet?> GetWalletById(int walletId)
    {
        return await _dbContext.Wallets.FindAsync(walletId);
    }

    public async Task<List<Wallet>> GetAllWallets()
    {
        return await _dbContext.Wallets.ToListAsync();
    }

    public async Task<bool> CreateWallet(Wallet wallet, int userId)
    {
        try
        {
            var newWallet = new Wallet
            {
                UserId = userId,
                // No need to set the ID here; it will be generated automatically by the database
            };
            await _dbContext.Wallets.AddAsync(newWallet);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            // Log the error
            return false;
        }
    }

    public async Task<bool> UpdateWallet(int walletId, int userId)
    {
        try
        {
            var wallet = await _dbContext.Wallets.FindAsync(walletId);
            if (wallet == null)
            {
                // Wallet not found
                return false;
            }

            wallet.UserId = userId;
            wallet.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            // Log the error
            return false;
        }
    }

    public async Task<bool> DeleteWallet(int walletId)
    {
        try
        {
            var wallet = await _dbContext.Wallets.FindAsync(walletId);
            if (wallet == null)
                return false;

            _dbContext.Wallets.Remove(wallet);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            // Log the error
            return false;
        }
    }

    public async Task<decimal> GetWalletBalance(int userId)
    {
        try
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet != null)
            {
                return wallet.Balance ?? 0;
            }
            else
            {
                throw new NotFoundException($"Wallet not found for user with ID {userId}.");
            }
        }
        catch (Exception ex)
        {
            // Log the error
            throw new Exception("Failed to retrieve wallet balance.", ex);
        }
    }

    public async Task IncreaseWalletBalance(int walletId, int cardId, decimal amount)
    {
        try
        {
            // Retrieve the wallet associated with the provided ID
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.Id == walletId);
            if (wallet == null)
            {
                throw new NotFoundException($"Wallet not found with ID {walletId}.");
            }

            // Retrieve the card associated with the provided ID
            var card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.Id == cardId);
            if (card == null)
            {
                throw new NotFoundException($"Card not found with ID {cardId}.");
            }

            // Check if the card has sufficient funds
            if (card.Balance < amount)
            {
                throw new InsufficientFundsException($"Insufficient funds in the card with ID {cardId}.");
            }

            // Decrease the card balance
            card.Balance -= amount;

            // Increase the wallet balance
            wallet.Balance += amount;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the error
            throw new Exception("Failed to increase wallet balance.", ex);
        }
    }
    private async Task AuthorizeCardForTransaction(int cardId, int cvc)
    {
        var card = await _cardService.GetCardById(cardId);

        if (card == null || card.Cvc != cvc)
        {
            throw new UnauthorizedAccessException("Card authorization failed.");
        }
    }

    private async Task LogTransactionAsync(int userId, int? cardId, decimal amount, string transactionType)
    {
        var transactionLog = new TransactionLog
        {
            UserId = userId,
            CardId = cardId,
            Amount = amount,
            TransactionType = transactionType,
            Created = DateTime.UtcNow
        };

        await _dbContext.TransactionLogs.AddAsync(transactionLog);
    }
}