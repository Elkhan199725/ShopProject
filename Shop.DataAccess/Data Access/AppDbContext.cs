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

        modelBuilder.Entity<User>()
            .HasMany(u => u.DeliveryAddresses)
            .WithOne(da => da.User)
            .HasForeignKey(da => da.UserId);

        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.UserName, u.Email })
            .IsUnique();
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;

}
