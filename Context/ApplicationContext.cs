using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>(stock =>
            {
                stock.ToTable("Stock");
                stock.HasKey(s => s.Id);
                stock.HasMany(s => s.Comments).WithOne(c => c.Stock).HasForeignKey(c => c.StockId);
                stock.Property(s => s.Symbol);
                stock.Property(s => s.CompanyName);
                stock.Property(s => s.Purchase).HasColumnType("decimal(18,2)");
                stock.Property(s => s.LastDiv).HasColumnType("decimal(18,2)");
                stock.Property(s => s.Industry);
                stock.Property(s => s.MarketCap);
            });

            modelBuilder.Entity<Comment>(comment =>
            {
                comment.ToTable("Comment");
                comment.HasKey(c => c.Id);
                comment.Property(c => c.Title);
                comment.Property(c => c.Content);
                comment.Property(c => c.CreatedOn);
            });

            modelBuilder.Entity<Portfolio>(portfolio =>
            {
                portfolio.ToTable("Portfolio");
                portfolio.HasKey(p => new { p.StockId, p.AppUserId });
                portfolio.HasOne(p => p.AppUser).WithMany(u => u.Portfolios).HasForeignKey(p => p.AppUserId);
                portfolio.HasOne(p => p.Stock).WithMany(s => s.Portfolios).HasForeignKey(p => p.StockId);
            });

            modelBuilder.Entity<IdentityUserLogin<string >>(login =>
            {
                login.HasAlternateKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            List<IdentityRole> roles =
            [
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            ];

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}