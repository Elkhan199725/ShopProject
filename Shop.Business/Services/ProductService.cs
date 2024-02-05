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

    public async Task CreateProduct(string name, string description, decimal price, int quantityAvailable, int categoryId, int brandId, int? discountId)
    {
        var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == categoryId);
        if (!categoryExists)
        {
            throw new NotFoundException($"Category with ID {categoryId} not found.");
        }

        var brandExists = await _dbContext.Brands.AnyAsync(b => b.Id == brandId);
        if (!brandExists)
        {
            throw new NotFoundException($"Brand with ID {brandId} not found.");
        }

        if (discountId.HasValue)
        {
            var discountExists = await _dbContext.Discounts.AnyAsync(d => d.Id == discountId.Value);
            if (!discountExists)
            {
                throw new NotFoundException($"Discount with ID {discountId.Value} not found.");
            }
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
    }

    public async Task UpdateProduct(int productId, string name, string description, decimal price, int quantityAvailable)
    {
        var existingProduct = await _dbContext.Products.FindAsync(productId);

        if (existingProduct != null)
        {
            var createdTime = existingProduct.Created;

            existingProduct.Name = name;
            existingProduct.Description = description;
            existingProduct.Price = price;
            existingProduct.QuantityAvailable = quantityAvailable;

            existingProduct.Created = createdTime;
            existingProduct.Updated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteProduct(int productId)
    {
        var productToDelete = await _dbContext.Products.FindAsync(productId);

        if (productToDelete != null)
        {
            _dbContext.Products.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ProductExists(int productId)
    {
        return await _dbContext.Products.AnyAsync(p => p.Id == productId);
    }
}