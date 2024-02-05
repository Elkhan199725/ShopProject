using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IProductService
{
    Task<Product> GetProductById(int productId);
    Task<List<Product>> GetAllProducts();
    Task CreateProduct(string name, string description, decimal price, int quantityAvailable, int categoryId, int brandId, int? discountId);
    Task UpdateProduct(int productId, string name, string description, decimal price, int quantityAvailable);
    Task DeleteProduct(int productId);
    Task<bool> ProductExists(int productId);
}