using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class DBGenSparkMinirojectContext : DbContext
    {
        public DBGenSparkMinirojectContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAuth> CustomerAuths { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantAuth> RestaurantAuths { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAuth> EmployeeAuths { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OnlinePayment> Payments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer Entity Configuration
            #region Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            #region Indexes
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CustomerEmail);
            #endregion

            #region Relations
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerAuth)
                .WithOne(ca => ca.Customer)
                .HasForeignKey<CustomerAuth>(ca => ca.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses)
                .WithOne(ca => ca.Customer)
                .HasForeignKey(ca => ca.CustomerId);
            #endregion

            #region Properties
            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .IsRequired();
            #endregion
            #endregion

            // CustomerAuth Entity Configuration
            #region CustomerAuth
            modelBuilder.Entity<CustomerAuth>()
                .HasKey(ca => ca.CustomerId);
            #endregion

            // CustomerAddress Entity Configuration
            #region CustomerAddress
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => ca.AddressId);
            #endregion

            // Restaurant Entity Configuration
            #region Restaurant
            modelBuilder.Entity<Restaurant>()
                .HasKey(r => r.RestaurantId);

            #region Indexes
            modelBuilder.Entity<Restaurant>()
                .HasIndex(r => r.Email);
            #endregion

            #region Relations
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.RestaurantAuth)
                .WithOne(ra => ra.Restaurant)
                .HasForeignKey<RestaurantAuth>(ra => ra.RestaurantId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Products)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestaurantId);
            #endregion
            #endregion

            // RestaurantAuth Entity Configuration
            #region RestaurantAuth
            modelBuilder.Entity<RestaurantAuth>()
                .HasKey(ra => ra.RestaurantId);
            #endregion

            // Product Entity Configuration
            #region Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            #region Indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.RestaurantId);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);
            #endregion

            #region Relations
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);
            #endregion

            #region Properties
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

            // ProductImage Entity Configuration
            #region ProductImage
            modelBuilder.Entity<ProductImage>()
                .HasKey(pi => pi.ProductImageId);
            #endregion

            // Employee Entity Configuration
            #region Employee
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            #region Indexes
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeEmail);
            #endregion

            #region Relations
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeAuth)
                .WithOne(ea => ea.Employee)
                .HasForeignKey<EmployeeAuth>(ea => ea.EmployeeId);
            #endregion

            #region Properties
            modelBuilder.Entity<Employee>()
                .Property(e => e.Balance)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

            // EmployeeAuth Entity Configuration
            #region EmployeeAuth
            modelBuilder.Entity<EmployeeAuth>()
                .HasKey(ea => ea.EmployeeId);
            #endregion

            // Order Entity Configuration
            #region Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            #region Relations
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
            #endregion

            #region Properties
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalOrderPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.TaxRat)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.DiscountRat)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

            // OrderItem Entity Configuration
            #region OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);

            #region Properties
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

            // OnlinePayment Entity Configuration
            #region OnlinePayment
            modelBuilder.Entity<OnlinePayment>()
                .HasKey(p => p.OnlinePaymentId);

            #region Relations
            modelBuilder.Entity<OnlinePayment>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.OnlinePayment)
                .HasForeignKey(o => o.OnlinePaymentId);
            #endregion

            #region Properties
            modelBuilder.Entity<OnlinePayment>()
                .Property(p => p.PayAmount)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

            // CashPayment Entity Configuration
            #region CashPayment
            modelBuilder.Entity<CashPayment>()
                .HasKey(p => p.CashPaymentId);

            #region Relations
            modelBuilder.Entity<CashPayment>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.CashPayment)
                .HasForeignKey(o => o.CashPaymentId);
            #endregion

            #region Properties
            modelBuilder.Entity<CashPayment>()
                .Property(p => p.PayAmount)
                .HasColumnType("decimal(18,2)");
            #endregion
            #endregion

        }
    }
}
