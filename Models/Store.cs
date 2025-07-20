namespace InvoiceApp.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Location { get; set; } = "";
        public ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }

}