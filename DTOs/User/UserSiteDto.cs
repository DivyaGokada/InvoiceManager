namespace InvoiceApp.Dtos.UserSite
{
    public class UserSiteDto
    {
        public int UserId { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; } = "";
        public string RoleInSite { get; set; } = ""; // "Owner" or "Partner"
    }
}