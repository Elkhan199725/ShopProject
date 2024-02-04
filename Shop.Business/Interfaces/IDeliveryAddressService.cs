using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IDeliveryAddressService
{
    Task CreateDeliveryAddress(string address, string postalCode, int userId);
    Task<List<DeliveryAddress>> GetAllDeliveryAddresses(int userId);
    Task<DeliveryAddress?> GetDeliveryAddressById(int deliveryAddressId);
    Task UpdateDeliveryAddress(int deliveryAddressId, string newAddress, string newPostalCode);
    Task DeleteDeliveryAddress(int deliveryAddressId);
}