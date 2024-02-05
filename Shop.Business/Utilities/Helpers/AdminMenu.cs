using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Utilities.Helpers;

internal enum AdminMenu
{
    //User Service
    CreateUser =1,
    UpdateUser,
    DeleteUser,
    GetAllUsers,
    GetUserByEmail,
    ActivateUser,
    DeactivateUser,
    IsUserAdmin,
    UserLogin,
    //Card Service
    CreateCard,
    UpdateCard,
    DeleteCard,
    GetCardBalance,
    IncreaseCardBalance,
    DecreaseCardBalance,
    CardExists,
    GetCardById,
    GetAllCards,
    //WalletService
    GetWalletBalance,
    IncreaseWalletBalance,
    //ProductService
    CreateProduct,
    UpdateProduct,
    DeleteProduct,
    ProductExists,
    GetProductById,
    GetAllProducts,
    //InvoiceItemService
    CreateInvoiceItem,
    GetInvoiceItems,
    CalculateTotal,
    //InvoiceService
    CreateInvoice,
    UpdateInvoice,
    DeleteInvoice,
    GetInvoicesByUser,
    GetInvoicesById,
    GetAllInvoices,
    //DeliveryAddressService
    CreateDeliveryAddress,
    UpdateDeliveryAddress,
    DeleteDeliveryAddress,
    GetDeliveryAddressById,
    GetAllDeliveryAddresses,
    //CategoryService
    CreateCategory,
    UpdateCategory,
    DeleteCategory,
    GetAllCategories,
    GetCategoryById,
    //BasketService
    AddToBasket,
    UpdateBasketItem,
    RemoveFromBasket,
    GetBasketItems,
    CalculateTotalBasket,
    //DiscountService
    CreateDiscount,
    UpdateDiscount,
    DeleteDiscount,
    GetDiscountById,
    GetAllDiscounts,
    //BrandService
    CreateBrand,
    UpdateBrand,
    DeleteBrand,
    BrandExists,
    GetBrandById,
    GetAllBrands
}
