namespace InvoiceApp.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public string InvoiceType { get; set; } = "";
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string? AccountHead;
        public string? Description;
        public decimal Amount { get; set; }
        public decimal? NonGSTAmount;
        public decimal? GST;
        public decimal TotalAmount { get; set; }
        public string? PaymentType;
        public string PaymentStatus { get; set; } = "";
        public DateTimeOffset? PaymentDate;
        public bool Preview { get; set; }
        public string? DirectDebit;
        public int SiteId { get; set; }
        public string? InvoiceUrl { get; set; }
    }
}
