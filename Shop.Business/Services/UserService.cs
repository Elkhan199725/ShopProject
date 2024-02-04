using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.Data_Access;

namespace Shop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUser(User user, string? name, string? username, string? userEmail, string? userPassword, string? phoneNumber)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(userEmail) || String.IsNullOrEmpty(userPassword))
                throw new EmptyNameException("Name, username, email, and password cannot be null or empty");

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (existingUser == null)
            {
                user.Name = name;
                user.UserName = username;
                user.Email = userEmail;
                user.Password = userPassword;
                user.Phone = phoneNumber;
                user.Created = DateTime.UtcNow;

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User: {username} {userEmail} successfully created");
                Console.ResetColor();
            }
            
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"User with email {userEmail} already exists");
                Console.ResetColor();
            }
            
        }

        public async Task DeleteUser(string? userEmail)
        {
            if (String.IsNullOrEmpty(userEmail))
                throw new EmptyNameException("User email cannot be null or empty");

            var userToDelete = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == userEmail);

            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with email {userEmail} successfully deleted");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("User not found for the given email. Deletion aborted.");
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var allUsers = await _dbContext.Users.ToListAsync();

            if (allUsers.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Total {allUsers.Count} users found");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("No users found in the database");
            }

            return allUsers;
        }

        public async Task<User?> GetUserByEmail(string? userEmail)
        {
            if (String.IsNullOrEmpty(userEmail))
                throw new EmptyNameException("User email cannot be null or empty");

            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == userEmail);

            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with email {userEmail} found");
                Console.ResetColor();
                return user;
            }
            else
            {
                Console.WriteLine($"User not found for the given email: {userEmail}");
                return null;
            }
        }

        public async Task UpdateUser(string newUsername, string newEmail, string newPassword, string newName, string newPhone)
        {
            if (String.IsNullOrEmpty(newUsername) || String.IsNullOrEmpty(newEmail) || String.IsNullOrEmpty(newPassword))
                throw new EmptyNameException("Username, email, and password cannot be null or empty");

            var userToUpdate = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == newEmail);

            if (userToUpdate != null)
            {
                DateTime? currentCreated = userToUpdate.Created;

                userToUpdate.UserName = newUsername;
                userToUpdate.Email = newEmail;
                userToUpdate.Password = newPassword;
                userToUpdate.Name = newName;
                userToUpdate.Phone = newPhone;
                userToUpdate.Updated = DateTime.UtcNow;
                userToUpdate.Created = currentCreated;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User: {newUsername} {newEmail} successfully updated!");
                Console.ResetColor();

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("User not found for the given email.");
            }
        }
        public async Task ActivateUser(int userId)
        {
            var userToActivate = await _dbContext.Users.FindAsync(userId);

            if (userToActivate != null)
            {
                DateTime? currentCreated = userToActivate.Created;

                userToActivate.IsDeleted = false;
                userToActivate.Updated = DateTime.UtcNow;
                userToActivate.Created = currentCreated;

                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with ID {userId} activated successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found. Unable to activate.");
            }
        }

        public async Task DeactivateUser(int userId)
        {
            var userToDeactivate = await _dbContext.Users.FindAsync(userId);

            if (userToDeactivate != null)
            {
                DateTime? currentCreated = userToDeactivate.Created;

                userToDeactivate.IsDeleted = true;
                userToDeactivate.Updated = DateTime.UtcNow;
                userToDeactivate.Created = currentCreated;

                await _dbContext.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with ID {userId} deactivated successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found. Unable to deactivate.");
            }
        }
    }
}