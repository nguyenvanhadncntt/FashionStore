using FashionStoreWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FashionStoreWebApi.Data
{
    public class FashionStoreDbContext : IdentityDbContext<User, Role, string, 
        IdentityUserClaim<string>, IdentityUserRole<string>, 
        IdentityUserLogin<string>, IdentityRoleClaim<string>, 
        IdentityUserToken<string>>
    {

        public FashionStoreDbContext(DbContextOptions<FashionStoreDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            configurateIdAutoIncrement(modelBuilder);

            // 1. User - Order (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // 2. User - Cart (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CartItems)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete cart items when user is deleted


            // 3. Product - Category (Many-to-One)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Product - Brand (Many-to-One)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Product - OrderItem (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // 6. Product - Cart (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // 7. Order - OrderItem (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete order items when order is deleted

            // 8. Cart - User (Many-to-One)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 9. Cart - Product (Many-to-One)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        private static void configurateIdAutoIncrement(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Cart>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderItem>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
