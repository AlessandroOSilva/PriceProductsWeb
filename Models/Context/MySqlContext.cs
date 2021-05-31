using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;

namespace PricesComparationWeb.Models.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }
        public DbSet<PriceRecord> PriceRecords { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(p => p.Products)
                .WithOne(b => b.Brand);

            modelBuilder.Entity<ProductShop>()
                .HasOne(b => b.Product)
                .WithMany(p => p.Products)
                .HasForeignKey("ProductId")
                ;

            modelBuilder.Entity<ProductShop>()
                .HasOne(s => s.Shop)
                .WithMany(p => p.Products)
                .HasForeignKey("ShopId");

            modelBuilder.Entity<PriceRecord>()
                .HasOne(p => p.ProductShop)
                .WithMany(p => p.Records)
                .HasForeignKey("ProductShopId");
        }
    }
}
