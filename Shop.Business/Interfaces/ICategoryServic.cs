using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(string name);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task UpdateCategoryAsync(int categoryId, string newName);
    Task DeleteCategoryAsync(int categoryId);
}