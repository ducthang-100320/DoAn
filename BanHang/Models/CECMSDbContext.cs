namespace BanHang.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CECMSDbContext : DbContext
    {
        public CECMSDbContext()
            : base("name=CECMSDbContext4")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Header> Headers { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            

            modelBuilder.Entity<ProductPrice>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductPrice>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Voucher>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.ConditionPrice)
                .HasPrecision(18, 0);
        }
    }
}
