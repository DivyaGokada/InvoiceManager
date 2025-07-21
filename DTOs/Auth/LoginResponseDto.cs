using InvoiceApp.Dtos.Invoice;

namespace InvoiceApp.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string UserRole { get; set; } = "";
        public List<SiteInvoicesDto> Stores { get; set; } = new();
    }
}
