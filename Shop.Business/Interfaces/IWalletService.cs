using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IWalletService
{
    Task<int> GetWalletBalance(int userId);
    Task UpdateWalletBalance(int userId, int newBalance);
}