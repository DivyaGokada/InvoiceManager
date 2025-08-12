namespace InvoiceApp.Dtos.Invoice
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string? AccountHead { get; set; } = "";
        public string? Description { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal? GST { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public bool Preview { get; set; }
        public string? PaymentType { get; set; } = "";      // "Cash" or "Card"
        public string PaymentStatus { get; set; } = "";    
        public string InvoiceType { get; set; } = "";     // "Original" or "Duplicate"
        public decimal? NonGSTAmount { get; set; }
        public string? DirectDebit { get; set; } = "";
        public int SiteId { get; set; }
        public string? InvoiceUrl { get; set; }
    }
}
