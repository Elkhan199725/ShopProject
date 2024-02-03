using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserByEmail(string? userEmail);
    public Task CreateUser(User? user, string? name, string? username, string? userEmail, string? userPassword, string? phoneNumber);
    public Task UpdateUser(string name, string newUsername, string newUserEmail, string newUserPassword, string phoneNumber);
    public Task DeleteUser(string? userEmail);
}
