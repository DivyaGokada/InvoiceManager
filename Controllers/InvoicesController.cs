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
    }
}
