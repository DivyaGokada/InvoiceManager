namespace InvoiceApp.Models
{
    public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string UserRole { get; set; } = ""; // Owner / Partner
    public ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}
}

