using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IWalletService
{
    Task<decimal> GetWalletBalance(int userId, string username, string password);
    Task IncreaseWalletBalance(int userId, int cardId, decimal amount, int cvc, string username, string password);
}