using InvoiceApp.DTOs;
using InvoiceApp.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetBySiteIdAsync(int siteId);
        Task<(bool isSuccess, object result)> CreateAsync(InvoiceDto invoiceDto);
        Task<(bool isSuccess, object result)> UpdateAsync(int invoiceId, InvoiceDto invoiceDto);
        Task<(bool isSuccess, object result)> DeleteAsync(int invoiceId);
        Task<(bool isSuccess, object result)> UploadFileAsync(IFormFile file, int invoiceId);

    }
}
