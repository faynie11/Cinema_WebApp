namespace CinemaBooking.API.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }

        public Movie? Movie { get; set; }
        public List<Reservation> Reservations { get; set; } = new();

        public DateTime EndTime => StartTime.AddMinutes(Movie?.DurationMinutes ?? 0);
    }
}
