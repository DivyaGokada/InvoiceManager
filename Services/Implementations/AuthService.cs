using InvoiceApp.Data;
using InvoiceApp.DTOs;
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

        public async Task<List<UserSiteDto>> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null || !string.Equals(user.Password, password, StringComparison.Ordinal))
                throw new UnauthorizedAccessException("Invalid username or password");

            var userSites = await _context.UserSites
                .Where(us => us.UserId == user.Id)
                .Join(
                    _context.Sites,
                    us => us.SiteId,
                    s => s.Id,
                    (us, s) => new UserSiteDto
                    {
                        SiteId = s.Id,
                        UserId = us.UserId,
                        SiteName = s.Location,
                        RoleInSite = us.RoleInSite
                    }
                )
                .ToListAsync();

            return userSites;
        }
    }
}