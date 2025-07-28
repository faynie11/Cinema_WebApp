using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaBooking.API.Data;
using CinemaBooking.API.Models;
using System.Security.Claims;

namespace CinemaBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReservationController(AppDbContext db)
        {
            _db = db;
        }



        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var reservation = new Reservation
            {
                UserId = userId,
                ScreeningId = dto.ScreeningId,
                CreatedAt = DateTime.UtcNow,
                Seats = dto.SeatIds.Select(id => new ReservationSeat { SeatId = id }).ToList()
            };

            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();

            return Ok(new { reservation.Id });
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetUserReservations()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var reservations = await _db.Reservations
                .Include(r => r.Screening)
                    .ThenInclude(s => s.Movie)
                .Include(r => r.Seats)
                    .ThenInclude(rs => rs.Seat)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            var result = reservations.Select(r => new ReservationResponseDto
            {
                Id = r.Id,
                ScreeningId = r.ScreeningId,
                CreatedAt = r.CreatedAt,
                MovieTitle = r.Screening!.Movie!.Title,
                PosterUrl = r.Screening.Movie.PosterUrl ?? "", // jeśli jest nullowalne
                StartTime = r.Screening.StartTime,
                SeatNumbers = r.Seats.Select(s => $"{s.Seat.Row}{s.Seat.Column}").ToList()
            });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _db.Reservations
                .Include(r => r.Screening)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            if (reservation.Screening.StartTime <= DateTime.UtcNow.AddMinutes(30))
                return BadRequest("Nie można usunąć rezerwacji na mniej niż 30 minut przed seansem.");

            _db.Reservations.Remove(reservation);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Screening)
                    .ThenInclude(s => s.Movie)
                .Include(r => r.Seats)
                    .ThenInclude(rs => rs.Seat)
                .ToListAsync();

            var result = reservations.Select(r => new ReservationResponseDto
            {
                Id = r.Id,
                ScreeningId = r.ScreeningId,
                CreatedAt = r.CreatedAt,
                MovieTitle = r.Screening.Movie!.Title,
                PosterUrl = r.Screening.Movie.PosterUrl ?? "",
                StartTime = r.Screening.StartTime,
                SeatNumbers = r.Seats.Select(s => $"{s.Seat.Row}{s.Seat.Column}").ToList(),
                FirstName = r.User?.FirstName ?? "",
                LastName = r.User?.LastName ?? ""
            });

            return Ok(result);
        }




        public class ReservationDto
        {
            public int ScreeningId { get; set; }
            public List<int> SeatIds { get; set; } = new();
        }

        public class ReservationResponseDto
        {
            public int Id { get; set; }
            public int ScreeningId { get; set; }
            public DateTime CreatedAt { get; set; }

            public string MovieTitle { get; set; } = "";
            public string PosterUrl { get; set; } = "";
            public DateTime StartTime { get; set; }

            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";


            public List<string> SeatNumbers { get; set; } = new();
        }
    }
}