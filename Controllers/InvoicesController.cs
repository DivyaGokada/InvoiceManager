using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Data;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;
using InvoiceApp.Services.Interfaces;

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetInvoicesByUser(int userId, [FromQuery] int? storeId)
        {
            try
            {
                var result = await _invoiceService.GetInvoicesByUserIdAsync(userId, storeId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // 403
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddInvoice(int userId, [FromBody] Invoice invoice)
        {
            var user = await _context.Users
                .Include(u => u.UserStores)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var isOwner = user?.UserStores.Any(us => us.StoreId == invoice.StoreId && us.RoleInStore == "Owner") ?? false;

            if (!isOwner)
                return Forbid("Only owners can add invoices to their store.");

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return Ok(invoice);
        }
    }
}
