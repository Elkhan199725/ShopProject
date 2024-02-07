using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Utilities.Helpers;

public enum UserMenu
{
    GetUserById = 1,
    ProductExists,
    GetAllProducts,
    GetProductById,
    GetCardById,
    GetAllCards,
    CreateCard,
    UpdateCard,
    DeleteCard,
    IncreaseCardBalance,
    DecreaseCardBalance,
    CardExists,
    GetCardBalance,
    GetWalletById,
    GetAllWallets,
    CreateWallet,
    UpdateWallet,
    DeleteWallet,
    GetWalletBalance,
    IncreaseWalletBalance,
    CreateInvoiceItem,
    CreateInvoice,
    Logout
}