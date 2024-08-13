using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure
{
    public class ApiDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UrlInfo> UrlInfos => Set<UrlInfo>();
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<UrlInfo>()
                .HasIndex(ui => ui.OriginalUrl)
                .IsUnique();

            modelBuilder.Entity<UrlInfo>()
                .HasIndex(ui => ui.Code)
                .IsUnique();

            modelBuilder.Entity<UrlInfo>()
                .HasOne(ui => ui.CreatedBy)
                .WithMany(u => u.UrlInfos)
                .HasForeignKey(ui => ui.IdCreatedBy);

            base.OnModelCreating(modelBuilder);
        }
    }
}
