using InvoiceApp.Data;
using InvoiceApp.Dtos.Invoice;
using InvoiceApp.Models;
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

        public async Task<List<InvoiceDto>> GetBySiteIdAsync(int siteId)
        {
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
                    PaymentStatus = i.PaymentStatus,
                    DirectDebit = i.DirectDebit,
                    Preview = i.Preview,
                    SiteId = i.SiteId,
                    PdfUrl = i.PdfUrl
                }).ToListAsync();
        }
        public async Task<(bool isSuccess, object result)> CreateAsync(InvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceDate = dto.InvoiceDate,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceType = dto.InvoiceType,
                DueDate = dto.DueDate,
                SupplierName = dto.SupplierName,
                AccountHead = dto.AccountHead,
                Description = dto.Description,
                Amount = dto.Amount,
                NonGSTAmount = dto.NonGSTAmount,
                GST = dto.GST,
                TotalAmount = dto.TotalAmount,
                PaymentDate = dto.PaymentDate,
                PaymentType = dto.PaymentType,
                PaymentStatus = dto.PaymentStatus,
                DirectDebit = dto.DirectDebit,
                Preview = dto.Preview,
                SiteId = dto.SiteId,
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            var updatedList = await GetBySiteIdAsync(dto.SiteId);
            return (true, updatedList);
        }

        public async Task<(bool isSuccess, object result)> UpdateAsync(int id, InvoiceDto dto)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return (false, $"Invoice ID {id} not found.");

            invoice.InvoiceDate = dto.InvoiceDate;
            invoice.InvoiceNumber = dto.InvoiceNumber;
            invoice.InvoiceType = dto.InvoiceType;
            invoice.DueDate = dto.DueDate;
            invoice.SupplierName = dto.SupplierName;
            invoice.AccountHead = dto.AccountHead;
            invoice.Description = dto.Description;
            invoice.Amount = dto.Amount;
            invoice.NonGSTAmount = dto.NonGSTAmount;
            invoice.GST = dto.GST;
            invoice.TotalAmount = dto.TotalAmount;
            invoice.PaymentDate = dto.PaymentDate;
            invoice.PaymentType = dto.PaymentType;
            invoice.DirectDebit = dto.DirectDebit;
            invoice.Preview = dto.Preview;
            invoice.SiteId = dto.SiteId;

            await _context.SaveChangesAsync();
            var updatedList = await GetBySiteIdAsync(dto.SiteId);
            return (true, updatedList);
        }

        public async Task<(bool isSuccess, object result)> DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return (false, $"Invoice ID {id} not found.");

            int siteId = invoice.SiteId;
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            var updatedList = await GetBySiteIdAsync(siteId);
            return (true, updatedList);
        }
    }
}