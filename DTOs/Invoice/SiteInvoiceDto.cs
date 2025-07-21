namespace InvoiceApp.Dtos.Invoice
{
    public class SiteInvoicesDto
    {
        public string SiteLocation { get; set; } = "";        // e.g., "Sydney", "Melbourne"
        public string RoleInSite { get; set; } = "";          // "Owner" or "Partner"
        public List<InvoiceDto> Invoices { get; set; } = new(); // All invoices for the site
    }
}
