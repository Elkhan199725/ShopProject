// See https://aka.ms/new-console-template for more information

using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Business.Services;
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
ProductService productService = new ProductService(new AppDbContext());

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
                await AdminPanel(userService, productService);
            }
            else
            {
                // User panel logic here
                await UserPanel(userService, productService);
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

static async Task AdminPanel(UserService userService, ProductService productService)
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
        Console.WriteLine("8) Create Product");
        Console.WriteLine("9) Update Product");
        Console.WriteLine("10) Delete Product");
        Console.WriteLine("11) Activate Product");
        Console.WriteLine("12) Deactivate Product");
        Console.WriteLine("13) Check if Product Exists");
        Console.WriteLine("14) Get All Products");
        Console.WriteLine("15) Get Product By Id");
        Console.WriteLine("16) Logout");
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
                case AdminMenu.CreateProduct:
                    Console.Write("Enter product name: ");
                    var productName = Console.ReadLine();
                    Console.Write("Enter product description: ");
                    var productDescription = Console.ReadLine();
                    Console.Write("Enter product price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                    {
                        Console.WriteLine("Invalid price format. Please enter a valid decimal.");
                        break;
                    }
                    Console.Write("Enter quantity available: ");
                    if (!int.TryParse(Console.ReadLine(), out int productQuantity))
                    {
                        Console.WriteLine("Invalid quantity format. Please enter a valid integer.");
                        break;
                    }
                    Console.Write("Enter category ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int productCategoryId))
                    {
                        Console.WriteLine("Invalid category ID format. Please enter a valid integer.");
                        break;
                    }
                    Console.Write("Enter brand ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int productBrandId))
                    {
                        Console.WriteLine("Invalid brand ID format. Please enter a valid integer.");
                        break;
                    }
                    Console.Write("Enter discount ID (leave blank if none): ");
                    if (!int.TryParse(Console.ReadLine(), out int productDiscountId))
                    {
                        productDiscountId = 0; // Set discount to 0 if not provided
                    }

                    try
                    {
                        await productService.CreateProduct(productName, productDescription, productPrice, productQuantity, productCategoryId, productBrandId, productDiscountId);
                        Console.WriteLine("Product created successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case AdminMenu.UpdateProduct:
                    Console.Write("Enter the product ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        Console.Write("Enter new product name (leave blank to keep unchanged): ");
                        var newName = Console.ReadLine();
                        Console.Write("Enter new product description (leave blank to keep unchanged): ");
                        var newDescription = Console.ReadLine();

                        Console.Write("Enter new product price (leave blank to keep unchanged): ");
                        var priceInput = Console.ReadLine();
                        decimal? newPrice = !string.IsNullOrWhiteSpace(priceInput) ? decimal.Parse(priceInput) : (decimal?)null;

                        Console.Write("Enter new product quantity available (leave blank to keep unchanged): ");
                        var quantityInput = Console.ReadLine();
                        int? newQuantityAvailable = !string.IsNullOrWhiteSpace(quantityInput) ? int.Parse(quantityInput) : (int?)null;

                        Console.Write("Enter new product category ID (leave blank to keep unchanged): ");
                        var categoryIdInput = Console.ReadLine();
                        int? newCategoryId = !string.IsNullOrWhiteSpace(categoryIdInput) ? int.Parse(categoryIdInput) : (int?)null;

                        Console.Write("Enter new product brand ID (leave blank to keep unchanged): ");
                        var brandIdInput = Console.ReadLine();
                        int? newBrandId = !string.IsNullOrWhiteSpace(brandIdInput) ? int.Parse(brandIdInput) : (int?)null;

                        Console.Write("Enter new product discount ID (leave blank to keep unchanged): ");
                        var discountIdInput = Console.ReadLine();
                        int? newDiscountId = !string.IsNullOrWhiteSpace(discountIdInput) ? int.Parse(discountIdInput) : (int?)null;

                        var updateResult = await productService.UpdateProduct(productId, newName, newDescription, newPrice, newQuantityAvailable, newCategoryId, newBrandId, newDiscountId);

                        if (updateResult == true)
                        {
                            Console.WriteLine("Product updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update product or product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case AdminMenu.DeleteProduct:
                    Console.Write("Enter the product ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int productToDeleteId))
                    {
                        // Confirmation before deletion
                        Console.Write($"Are you sure you want to permanently delete the product with ID {productToDeleteId}? (yes/no): ");
                        var productDeleteConfirmation = Console.ReadLine();
                        if (productDeleteConfirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            var productExists = await productService.ProductExists(productToDeleteId);
                            if (productExists)
                            {
                                await productService.DeleteProduct(productToDeleteId);
                                Console.WriteLine("Product deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Product not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case AdminMenu.ActivateProduct:
                    Console.Write("Enter the product ID to activate: ");
                    if (int.TryParse(Console.ReadLine(), out int activateProductId))
                    {
                        var activateResult = await productService.ActivateProduct(activateProductId);
                        if (activateResult)
                        {
                            Console.WriteLine("Product activated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to activate product or product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case AdminMenu.DeactivateProduct:
                    Console.Write("Enter the product ID to deactivate: ");
                    if (int.TryParse(Console.ReadLine(), out int deactivateProductId))
                    {
                        var deactivateResult = await productService.DeactivateProduct(deactivateProductId);
                        if (deactivateResult)
                        {
                            Console.WriteLine("Product deactivated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to deactivate product or product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case AdminMenu.ProductExists:
                    Console.Write("Enter the product ID to check if it exists: ");
                    if (int.TryParse(Console.ReadLine(), out int productIdToCheck))
                    {
                        var productExists = await productService.ProductExists(productIdToCheck);
                        if (productExists)
                        {
                            Console.WriteLine("Product exists.");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case AdminMenu.GetAllProducts:
                    try
                    {
                        var allProducts = await productService.GetAllProducts();

                        Console.WriteLine("\n--- All Products ---");
                        foreach (var product in allProducts)
                        {
                            Console.WriteLine($"ID: {product.Id}");
                            Console.WriteLine($"Name: {product.Name}");
                            Console.WriteLine($"Description: {product.Description}");
                            Console.WriteLine($"Price: {product.Price:C}");
                            Console.WriteLine($"Quantity Available: {product.QuantityAvailable}");
                            Console.WriteLine($"Category ID: {product.CategoryId}");
                            Console.WriteLine($"Brand ID: {product.BrandId}");
                            Console.WriteLine($"Discount ID: {product.DiscountId}");
                            Console.WriteLine($"Created: {product.Created}");
                            Console.WriteLine($"Updated: {product.Updated}");
                            Console.WriteLine("---------------------------------");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while getting all products: {ex.Message}");
                    }
                    break;

                case AdminMenu.GetProductById:
                    Console.Write("Enter the product ID to retrieve: ");
                    if (int.TryParse(Console.ReadLine(), out int productIdToRetrieve))
                    {
                        try
                        {
                            var product = await productService.GetProductById(productIdToRetrieve);

                            Console.WriteLine("\n--- Product Details ---");
                            Console.WriteLine($"ID: {product.Id}");
                            Console.WriteLine($"Name: {product.Name}");
                            Console.WriteLine($"Description: {product.Description}");
                            Console.WriteLine($"Price: {product.Price:C}");
                            Console.WriteLine($"Quantity Available: {product.QuantityAvailable}");
                            Console.WriteLine($"Category ID: {product.CategoryId}");
                            Console.WriteLine($"Brand ID: {product.BrandId}");
                            Console.WriteLine($"Discount ID: {product.DiscountId}");
                            Console.WriteLine($"Created: {product.Created}");
                            Console.WriteLine($"Updated: {product.Updated}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
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
static async Task UserPanel(UserService userService, ProductService productService)
{
    bool userSession = true;
    while (userSession)
    {
        Console.WriteLine("\n--- User Panel ---");
        Console.WriteLine("1) Get User By Id");
        Console.WriteLine("2) Check Product Existence");
        Console.WriteLine("3) Get All Products");
        Console.WriteLine("4) Get Product By Id");
        Console.WriteLine("5) Logout");
        Console.Write("Choose an option: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(UserMenu), choice))
        {
            switch ((UserMenu)choice)
            {
                case UserMenu.GetUserById:
                    Console.Write("Enter your user ID to retrieve your information: ");
                    if (int.TryParse(Console.ReadLine(), out int userIdToRetrieve))
                    {
                        try
                        {
                            var user = await userService.GetUserById(userIdToRetrieve);

                            Console.WriteLine("\n--- User Details ---");
                            Console.WriteLine($"ID: {user.Id}");
                            Console.WriteLine($"Username: {user.UserName}");
                            Console.WriteLine($"Email: {user.Email}");
                            Console.WriteLine($"Name: {user.Name}");
                            Console.WriteLine($"Phone: {user.Phone}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while retrieving user information: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid user ID.");
                    }
                    break;

                case UserMenu.ProductExists:
                    Console.Write("Enter the product ID to check if it exists: ");
                    if (int.TryParse(Console.ReadLine(), out int productIdToCheck))
                    {
                        var productExists = await productService.ProductExists(productIdToCheck);
                        if (productExists)
                        {
                            Console.WriteLine("Product exists.");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                    break;

                case UserMenu.GetAllProducts:
                    try
                    {
                        var products = await productService.GetAllProducts();
                        Console.WriteLine("\n--- All Products ---");
                        foreach (var product in products)
                        {
                            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
                    }
                    break;

                case UserMenu.GetProductById:
                    Console.Write("Enter the product ID to retrieve: ");
                    if (int.TryParse(Console.ReadLine(), out int productIdToRetrieve))
                    {
                        try
                        {
                            var product = await productService.GetProductById(productIdToRetrieve);

                            Console.WriteLine("\n--- Product Details ---");
                            Console.WriteLine($"ID: {product.Id}");
                            Console.WriteLine($"Name: {product.Name}");
                            Console.WriteLine($"Description: {product.Description}");
                            Console.WriteLine($"Price: {product.Price:C}");
                            Console.WriteLine($"Quantity Available: {product.QuantityAvailable}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
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