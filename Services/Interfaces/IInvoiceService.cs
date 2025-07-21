using InvoiceApp.Dtos.Invoice;
using InvoiceApp.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetBySiteIdAsync(int siteId);
        Task<(bool isSuccess, object result)> CreateAsync(InvoiceDto invoiceDto);
        Task<(bool isSuccess, object result)> UpdateAsync(int invoiceId, InvoiceDto invoiceDto);
        Task<(bool isSuccess, object result)> DeleteAsync(int invoiceId);

    }
}
