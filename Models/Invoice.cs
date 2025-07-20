namespace InvoiceApp.Models
{

    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public string InvoiceType { get; set; } = "";
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string SupplierName { get; set; } = "";
        public string AccountHead { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal NonGSTAmount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public string DebitType { get; set; } = "";
        public DateTime PaymentDate { get; set; }
        public bool Preview { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;
    }
}
