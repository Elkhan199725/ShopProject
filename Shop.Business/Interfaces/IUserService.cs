using Shop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(int userId);
    Task<bool?> CreateUser(string name, string userName, string password, string email, string phone, bool isAdmin);
    Task<bool?> UpdateUser(int userId, string newUsername, string newEmail, string newPassword, string newName, string newPhone);
    Task<bool> DeleteUser(int userId);
    Task<bool> ActivateUser(int userId);
    Task<bool> DeactivateUser(int userId);
    Task<bool> IsUserAdmin(string userName);
    Task<bool> UserLogin(string usernameOrEmail, string password);
}