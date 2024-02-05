using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IBrandService
{
    Task<Brand> CreateBrandAsync(string name);
    Task<List<Brand>> GetAllBrandsAsync();
    Task<Brand> GetBrandByIdAsync(int brandId);
    Task UpdateBrandAsync(int brandId, string newName);
    Task DeleteBrandAsync(int brandId);
    Task<bool> BrandExistsAsync(int brandId);
}
