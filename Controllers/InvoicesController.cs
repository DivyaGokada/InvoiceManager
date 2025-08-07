using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Data;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Dtos.Invoice;

namespace InvoiceApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(ApplicationDbContext context, IInvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        [HttpGet("{siteId}")]
        public async Task<IActionResult> GetInvoicesBySite(int siteId)
        {
            var invoices = await _invoiceService.GetBySiteIdAsync(siteId);
            if (invoices == null || invoices.Count == 0)
                return NotFound(new { message = $"No invoices found for site ID {siteId}." });

            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDto dto)
        {
            if (!ModelState.IsValid)
            {
                var fields = ModelState.Where(x => x.Value.Errors.Any())
                                       .Select(x => x.Key).ToList();
                return BadRequest(new { message = "Invalid input", missingFields = fields });
            }

            var result = await _invoiceService.CreateAsync(dto);
            return result.isSuccess ? Ok(result.result) : BadRequest(result.result);
        }
        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> Update(int invoiceId, [FromBody] InvoiceDto dto)
        {
            if (!ModelState.IsValid)
            {
                var fields = ModelState.Where(x => x.Value.Errors.Any())
                                       .Select(x => x.Key).ToList();
                return BadRequest(new { message = "Invalid input", errorFields = fields });
            }

            var result = await _invoiceService.UpdateAsync(invoiceId, dto);
            return result.isSuccess ? Ok(result.result) : NotFound(new { message = result.result });
        }

        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> Delete(int invoiceId)
        {
            var result = await _invoiceService.DeleteAsync(invoiceId);
            return result.isSuccess ? Ok(result.result) : NotFound(new { message = result.result });
        }


        [HttpPost("{invoiceId}/upload")]
        public async Task<IActionResult> UploadInvoicePdf(int invoiceId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            // Optional: Validate file type
            var allowedExtensions = new[] { ".pdf" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return BadRequest("Only PDF files are allowed.");

            // Optional: Check if invoice exists
            var invoiceExists = await _context.Invoices.AnyAsync(i => i.InvoiceId == invoiceId);
            if (!invoiceExists)
                return NotFound(new { message = $"Invoice with ID {invoiceId} not found." });

            // Create filename and path
            var newFileName = $"invoice_{invoiceId}{extension}";
            var relativePath = Path.Combine("Uploads", newFileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice != null)
            {
                invoice.PdfUrl = newFileName;
                await _context.SaveChangesAsync();
            }

            return Ok(new { newFileName });
        }


    }
}
