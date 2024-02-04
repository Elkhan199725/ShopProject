using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.Data_Access;

namespace Shop.Business.Services;

internal class DeliveryAddressService : IDeliveryAddressService
{
    private readonly AppDbContext _dbContext;

    public DeliveryAddressService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateDeliveryAddress(string address, string postalCode, int userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            Console.WriteLine($"User with ID {userId} not found. Unable to create delivery address.");
            return;
        }

        var existingAddress = await _dbContext.DeliveryAddresses
            .FirstOrDefaultAsync(da => da.User.Id == userId && (da.Address == address || da.PostalCode == postalCode));

        if (existingAddress == null)
        {
            await _dbContext.DeliveryAddresses.AddAsync
                (
                new DeliveryAddress
                {
                    Address = address,
                    PostalCode = postalCode,
                    User = user,
                    Created = DateTime.UtcNow
                }
                );

            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Delivery address created successfully.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("A delivery address with the same details already exists.");
            Console.ResetColor();
        }
    }

    public async Task DeleteDeliveryAddress(int addressId)
    {
        var addressToDelete = await _dbContext.DeliveryAddresses.FindAsync(addressId);

        if (addressToDelete != null)
        {
            _dbContext.DeliveryAddresses.Remove(addressToDelete);
            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Delivery address with ID {addressId} deleted successfully.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"Delivery address with ID {addressId} not found. Unable to delete.");
        }
    }

    public async Task<List<DeliveryAddress>> GetAllDeliveryAddresses(int userId)
    {
        var addresses = await _dbContext.DeliveryAddresses
            .Where(da => da.UserId == userId)
            .ToListAsync();

        return addresses;
    }

    public async Task<DeliveryAddress?> GetDeliveryAddressById(int deliveryAddressId)
    {
        var address = await _dbContext.DeliveryAddresses
            .FirstOrDefaultAsync(da => da.Id == deliveryAddressId);

        return address;
    }

    public async Task UpdateDeliveryAddress(int addressId, string newAddress, string newPostalCode)
    {
        var addressToUpdate = await _dbContext.DeliveryAddresses.FindAsync(addressId);

        if (addressToUpdate != null)
        {
            var existingAddress = await _dbContext.DeliveryAddresses
                .FirstOrDefaultAsync(da => da.User.Id == addressToUpdate.User.Id && (da.Address == newAddress || da.PostalCode == newPostalCode));

            if (existingAddress == null || existingAddress.Id == addressId)
            {
                DateTime? currentCreated = addressToUpdate.Created;

                addressToUpdate.Address = newAddress;
                addressToUpdate.PostalCode = newPostalCode;
                addressToUpdate.Updated = DateTime.UtcNow;

                addressToUpdate.Created = currentCreated;

                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Delivery address with ID {addressId} updated successfully. Updated timestamp: {addressToUpdate.Updated}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("A delivery address with the same details already exists.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine($"Delivery address with ID {addressId} not found. Unable to update.");
        }
    }
}
