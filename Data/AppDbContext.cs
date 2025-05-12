using EAPD7111_Agri_Energy_Connect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EAPD7111_Agri_Energy_Connect.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<FarmerModel> Farmers { get; set; }  
        public DbSet<LoginModel> Logins { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FarmerModel>()
                .HasMany(f => f.Products)
                .WithOne(p => p.Farmer)
                .HasForeignKey(p => p.FarmerId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
