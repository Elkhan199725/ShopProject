using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface ICardService
{
    Task<Card> GetCardById(int cardId);
    Task<List<Card>> GetAllCards();
    Task CreateCard(int userId, string cardNumber, string cardHolderName, int cvc);
    Task UpdateCard(int cardId, string cardNumber, string cardHolderName, int cvc);
    Task DeleteCard(int cardId);
    Task<decimal> GetCardBalanceAsync(int cardId);
    Task<bool> CardExists(int cardId);
    Task IncreaseCardBalance(int cardId, decimal amount);
    Task DecreaseCardBalance(int cardId, decimal amount);
}