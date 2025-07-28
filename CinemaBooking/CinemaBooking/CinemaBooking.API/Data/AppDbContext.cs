using Microsoft.EntityFrameworkCore;
using CinemaBooking.API.Models;

namespace CinemaBooking.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Screening> Screenings => Set<Screening>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<ReservationSeat> ReservationSeats => Set<ReservationSeat>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Seat>().HasData(GenerateSeats());
        }

        private List<Seat> GenerateSeats()
        {
            var seats = new List<Seat>();
            int id = 1;
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 10; col++)
                {
                    seats.Add(new Seat { Id = id++, Row = row, Column = col });
                }
            }
            return seats;
        }
    }
}