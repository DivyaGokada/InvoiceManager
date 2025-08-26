using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Site> Sites => Set<Site>();
    public DbSet<UserSite> UserSites => Set<UserSite>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSite>().HasKey(us => new { us.UserId, us.SiteId });

        modelBuilder.Entity<UserSite>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserSites)
            .HasForeignKey(us => us.UserId);

        modelBuilder.Entity<UserSite>()
            .HasOne(us => us.Site)
            .WithMany(s => s.UserSites)
            .HasForeignKey(us => us.SiteId);
    }
}

