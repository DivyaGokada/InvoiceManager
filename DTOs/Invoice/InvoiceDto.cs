namespace InvoiceApp.Dtos.Invoice
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string AccountHead { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Preview { get; set; }

        public string DebitType { get; set; } = "";       // "Cash" or "Card"
        public string InvoiceType { get; set; } = "";     // "Original" or "Duplicate"
        public decimal NonGSTAmount { get; set; }
    }
}
