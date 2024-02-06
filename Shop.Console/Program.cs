// See https://aka.ms/new-console-template for more information

using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;
using Shop.Core.Services;
using Shop.DataAccess.Data_Access;
using Shop.DataAccess.Migrations;
using System;

string s = "Welcome to Shop";
Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
Console.WriteLine(s);

UserService userService = new UserService(new AppDbContext());

bool appRun = true;
while (appRun)
{
    Console.WriteLine("\n1) Login\n2) Exit");
    Console.Write("Choose the option: ");
    var choice = Console.ReadLine();

    if (choice == "1") // Login
    {
        var (loginSuccess, usernameOrEmail) = await AttemptLogin(userService);
        if (!loginSuccess) // If login is unsuccessful
        {
            Console.WriteLine("Invalid credentials. Please try again.");
        }
        else
        {
            // Login successful, check if user is admin
            bool isAdmin = await userService.IsUserAdmin(usernameOrEmail);

            if (isAdmin)
            {
                // Admin panel logic here
                await AdminPanel(userService);
            }
            else
            {
                // User panel logic here
                await UserPanel(userService);
            }
        }
    }
    else if (choice == "2") // Exit
    {
        Console.WriteLine("Exiting application...");
        appRun = false;
    }
    else
    {
        Console.WriteLine("Invalid option. Please try again.");
    }
}

static async Task<(bool, string)> AttemptLogin(UserService userService)
{
    Console.Write("Enter username or email: ");
    var usernameOrEmail = Console.ReadLine();
    Console.Write("Enter password: ");
    var password = ReadPassword();

    var loginSuccess = await userService.UserLogin(usernameOrEmail, password);
    return (loginSuccess, usernameOrEmail); // Return both login success and usernameOrEmail
}

static string ReadPassword()
{
    var password = "";
    while (true)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }
        else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            password = password[0..^1];
            Console.Write("\b \b");
        }
        else if (!char.IsControl(key.KeyChar))
        {
            password += key.KeyChar;
            Console.Write("*");
        }
    }
    return password;
}

static async Task AdminPanel(UserService userService)
{
    bool adminSession = true;
    while (adminSession)
    {
        Console.WriteLine("\n--- Admin Panel ---");
        Console.WriteLine("1) Create User");
        Console.WriteLine("2) Update User");
        Console.WriteLine("3) Delete User");
        Console.WriteLine("4) Get All Users");
        Console.WriteLine("5) Get User By Id");
        Console.WriteLine("6) Activate User");
        Console.WriteLine("7) Deactivate User");
        Console.WriteLine("8) Logout");
        Console.Write("Choose an option: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(AdminMenu), choice))
        {
            switch ((AdminMenu)choice)
            {
                case AdminMenu.CreateUser:
                    Console.Write("Enter name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter username: ");
                    var userName = Console.ReadLine();
                    Console.Write("Enter password: "); // Consider masking this input in a real application
                    var password = Console.ReadLine();
                    Console.Write("Enter email: ");
                    var email = Console.ReadLine();
                    Console.Write("Enter phone: ");
                    var phone = Console.ReadLine();
                    Console.Write("Is Admin (true/false): ");
                    bool isAdmin = bool.Parse(Console.ReadLine() ?? "false");

                    var createUserResult = await userService.CreateUser(name, userName, password, email, phone, isAdmin);

                    if (createUserResult == true)
                    {
                        Console.WriteLine($"User '{name}' created successfully.");
                    }
                    else if (createUserResult == null)
                    {
                        Console.WriteLine("A user with the given username or email already exists.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create user.");
                    }
                    break;

                case AdminMenu.UpdateUser:
                    Console.Write("Enter the user ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        Console.Write("Enter new username (leave blank to keep unchanged): ");
                        var newUsername = Console.ReadLine();
                        Console.Write("Enter new email (leave blank to keep unchanged): ");
                        var newEmail = Console.ReadLine();
                        Console.Write("Enter new password (leave blank to keep unchanged): ");
                        var newPassword = Console.ReadLine();
                        Console.Write("Enter new name (leave blank to keep unchanged): ");
                        var newName = Console.ReadLine();
                        Console.Write("Enter new phone (leave blank to keep unchanged): ");
                        var newPhone = Console.ReadLine();

                        var updateResult = await userService.UpdateUser(userId, newUsername, newEmail, newPassword, newName, newPhone);

                        if (updateResult == true)
                        {
                            Console.WriteLine("User updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update user or user not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid user ID.");
                    }
                    break;

                case AdminMenu.DeleteUser:
                    Console.Write("Enter the ID of the user to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int deleteUserId))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid user ID.");
                        break;
                    }

                    // Confirmation before deletion
                    Console.Write($"Are you sure you want to permanently delete the user with ID {deleteUserId}? (yes/no): ");
                    var confirmation = Console.ReadLine();
                    if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        var deleteResult = await userService.DeleteUser(deleteUserId);
                        if (deleteResult)
                        {
                            Console.WriteLine("User deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete user or user not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                    break;

                case AdminMenu.GetAllUsers:
                    var allUsers = await userService.GetAllUsers();

                    if (allUsers.Count > 0)
                    {
                        Console.WriteLine("All Users:");
                        foreach (var user in allUsers)
                        {
                            Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Phone: {user.Phone}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users found.");
                    }
                    break;

                case AdminMenu.GetUserById:
                    Console.Write("Enter the ID of the user to retrieve: ");
                    if (int.TryParse(Console.ReadLine(), out int iUserId))
                    {
                        var user = await userService.GetUserById(iUserId);

                        if (user != null)
                        {
                            Console.WriteLine($"User found: {user.Name}, {user.Email}, {user.Phone}");
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid user ID.");
                    }
                    break;

                case AdminMenu.ActivateUser:
                    Console.Write("Enter the ID of the user to activate: ");
                    var activateUserId = Console.ReadLine();

                    if (int.TryParse(activateUserId, out int idToActivate))
                    {
                        var activationResult = await userService.ActivateUser(idToActivate);

                        if (activationResult)
                        {
                            Console.WriteLine("User activated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to activate user or user not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid user ID.");
                    }
                    break;

                case AdminMenu.DeactivateUser:
                    Console.Write("Enter the ID of the user to deactivate: ");
                    var deactivateUserId = Console.ReadLine();

                    if (int.TryParse(deactivateUserId, out int idToDeactivate))
                    {
                        var deactivationResult = await userService.DeactivateUser(idToDeactivate);

                        if (deactivationResult)
                        {
                            Console.WriteLine("User deactivated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to deactivate user or user not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid user ID.");
                    }
                    break;

                case AdminMenu.Logout:
                    adminSession = false;
                    Console.WriteLine("Logging out...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid number.");
        }
    }
}
static async Task UserPanel(UserService userService)
{
    bool userSession = true;
    while (userSession)
    {
        Console.WriteLine("\n--- User Panel ---");
        Console.WriteLine("1) Get User By Id");
        Console.WriteLine("2) Logout");
        Console.Write("Choose an option: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(UserMenu), choice))
        {
            switch ((UserMenu)choice)
            {
                case UserMenu.GetUserById:
                    Console.Write("Enter the ID of the user to retrieve: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        var user = await userService.GetUserById(userId);

                        if (user != null)
                        {
                            Console.WriteLine($"User found: {user.Name}, {user.Email}, {user.Phone}");
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid user ID.");
                    }
                    break;

                case UserMenu.Logout:
                    userSession = false;
                    Console.WriteLine("Logging out...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
        }
    }
}