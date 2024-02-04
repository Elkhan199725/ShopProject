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

    public async Task<decimal> GetWalletBalance(int userId, string username, string password)
    {
        await AuthenticateUser(username, password);

        var wallet = await _dbContext.Wallets
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        if (wallet == null)
        {
            throw new NotFoundException($"Wallet not found for user with ID {userId}.");
        }

        return wallet.Balance ?? 0;
    }

    public async Task IncreaseWalletBalance(int userId, int cardId, decimal amount, int cvc, string username, string password)
    {
        await AuthenticateUser(username, password);
        await AuthorizeCardForTransaction(cardId, cvc);

        var cardBalance = await _cardService.GetCardBalanceAsync(cardId);
        if (cardBalance < amount)
        {
            throw new InsufficientFundsException("Insufficient funds in the card.");
        }

        var wallet = await _dbContext.Wallets
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        if (wallet == null)
        {
            throw new NotFoundException($"Wallet not found for user with ID {userId}.");
        }

        wallet.Balance += amount;

        await LogTransactionAsync(userId, cardId, amount, "Credit");

        await _dbContext.SaveChangesAsync();
    }

    private async Task AuthorizeCardForTransaction(int cardId, int cvc)
    {
        var card = await _cardService.GetCardById(cardId);

        if (card == null || card.Cvc != cvc)
        {
            throw new UnauthorizedAccessException("Card authorization failed.");
        }
    }

    private async Task AuthenticateUser(string username, string password)
    {
        var authenticatedUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

        if (authenticatedUser == null)
        {
            throw new UnauthorizedAccessException("User authentication failed.");
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