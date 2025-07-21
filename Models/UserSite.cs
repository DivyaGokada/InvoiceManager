namespace InvoiceApp.Models
{
    public class UserSite
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SiteId { get; set; }
        public Site Site { get; set; } = null!;
        public string RoleInSite { get; set; } = ""; // "Owner" or "Partner"
    }
}
