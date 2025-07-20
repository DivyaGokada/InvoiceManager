using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<UserStore> UserStores => Set<UserStore>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserStore>().HasKey(us => new { us.UserId, us.StoreId });

        modelBuilder.Entity<UserStore>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserStores)
            .HasForeignKey(us => us.UserId);

        modelBuilder.Entity<UserStore>()
            .HasOne(us => us.Store)
            .WithMany(s => s.UserStores)
            .HasForeignKey(us => us.StoreId);
    }
}

