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

public class CardService : ICardService
{
    private readonly AppDbContext _dbContext;

    public CardService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Card> GetCardById(int cardId)
    {
        var card = await _dbContext.Cards.FindAsync(cardId);

        if (card == null)
        {
            throw new NotFoundException($"Card with ID {cardId} not found.");
        }

        return card;
    }
    public async Task IncreaseCardBalance(int cardId, decimal amount)
    {
        var card = await _dbContext.Cards.FindAsync(cardId);

        if (card != null)
        {
            var createdTime = card.Created;

            card.Balance += amount;

            card.Created = createdTime;
            card.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<decimal> GetCardBalanceAsync(int cardId)
    {
        var card = await _dbContext.Cards.FindAsync(cardId);

        if (card != null)
        {
            return card.Balance;
        }

        throw new NotFoundException($"Card with ID {cardId} not found.");
    }

    public async Task DecreaseCardBalance(int cardId, decimal amount)
    {
        var card = await _dbContext.Cards.FindAsync(cardId);

        if (card != null)
        {
            var createdTime = card.Created;

            if (card.Balance >= amount)
            {
                card.Balance -= amount;

                card.Created = createdTime; // Restore the created time
                card.Updated = DateTime.UtcNow; // Update the modified time

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InsufficientFundsException("Insufficient balance to perform the transaction.");
            }
        }
    }
    public async Task<List<Card>> GetAllCards()
    {
        var cards = await _dbContext.Cards.ToListAsync();

        if (cards.Count == 0)
        {
            throw new NotFoundException("No cards found.");
        }

        return cards;
    }

    public async Task CreateCard(int userId, string cardNumber, string cardHolderName, int cvc)
    {
        var card = new Card
        {
            CardNumber = cardNumber,
            CardHolderName = cardHolderName,
            Cvc = cvc
        };

        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        card.User = user;

        card.Created = DateTime.UtcNow;
        await _dbContext.Cards.AddAsync(card);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCard(int cardId, string cardNumber, string cardHolderName, int cvc)
    {
        var existingCard = await _dbContext.Cards.FindAsync(cardId);

        if (existingCard != null)
        {
            var createdTime = existingCard.Created;

            existingCard.CardNumber = cardNumber;
            existingCard.CardHolderName = cardHolderName;
            existingCard.Cvc = cvc;

            existingCard.Created = createdTime;
            existingCard.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCard(int cardId)
    {
        var cardToDelete = await _dbContext.Cards.FindAsync(cardId);

        if (cardToDelete != null)
        {
            _dbContext.Cards.Remove(cardToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> CardExists(int cardId)
    {
        return await _dbContext.Cards.AnyAsync(c => c.Id == cardId);
    }


}