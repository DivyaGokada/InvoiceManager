namespace InvoiceApp.Dtos.Invoice
{
    public class StoreInvoicesDto
    {
        public string StoreLocation { get; set; } = "";        // e.g., "Sydney", "Melbourne"
        public string RoleInStore { get; set; } = "";          // "Owner" or "Partner"
        public List<InvoiceDto> Invoices { get; set; } = new(); // All invoices for the store
    }
}
