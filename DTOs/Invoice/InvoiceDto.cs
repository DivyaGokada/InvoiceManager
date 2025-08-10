namespace InvoiceApp.Dtos.Invoice
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string? AccountHead;
        public string? Description;
        public decimal Amount { get; set; }
        public decimal? GST;
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public bool Preview { get; set; }
        public string? PaymentType;       // "Cash" or "Card"
        public string PaymentStatus { get; set; } = "";    
        public string InvoiceType { get; set; } = "";     // "Original" or "Duplicate"
        public decimal? NonGSTAmount;
        public string? DirectDebit;
        public int SiteId { get; set; }
        public string? InvoiceUrl { get; set; }
    }
}
