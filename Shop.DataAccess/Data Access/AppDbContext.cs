using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.Data_Access;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=TITAN03\SQLEXPRESS;Database=ShopProject;Trusted_Connection=true;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<DeliveryAddress>()
            .HasKey(da => da.Id);
        modelBuilder.Entity<Wallet>()
            .HasKey(w => w.Id);        
        modelBuilder.Entity<Invoice>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<User>()
            .HasMany(u => u.DeliveryAddresses)
            .WithOne(da => da.User)
            .HasForeignKey(da => da.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.UserName, u.Email })
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Invoices)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<Wallet>()
            .HasMany(w=>w.Invoices)
            .WithOne(i => i.Wallet)
            .HasForeignKey(i => i.WalletId);

        modelBuilder.Entity<Wallet>()
            .HasIndex(w => w.CardNumber)
            .IsUnique();
    }
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;

}
