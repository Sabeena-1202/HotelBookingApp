using HotelAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAuthService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasDefaultValue("User");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(h => h.HotelId);
                entity.Property(h => h.Rating)
                      .HasPrecision(3, 1);
                entity.HasMany(h => h.Rooms)
                      .WithOne(r => r.Hotel)
                      .HasForeignKey(r => r.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.RoomId);
                entity.Property(r => r.PricePerNight)
                      .HasPrecision(10, 2);
                entity.Property(r => r.IsAvailable)
                      .HasDefaultValue(true);
            });
        }
    }
}