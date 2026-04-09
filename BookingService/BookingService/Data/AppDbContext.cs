using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status)
                      .HasDefaultValue("Pending");

                entity.Property(e => e.TotalPrice)
                      .HasColumnType("decimal(10,2)");

                // Remove CURRENT_TIMESTAMP — let C# set it instead
                entity.Property(e => e.CreatedAt)
                      .ValueGeneratedNever();
            });
        }
    }
}