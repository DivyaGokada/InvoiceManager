using InvoiceApp.Dtos.Invoice;

namespace InvoiceApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<List<InvoiceDto>> LoginAsync(string username, string password);
    }
}
