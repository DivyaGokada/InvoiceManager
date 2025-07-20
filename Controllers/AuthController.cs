using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Dtos.Invoice;
using InvoiceApp.DTOs.Auth;

namespace InvoiceApp.Controllers{

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<List<InvoiceDto>>> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var invoices = await _authService.LoginAsync(request.Username, request.Password);
            return Ok(invoices);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}


}
