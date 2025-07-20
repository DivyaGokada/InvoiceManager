using InvoiceApp.Data;
using InvoiceApp.Dtos.Invoice;
using InvoiceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDto>> GetInvoicesByUserIdAsync(int userId, int? storeId = null)
        {
            var userStores = await _context.UserStores
                .Where(us => us.UserId == userId)
                .ToListAsync();

            var ownedStoreId = userStores
                .FirstOrDefault(us => us.RoleInStore == "Owner")?.StoreId;

            if (storeId == null)
            {
                storeId = ownedStoreId;
            }

            if (storeId == 0)
            {
                return await _context.Invoices
                    .Select(i => new InvoiceDto
                    {
                        InvoiceId = i.Id,
                        InvoiceDate = i.InvoiceDate,
                        InvoiceNumber = i.InvoiceNumber,
                        DueDate = i.DueDate,
                        SupplierName = i.SupplierName,
                        AccountHead = i.AccountHead,
                        Description = i.Description,
                        Amount = i.Amount,
                        GST = i.GST,
                        TotalAmount = i.TotalAmount,
                        PaymentDate = i.PaymentDate,
                        Preview = i.Preview
                    }).ToListAsync();
            }

            if (!userStores.Any(us => us.StoreId == storeId))
            {
                throw new UnauthorizedAccessException("Access denied to the specified store.");
            }

            return await _context.Invoices
                .Where(i => i.StoreId == storeId)
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.Id,
                    InvoiceDate = i.InvoiceDate,
                    InvoiceNumber = i.InvoiceNumber,
                    DueDate = i.DueDate,
                    SupplierName = i.SupplierName,
                    AccountHead = i.AccountHead,
                    Description = i.Description,
                    Amount = i.Amount,
                    GST = i.GST,
                    TotalAmount = i.TotalAmount,
                    PaymentDate = i.PaymentDate,
                    Preview = i.Preview
                }).ToListAsync();
        }
    }
}