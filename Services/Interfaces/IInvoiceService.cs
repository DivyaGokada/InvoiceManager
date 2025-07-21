using InvoiceApp.Dtos.Invoice;

namespace InvoiceApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetInvoicesBySiteIdAsync(int siteId);

    }
}
