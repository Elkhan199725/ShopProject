// See https://aka.ms/new-console-template for more information

using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Services;
using Shop.DataAccess.Data_Access;
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
        var loginResult = await AttemptLogin(userService);
        if (!loginResult.Item1) // If login is unsuccessful
        {
            Console.WriteLine("Invalid credentials. Please try again.");
            continue; // Skip the rest of the loop and show the main menu again
        }

        // Login successful, check if user is admin
        bool isAdmin = await userService.IsUserAdmin(loginResult.Item2);
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
        // After returning from AdminPanel or UserPanel, go directly to the start of the loop
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
    Console.WriteLine("\n--- Admin Panel ---");
    // Implement admin-specific functionalities here
    // Placeholder logic to simulate panel interaction
    Console.WriteLine("Press any key to log out...");
    Console.ReadKey();
}

static async Task UserPanel(UserService userService)
{
    Console.WriteLine("\n--- User Panel ---");
    // Implement user-specific functionalities here
    // Placeholder logic to simulate panel interaction
    Console.WriteLine("Press any key to log out...");
    Console.ReadKey();
}
