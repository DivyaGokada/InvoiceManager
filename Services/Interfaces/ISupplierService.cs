using InvoiceApp.DTOs;

namespace InvoiceApp.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetBySiteIdAsync(int siteId);
        Task<(bool isSuccess, object result)> CreateAsync(int siteId, SupplierDto supplierDto);
    }
}
