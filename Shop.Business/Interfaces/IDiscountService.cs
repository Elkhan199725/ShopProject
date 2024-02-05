using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IDiscountService
{
    Task<Discount> CreateDiscountAsync(string name, string description, decimal discountPercentage, DateTime startDate, DateTime endDate);
    Task<List<Discount>> GetAllDiscountsAsync();
    Task<Discount> GetDiscountByIdAsync(int discountId);
    Task UpdateDiscountAsync(int discountId, string newName, string newDescription, decimal newDiscountPercentage, DateTime newStartDate, DateTime newEndDate);
    Task DeleteDiscountAsync(int discountId);
}
