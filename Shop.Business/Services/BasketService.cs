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

public class BasketService : IBasketService
{
    private readonly AppDbContext _dbContext;
    private readonly IProductService _productService;

    public BasketService(AppDbContext dbContext, IProductService productService)
    {
        _dbContext = dbContext;
        _productService = productService;
    }

    public async Task<Basket> AddToBasketAsync(int userId, int productId, int quantity)
    {
        await AuthenticateUserAsync(userId);
        await ValidateProductAdditionAsync(userId, productId, quantity);

        var basketItem = new Basket
        {
            UserId = userId,
            ProductId = productId,
            Quantity = quantity
        };

        await _dbContext.Baskets.AddAsync(basketItem);
        await _dbContext.SaveChangesAsync();

        return basketItem;
    }

    public async Task UpdateBasketItemAsync(int basketItemId, int quantity)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (basketItem != null)
        {
            ValidateQuantity(quantity);

            basketItem.Quantity = quantity;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveFromBasketAsync(int basketItemId)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (basketItem != null)
        {
            _dbContext.Baskets.Remove(basketItem);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Basket>> GetBasketItemsAsync(int userId)
    {
        await AuthenticateUserAsync(userId);

        return await _dbContext.Baskets
            .Include(b => b.Product)
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<decimal> CalculateTotalAsync(int userId)
    {
        await AuthenticateUserAsync(userId);

        var basketItems = await _dbContext.Baskets
            .Include(b => b.Product)
            .Where(b => b.UserId == userId)
            .ToListAsync();

        return basketItems.Sum(b => b.Quantity * (b.Product?.Price ?? 0)).GetValueOrDefault();
    }

    private async Task<Basket> GetBasketItemByIdAsync(int basketItemId)
    {
        var basketItem = await _dbContext.Baskets.FindAsync(basketItemId);

        if (basketItem == null)
        {
            throw new NotFoundException($"Basket item with ID {basketItemId} not found.");
        }

        return basketItem;
    }

    private void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }
    }

    private async Task ValidateProductAdditionAsync(int userId, int productId, int quantity)
    {
        ValidateQuantity(quantity);

        var product = await _productService.GetProductById(productId);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {productId} not found.");
        }

        if (quantity > product.QuantityAvailable)
        {
            throw new InsufficientQuantityException("Not enough quantity available for the product.");
        }
    }

    private async Task AuthenticateUserAsync(int userId)
    {
        var authenticatedUser = await _dbContext.Users.FindAsync(userId);

        if (authenticatedUser == null)
        {
            throw new UnauthorizedAccessException("User authentication failed.");
        }
    }
}