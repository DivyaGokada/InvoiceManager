using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOs;
using InvoiceApp.Models;
using InvoiceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly string _invoiceFolder;

        public InvoiceService(ApplicationDbContext context, IConfiguration config, IMapper mapper)
        {
             _mapper = mapper;
            _context = context;
            _invoiceFolder = config["FileUpload:InvoiceFolder"] 
                     ?? throw new ArgumentNullException("FileUpload:InvoiceFolder config missing");
        }

        public async Task<List<InvoiceDto>> GetBySiteIdAsync(int siteId)
        {
            var invoices = await _context.Invoices
                .Where(i => i.SiteId == siteId)
                .OrderByDescending(i => i.DueDate).ToListAsync();

            return _mapper.Map<List<InvoiceDto>>(invoices);

        }
        public async Task<(bool isSuccess, object result)> CreateAsync(InvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
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
            _mapper.Map(dto, invoice);
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

        public async Task<(bool isSuccess, object result)> UploadFileAsync(IFormFile file, int invoiceId)
        {
            if (file == null || file.Length == 0)
                return (false, "File is empty.");

            // Optional: Validate file type
            var allowedExtensions = new[] { ".pdf" , ".jpeg", ".jpg"};
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return (false, "Only PDF files are allowed.");

            // Fetch site name based on invoiceId
            var siteName = await _context.Invoices
                .Where(i => i.InvoiceId == invoiceId)
                .Join(
                    _context.Sites,
                    i => i.SiteId,
                    s => s.Id,
                    (us, s) => s.Location
                )
                .FirstOrDefaultAsync();
                
            if (siteName == null)
            {
                siteName = "";
            }

            // Create filename and path
            var newFileName = $"invoice_{invoiceId}{extension}";
            var relativePath = Path.Combine(siteName, newFileName);
            var fullPath = Path.Combine( _invoiceFolder, relativePath);

            // Ensure directory exists and delete old files if necessary
            var fileBaseName = Path.GetFileNameWithoutExtension(fullPath);
            var directory = Path.GetDirectoryName(fullPath);
            foreach (var ext in allowedExtensions)
            {
                var oldFilePath = Path.Combine(directory!, fileBaseName + ext);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            
            Directory.CreateDirectory(directory!);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice != null)
            {
                invoice.InvoiceUrl = relativePath;
                invoice.Preview = true;
                await _context.SaveChangesAsync();
            }

            return (true, new { newFileName });
        }
    }
}