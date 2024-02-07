using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IWalletService
{
    Task<Wallet> GetWalletById(int walletId);
    Task<List<Wallet>> GetAllWallets();
    Task<bool> CreateWallet(Wallet newWallet, int userId);
    Task<bool> UpdateWallet(int walletId, int userId);
    Task<bool> DeleteWallet(int walletId);
    Task<decimal> GetWalletBalance(int userId);
    Task IncreaseWalletBalance(int walletId, int cardId, decimal amount);
}