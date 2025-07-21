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

        public async Task<List<InvoiceDto>> GetInvoicesBySiteIdAsync(int siteId)
        {
            var sites = _context.Sites;

            if (!sites.Any(us => us.Id == siteId))
            {
                throw new Exception("Invalid Site Name.");
            }

            return await _context.Invoices
                .Where(i => i.SiteId == siteId)
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceDate = i.InvoiceDate,
                    InvoiceNumber = i.InvoiceNumber,
                    InvoiceType = i.InvoiceType,
                    DueDate = i.DueDate,
                    SupplierName = i.SupplierName,
                    AccountHead = i.AccountHead,
                    Description = i.Description,
                    Amount = i.Amount,
                    NonGSTAmount = i.NonGSTAmount,
                    GST = i.GST,
                    TotalAmount = i.TotalAmount,
                    PaymentDate = i.PaymentDate,
                    PaymentType = i.PaymentType,
                    DirectDebit = i.DirectDebit,
                    Preview = i.Preview,
                    SiteId = i.SiteId,
                }).ToListAsync();
        }
    }
}