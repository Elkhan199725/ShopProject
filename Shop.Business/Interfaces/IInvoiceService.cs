using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IInvoiceService
{
    Task<List<Invoice>> GetInvoicesByUser(int userId);
    Task<Invoice?> GetInvoiceById(int invoiceId);
    Task CreateInvoice(Invoice invoice);
    Task UpdateInvoice(Invoice updatedInvoice);
    Task DeleteInvoice(int invoiceId);
}
