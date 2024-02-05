using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IBasketService
{
    Task<Basket> AddToBasketAsync(int userId, int productId, int quantity);
    Task UpdateBasketItemAsync(int basketItemId, int quantity);
    Task RemoveFromBasketAsync(int basketItemId);
    Task<List<Basket>> GetBasketItemsAsync(int userId);
    Task<decimal> CalculateTotalAsync(int userId);
}
