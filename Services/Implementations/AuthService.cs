using InvoiceApp.Data;
using InvoiceApp.Dtos.Invoice;
using InvoiceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services.Implementations
{
    public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IInvoiceService _invoiceService;

    public AuthService(ApplicationDbContext context, IInvoiceService invoiceService)
    {
        _context = context;
        _invoiceService = invoiceService;
    }

    public async Task<List<InvoiceDto>> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        if (user == null || !string.Equals(user.Password, password, StringComparison.Ordinal))
            throw new UnauthorizedAccessException("Invalid username or password");

        return await _invoiceService.GetInvoicesByUserIdAsync(user.Id);
    }
}
}