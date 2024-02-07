using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IInvoiceService
{

    Task<bool> CreateInvoice(List<int> invoiceItemIds, int cardId, int userId);

}