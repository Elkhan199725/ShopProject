using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Services;

public class BrandService : IBrandService
{
    private readonly AppDbContext _dbContext;

    public BrandService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Brand> CreateBrandAsync(string name)
    {
        ValidateBrandName(name);

        var brand = new Brand
        {
            Name = name
        };

        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();

        return brand;
    }

    public async Task<List<Brand>> GetAllBrandsAsync()
    {
        return await _dbContext.Brands.ToListAsync();
    }

    public async Task<Brand> GetBrandByIdAsync(int brandId)
    {
        return await _dbContext.Brands.FindAsync(brandId);
    }

    public async Task UpdateBrandAsync(int brandId, string newName)
    {
        ValidateBrandName(newName);

        var brand = await _dbContext.Brands.FindAsync(brandId);

        if (brand != null)
        {
            brand.Name = newName;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteBrandAsync(int brandId)
    {
        var brandToDelete = await _dbContext.Brands.FindAsync(brandId);

        if (brandToDelete != null)
        {
            _dbContext.Brands.Remove(brandToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> BrandExistsAsync(int brandId)
    {
        return await _dbContext.Brands.AnyAsync(b => b.Id == brandId);
    }

    private void ValidateBrandName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Brand name cannot be empty.");
        }

    }
}
