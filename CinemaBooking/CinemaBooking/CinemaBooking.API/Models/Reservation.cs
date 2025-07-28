namespace CinemaBooking.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ScreeningId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
        public Screening? Screening { get; set; }
        public List<ReservationSeat> Seats { get; set; } = new();

        public bool CanBeCancelled()
        {
            if (Screening == null) return false;
            return DateTime.Now < Screening.StartTime.AddMinutes(-30);
        }
    }
}