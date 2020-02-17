namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTrip>().HasKey(pm => new { pm.UserId, pm.TripId });

            modelBuilder.Entity<UserTrip>()
                .HasOne(u => u.User)
                .WithMany(t => t.UserTrips)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserTrip>()
                .HasOne(t => t.Trip)
                .WithMany(u => u.UserTrips)
                .HasForeignKey(t => t.TripId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
    }
}
