using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.Data_Access;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=ELKHAN-HOME\SQLEXPRESS;Database=ShopProject;Trusted_Connection=true;TrustServerCertificate=True;");
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
        modelBuilder.Entity<Basket>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<InvoiceItem>()
            .HasKey(ii => ii.Id);
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Brand>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Discount>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Card>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Wallet)
            .WithMany(w => w.Cards)
            .HasForeignKey(c => c.WalletId);


        modelBuilder.Entity<User>()
            .HasMany(u => u.DeliveryAddresses)
            .WithOne(da => da.User)
            .HasForeignKey(da => da.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Invoices)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Baskets)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<User>()
             .HasMany(u => u.Cards)
             .WithOne(c => c.User)
             .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.UserName, u.Email })
            .IsUnique();


        modelBuilder.Entity<Card>()
            .HasIndex(c => new { c.CardNumber, c.Cvc })
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Baskets)
            .WithOne(b => b.Product)
            .HasForeignKey(b => b.ProductId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.InvoiceItems)
            .WithOne(ii => ii.Product)
            .HasForeignKey(ii => ii.ProductId);

        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId);
        
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Cards)
            .WithOne(c => c.Invoice)
            .HasForeignKey(c => c.InvoiceId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Brand>()
            .HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId);

        modelBuilder.Entity<Discount>()
            .HasMany(d => d.Products)
            .WithOne(p => p.Discount)
            .HasForeignKey(p => p.DiscountId);
        modelBuilder.Entity<TransactionLog>()
            .HasOne(t => t.User)
            .WithMany(u => u.TransactionLogs)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<TransactionLog>()
            .HasOne(t => t.Card)
            .WithMany(c => c.TransactionLogs)
            .HasForeignKey(t => t.CardId);
    }
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<Basket> Baskets { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<TransactionLog>? TransactionLogs { get; set; }

}
