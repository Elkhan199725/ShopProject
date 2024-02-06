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

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductById(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {productId} not found.");
        }

        return product;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _dbContext.Products.ToListAsync();

        if (products.Count == 0)
        {
            throw new NotFoundException("No products found.");
        }

        return products;
    }

    public async Task<bool> CreateProduct(string name, string description, decimal price, int quantityAvailable, int categoryId, int brandId, int? discountId)
    {
        var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == categoryId);
        var brandExists = await _dbContext.Brands.AnyAsync(b => b.Id == brandId);
        var discountExists = discountId == null || await _dbContext.Discounts.AnyAsync(d => d.Id == discountId.Value);

        if (!categoryExists)
        {
            throw new NotFoundException($"Category with ID {categoryId} not found.");
        }

        if (!brandExists)
        {
            throw new NotFoundException($"Brand with ID {brandId} not found.");
        }

        if (!discountExists)
        {
            throw new NotFoundException($"Discount with ID {discountId} not found.");
        }

        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            QuantityAvailable = quantityAvailable,
            CategoryId = categoryId,
            BrandId = brandId,
            DiscountId = discountId
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateProduct(int productId, string? newName, string? newDescription, decimal? newPrice, int? newQuantityAvailable, int? newCategoryId, int? newBrandId, int? newDiscountId)
    {
        var existingProduct = await _dbContext.Products.FindAsync(productId);

        if (existingProduct != null)
        {
            var createdTime = existingProduct.Created;

            if (!string.IsNullOrWhiteSpace(newName))
            {
                existingProduct.Name = newName;
            }

            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                existingProduct.Description = newDescription;
            }

            if (newPrice.HasValue)
            {
                existingProduct.Price = newPrice.Value;
            }

            if (newQuantityAvailable.HasValue)
            {
                existingProduct.QuantityAvailable = newQuantityAvailable.Value;
            }

            if (newCategoryId.HasValue)
            {
                var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == newCategoryId.Value);
                if (!categoryExists)
                {
                    throw new NotFoundException($"Category with ID {newCategoryId.Value} not found.");
                }
                existingProduct.CategoryId = newCategoryId.Value;
            }

            if (newBrandId.HasValue)
            {
                var brandExists = await _dbContext.Brands.AnyAsync(b => b.Id == newBrandId.Value);
                if (!brandExists)
                {
                    throw new NotFoundException($"Brand with ID {newBrandId.Value} not found.");
                }
                existingProduct.BrandId = newBrandId.Value;
            }

            if (newDiscountId.HasValue)
            {
                var discountExists = await _dbContext.Discounts.AnyAsync(d => d.Id == newDiscountId.Value);
                if (!discountExists)
                {
                    throw new NotFoundException($"Discount with ID {newDiscountId.Value} not found.");
                }
                existingProduct.DiscountId = newDiscountId.Value;
            }

            existingProduct.Created = createdTime;
            existingProduct.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        return false; // Product not found
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var productToDelete = await _dbContext.Products.FindAsync(productId);

        if (productToDelete != null)
        {
            _dbContext.Products.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        else
        {
            throw new NotFoundException($"Product with ID {productId} not found.");
        }
    }

    public async Task<bool> ActivateProduct(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product != null && product.IsDeleted)
        {
            product.IsDeleted = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeactivateProduct(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product != null && !product.IsDeleted)
        {
            product.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ProductExists(int productId)
    {
        return await _dbContext.Products.AnyAsync(p => p.Id == productId);
    }


}