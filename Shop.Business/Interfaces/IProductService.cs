﻿using Shop.Core.Entities;
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
    Task<bool> CreateProduct(string name, string description, decimal price, int quantityAvailable, int categoryId, int brandId, int? discountId);
    Task<bool> UpdateProduct(int productId, string? newName, string? newDescription, decimal? newPrice, int? newQuantityAvailable, int? newCategoryId, int? newBrandId, int? newDiscountId);
    Task<bool> DeleteProduct(int productId);
    Task<bool> ActivateProduct(int productId);
    Task<bool> DeactivateProduct(int productId);
    Task<bool> ProductExists(int productId);
}