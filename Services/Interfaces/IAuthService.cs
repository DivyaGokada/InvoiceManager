using InvoiceApp.DTOs;

namespace InvoiceApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<List<UserSiteDto>> LoginAsync(string username, string password);
    }
}
