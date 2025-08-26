using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.DTOs;

namespace InvoiceApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("{siteId}")]
        public async Task<IActionResult> GetSupplierssBySite(int siteId)
        {
            var suppliers = await _supplierService.GetBySiteIdAsync(siteId);
            if (suppliers == null || suppliers.Count == 0)
                return NotFound(new { message = $"No suppliers found for site ID {siteId}." });

            return Ok(suppliers);
        }

        [HttpPost("{siteId}")]
        public async Task<IActionResult> Create(int siteId, [FromBody] SupplierDto dto)
        {
            if (!ModelState.IsValid)
            {
                var fields = ModelState.Where(x => x.Value != null && x.Value.Errors.Any())
                                       .Select(x => x.Key).ToList();
                return BadRequest(new { message = "Invalid input", missingFields = fields });
            }

            var createRes = await _supplierService.CreateAsync(siteId, dto);
            return createRes.isSuccess ? Ok(createRes.result) : BadRequest(createRes.result);
        }
    }
}
