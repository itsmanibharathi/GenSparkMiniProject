using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class DBGenSparkMinirojectContext : DbContext
    {
        public DBGenSparkMinirojectContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAuth> CustomerAuths { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantAuth> RestaurantAuths { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Table Key 
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<CustomerAuth>()
                .HasKey(ca => ca.CustomerId);
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => new { ca.CustomerId, ca.AddressId });
            modelBuilder.Entity<Restaurant>()
                .HasKey(r => r.RestaurantId);
            modelBuilder.Entity<RestaurantAuth>()
                .HasKey(ra => ra.RestaurantId);
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<ProductImage>()
                .HasKey(pi => pi.ProductImageId);

            // Table Index
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.RestaurantId);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);
           
            // Table Relation 
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerAuth)
                .WithOne(ca => ca.Customer)
                .HasForeignKey<CustomerAuth>(ca => ca.CustomerId);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses)
                .WithOne(ca => ca.Customer)
                .HasForeignKey(ca => ca.CustomerId);

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.RestaurantAuth)
                .WithOne(ra => ra.Restaurant)
                .HasForeignKey<RestaurantAuth>(ra => ra.RestaurantId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Products)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestaurantId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            // Property
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");
        }   


    }
}
