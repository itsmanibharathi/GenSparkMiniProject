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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAuth> EmployeeAuths { get; set; }
        

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Table Key 
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<CustomerAuth>()
                .HasKey(ca => ca.CustomerId);
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca =>ca.AddressId );
            modelBuilder.Entity<Restaurant>()
                .HasKey(r => r.RestaurantId);
            modelBuilder.Entity<RestaurantAuth>()
                .HasKey(ra => ra.RestaurantId);
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<ProductImage>()
                .HasKey(pi => pi.ProductImageId);
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeAuth>()
                .HasKey(ea => ea.EmployeeId);
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            // Table Index
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CustomerEmail);
            modelBuilder.Entity<Restaurant>()
                .HasIndex(r => r.Email);
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeEmail);
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

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeAuth)
                .WithOne(ea => ea.Employee)
                .HasForeignKey<EmployeeAuth>(ea => ea.EmployeeId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);
            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
                
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
                
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CustomerAddress)
                .WithMany(ca => ca.Orders)
                .HasForeignKey(o => o.CustomerAddressId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Property
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotal)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Employee>()
                .Property(e => e.Balance)
                .HasColumnType("decimal(18,2)");
        }
    }
}
