using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.API.Data;
using CinemaBooking.API.Models;

namespace CinemaBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MovieController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _db.Movies.ToList();
            return Ok(movies);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}