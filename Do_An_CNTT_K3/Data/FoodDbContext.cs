using Do_An_CNTT_K3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Do_An_CNTT_K3.Data
{
    public class FoodDbContext: IdentityDbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {

        }
        //Lien Lac Den Web
        public DbSet<Contact> contacts { get; set; }
        public DbSet<InformationSP> informationSPs { get; set;}
        public DbSet<BaiViet> baiViets { get; set; }
        public DbSet<DanhGia> danhGias { get; set; }
        public DbSet<ShoppingSP> shoppingSPs { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhGia>()
            .HasOne(d => d.InformationSP)
            .WithMany(i => i.DanhGias)
            .HasForeignKey(d => d.InformationSPIdSP);
            modelBuilder.Entity<BaiViet>(entity =>
            {
                entity.HasKey(e => e.IdBV);
            });
            modelBuilder.Entity<InformationSP>(entity =>
            {
                entity.HasKey(e => e.IdSP);
            });
            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasKey(e => e.IdDanhGia);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
