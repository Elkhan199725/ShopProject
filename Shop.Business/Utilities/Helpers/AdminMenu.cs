using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Utilities.Helpers;

public enum AdminMenu
{
    CreateUser = 1,
    UpdateUser,
    DeleteUser,
    GetAllUsers,
    GetUserById,
    ActivateUser,
    DeactivateUser,
    CreateProduct,
    UpdateProduct,
    DeleteProduct,
    ActivateProduct,
    DeactivateProduct,
    ProductExists,
    GetAllProducts,
    GetProductById,
    Logout
}