namespace CinemaBooking.API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string? PosterUrl { get; set; }

        public List<Screening> Screenings { get; set; } = new();
    }
}