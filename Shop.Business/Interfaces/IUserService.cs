using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User> GetUserByEmail(string? userEmail);
    Task CreateUser(User? user, string? name, string? username, string? userEmail, string? userPassword, string? phoneNumber);
    Task UpdateUser(string name, string newUsername, string newUserEmail, string newUserPassword, string phoneNumber);
    Task DeleteUser(string? userEmail);
    Task ActivateUser(int userId);
    Task DeactivateUser(int userId);
    Task<bool> UserLogin(string usernameOrEmail, string password);
}