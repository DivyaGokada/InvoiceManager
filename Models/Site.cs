namespace InvoiceApp.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Location { get; set; } = "";
        public ICollection<UserSite> UserSites { get; set; } = new List<UserSite>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }

}