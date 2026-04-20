using Microsoft.EntityFrameworkCore;
using ResellerApp.Api.Entities;

namespace ResellerApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Images)
                .WithOne(img => img.Item)
                .HasForeignKey(img => img.ItemId);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Listings)
                .WithOne(l => l.Item)
                .HasForeignKey(l => l.ItemId);

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Cost).HasPrecision(18, 2);
                entity.Property(e => e.DesiredProfit).HasPrecision(18, 2);

                entity.Property(e => e.Length).HasPrecision(10, 2);
                entity.Property(e => e.Width).HasPrecision(10, 2);
                entity.Property(e => e.Height).HasPrecision(10, 2);

                entity.Property(e => e.Weight).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Listing>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });
        }
    }
}
