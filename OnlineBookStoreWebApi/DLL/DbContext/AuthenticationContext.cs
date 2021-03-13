using DLL.Models;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLL.Models.Interface;

namespace DLL.DbContext
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options):base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one relationship
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoices>() //foreign key table
                .HasOne(a => a.Payments) //which table to insert foreign key
                .WithOne(b => b.Invoices) //one relationship
                .HasForeignKey<Payments>(c => c.InvoiceId); //foreign key
            modelBuilder.Entity<RefInvoiceStatusCode>()
                .HasOne(a => a.Invoices)
                .WithOne(b => b.RefInvoiceStatusCode)
                .HasForeignKey<Invoices>(c => c.InvoiceStatusCode);
            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Shipment)
                .WithOne(b => b.Invoices)
                .HasForeignKey<Shipment>(c => c.InvoiceId);
            modelBuilder.Entity<RefOrderStatusCodes>()
                .HasOne(a => a.Orders)
                .WithOne(b => b.RefOrderStatusCodes)
                .HasForeignKey<Orders>(c => c.OrderStatusCode);
            modelBuilder.Entity<Orders>()
                .HasOne(a => a.Shipment)
                .WithOne(b => b.Orders)
                .HasForeignKey<Shipment>(c => c.OrderId);
            modelBuilder.Entity<Product>()
                .HasOne(a => a.ProductDescriptions)
                .WithOne(b => b.Products)
                .HasForeignKey<ProductDescription>(c => c.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(a => a.ProductInStock)
                .WithOne(b => b.Product)
                .HasForeignKey<ProductInStock>(c => c.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(a => a.ProductReview)
                .WithOne(b => b.Product)
                .HasForeignKey<ProductReview>(c => c.ProductId);
            modelBuilder.Entity<Orders>()
                .HasOne(a => a.Invoices)
                .WithOne(b => b.Orders)
                .HasForeignKey<Invoices>(c => c.OrderId);


            //auto generated identity property to none
            modelBuilder.Entity<RefOrderStatusCodes>()
                .Property(o => o.OrderStatusCode)
                .ValueGeneratedNever();
            modelBuilder.Entity<RefOrderItemStatusCodes>()
                .Property(o => o.OrderItemStatusCode)
                .ValueGeneratedNever();
            modelBuilder.Entity<RefInvoiceStatusCode>()
                .Property(i => i.InvoiceStatusCode)
                .ValueGeneratedNever();

            //specify decimal type
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(8,3)");
            modelBuilder.Entity<OrderItems>()
                .Property(p => p.OrderItemPrice)
                .HasColumnType("decimal(8,3)");
            modelBuilder.Entity<OrderItems>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(8,3)");
            modelBuilder.Entity<Payments>()
                .Property(p => p.PaymentAmount)
                .HasColumnType("decimal(8,3)");

            //one to many relationship
            modelBuilder.Entity<OrderItems>() //foreign key table
                .HasOne<RefOrderItemStatusCodes>(o => o.RefOrderItemStatusCodes) //One relationship
                .WithMany(r => r.OrderItems) //many relationship
                .HasForeignKey(o => o.OrderItemStatusCode); // foreign key
            modelBuilder.Entity<OrderItems>()
                .HasOne<Orders>(i => i.Orders)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.OrderId);
            modelBuilder.Entity<OrderItems>()
                .HasOne<Product>(i => i.Products)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne<ProductCategory>(c => c.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.ProductCategroyId);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSavingData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSavingData()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged);

            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedAt = DateTimeOffset.Now;
                            trackable.LastUpdateAt = DateTimeOffset.Now;
                            break;
                        case EntityState.Modified:
                            trackable.LastUpdateAt = DateTimeOffset.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSavingData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<PaymentDetailsUser> PaymentDetailsUser { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductInStock> ProductInStocks { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<RefInvoiceStatusCode> RefInvoiceStatusCodes { get; set; }
        public DbSet<RefOrderItemStatusCodes> RefOrderItemStatusCodes { get; set; }
        public DbSet<RefOrderStatusCodes> RefOrderStatusCodes { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

    }
}
