using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaBooking.API.Data;
using CinemaBooking.API.Models;

namespace CinemaBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
   // [Authorize(Roles = "Admin")]
    public class ScreeningController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ScreeningController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("by-movie/{movieId}")]
        public IActionResult GetScreeningsByMovie(int movieId)
        {
            var screening23 = _db.Screenings.Where(x => x.MovieId == movieId && x.StartTime >= DateTime.UtcNow.Date);


            return Ok(screening23);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddScreening(Screening screening)
        {
            _db.Screenings.Add(screening);
            await _db.SaveChangesAsync();
            return Ok(screening);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            var screening = await _db.Screenings.FindAsync(id);

            if (screening == null)
            {
                return NotFound(new { message = "Seans o podanym ID nie istnieje." });
            }

            _db.Screenings.Remove(screening);
            await _db.SaveChangesAsync();

            return NoContent(); // 204
        }

        [HttpGet("{id}/reserved-seats")]
        public async Task<IActionResult> GetReservedSeats(int id)
        {
            var seatIds = await _db.Reservations
                .Where(r => r.ScreeningId == id)
                .SelectMany(r => r.Seats)
                .Select(rs => rs.SeatId)
                .ToListAsync();

            return Ok(seatIds);
        }

    }
}

