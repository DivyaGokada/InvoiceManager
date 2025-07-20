namespace InvoiceApp.Models
{
    public class UserStore
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;
        public string RoleInStore { get; set; } = ""; // "Owner" or "Partner"
    }
}
