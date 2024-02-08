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

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _dbContext;

    public CategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Category> CreateCategoryAsync(string name)
    {
        try
        {
            ValidateCategoryName(name);

            var category = new Category
            {
                Name = name
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
        catch (Exception ex)
        {
            // Log the error or handle it accordingly
            throw new Exception("Failed to create category.", ex);
        }
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _dbContext.Categories.FindAsync(categoryId);

        if (category == null)
        {
            throw new NotFoundException($"Category with ID {categoryId} not found.");
        }

        return category;
    }

    public async Task UpdateCategoryAsync(int categoryId, string newName)
    {
        var category = await GetCategoryByIdAsync(categoryId);

        if (category != null)
        {
            ValidateCategoryName(newName);

            category.Name = newName;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var categoryToDelete = await GetCategoryByIdAsync(categoryId);

        if (categoryToDelete != null)
        {
            _dbContext.Categories.Remove(categoryToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    private void ValidateCategoryName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be empty or null.");
        }
    }
}
