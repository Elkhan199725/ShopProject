using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserByEmail(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail && !u.IsDeleted);
        }

        public async Task<User?> CreateUser(string name, string userName, string password, string email, string phone, bool isAdmin)
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
            return user;
        }

        public async Task<User?> UpdateUser(int userId, string newUsername, string newEmail, string newPassword, string newName, string newPhone)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.IsDeleted)
            {
                return null; // User not found or deleted
            }
            DateTime? dateTime = user.Created;
            user.UserName = newUsername;
            user.Email = newEmail;
            user.Password = newPassword; // In a real application, ensure this is securely hashed
            user.Name = newName;
            user.Phone = newPhone;
            user.Updated = DateTime.UtcNow; // Update the timestamp, but keep Created unchanged
            user.Created = dateTime;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
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
    }
}