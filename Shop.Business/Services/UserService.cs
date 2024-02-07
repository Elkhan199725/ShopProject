using Microsoft.EntityFrameworkCore;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.Data_Access;
using Shop.DataAccess.Migrations;

namespace Shop.Core.Services
{
    public class UserService : IUserService
    {
        public readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
        }

        public async Task<bool?> CreateUser(string name, string userName, string password, string email, string phone, bool isAdmin)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == userName || u.Email == email))
            {
                return null; // User already exists
            }

            var user = new User
            {
                Name = name,
                UserName = userName,
                Password = password,
                Email = email,
                Phone = phone,
                IsUserAdmin = isAdmin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool?> UpdateUser(int userId, string newUsername, string newEmail, string newPassword, string newName, string newPhone)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || user.IsDeleted)
            {
                return null; // User not found or deleted
            }

            // Only update fields if the new value is not null and not empty
            user.UserName = !string.IsNullOrWhiteSpace(newUsername) ? newUsername : user.UserName;
            user.Email = !string.IsNullOrWhiteSpace(newEmail) ? newEmail : user.Email;
            user.Password = !string.IsNullOrWhiteSpace(newPassword) ? newPassword : user.Password; // Ensure password hashing
            user.Name = !string.IsNullOrWhiteSpace(newName) ? newName : user.Name;
            user.Phone = !string.IsNullOrWhiteSpace(newPhone) ? newPhone : user.Phone;
            user.Updated = DateTime.UtcNow; // Always update the 'Updated' timestamp

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user); // Perform the hard delete
                await _context.SaveChangesAsync();
                return true; // Indicate success
            }
            return false; // User not found, indicate failure
        }

        public async Task<bool> ActivateUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && user.IsDeleted)
            {
                user.IsDeleted = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeactivateUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && !user.IsDeleted)
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> IsUserAdmin(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
            return user != null && user.IsUserAdmin;
        }
        public async Task<bool> UserLogin(string usernameOrEmail, string password)
        {
            // Attempt to find a user by username or email. This assumes passwords are stored in plain text.
            // In a real application, ensure passwords are securely hashed and compared.
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    (u.UserName == usernameOrEmail || u.Email == usernameOrEmail) &&
                    u.Password == password &&
                    !u.IsDeleted);

            return user != null;
        }
        public async Task<bool> RegisterUser(string username, string email, string password)
        {
            // Check if the username or email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == email);
            if (existingUser != null)
            {
                // Username or email already exists, registration failed
                return false;
            }

            // Create a new user entity
            var newUser = new User
            {
                UserName = username,
                Email = email,
                Password = password // Note: You should hash the password before storing it in the database for security
            };

            try
            {
                // Add the new user to the database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return true; // Registration successful
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during user registration
                Console.WriteLine($"Error registering user: {ex.Message}");
                return false; // Registration failed
            }
        }
    }
}