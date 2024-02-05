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

public class DiscountService : IDiscountService
{
    private readonly AppDbContext _dbContext;

    public DiscountService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Discount> CreateDiscountAsync(string name, string description, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        ValidateDiscountInput(name, description, discountPercentage, startDate, endDate);

        var discount = new Discount
        {
            Name = name,
            Description = description,
            DiscountPercentage = discountPercentage,
            StartDate = startDate,
            EndDate = endDate
        };

        _dbContext.Discounts.Add(discount);
        await _dbContext.SaveChangesAsync();

        return discount;
    }

    public async Task<List<Discount>> GetAllDiscountsAsync()
    {
        return await _dbContext.Discounts.ToListAsync();
    }

    public async Task<Discount> GetDiscountByIdAsync(int discountId)
    {
        var discount = await _dbContext.Discounts.FindAsync(discountId);

        if (discount == null)
        {
            throw new NotFoundException($"Discount with ID {discountId} not found.");
        }

        return discount;
    }

    public async Task UpdateDiscountAsync(int discountId, string newName, string newDescription, decimal newDiscountPercentage, DateTime newStartDate, DateTime newEndDate)
    {
        var discount = await GetDiscountByIdAsync(discountId);

        if (discount != null)
        {
            ValidateDiscountInput(newName, newDescription, newDiscountPercentage, newStartDate, newEndDate);

            DateTime createdTime = discount.Created ?? DateTime.UtcNow;

            discount.Name = newName;
            discount.Description = newDescription;
            discount.DiscountPercentage = newDiscountPercentage;
            discount.StartDate = newStartDate;
            discount.EndDate = newEndDate;
            discount.Created = createdTime;
            discount.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteDiscountAsync(int discountId)
    {
        var discountToDelete = await GetDiscountByIdAsync(discountId);

        if (discountToDelete != null)
        {
            _dbContext.Discounts.Remove(discountToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    private void ValidateDiscountInput(string name, string description, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Discount name cannot be empty or null.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Discount description cannot be empty or null.");
        }

        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentException("Discount percentage must be between 0 and 100.");
        }

        if (startDate >= endDate)
        {
            throw new ArgumentException("Start date must be before the end date.");
        }

        // You can add more validation logic based on your requirements
    }
}