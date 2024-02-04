using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IInvoiceService
{
    Task<List<Invoice>> GetInvoicesByUser(int userId);
    Task<Invoice?> GetInvoiceById(int invoiceId);
    Task CreateInvoice(Invoice invoice, List<InvoiceItem> invoiceItems);
    Task UpdateInvoice(Invoice updatedInvoice, List<InvoiceItem> updatedItems);
    Task DeleteInvoice(int invoiceId);
    Task<List<Invoice>> GetAllInvoices();
}