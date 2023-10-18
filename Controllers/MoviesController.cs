using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Database;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(MvcMovieContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {

            IQueryable<Movie> movies = _context.Movie.AsQueryable();


            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));

            }


            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);

            }


            return View(movies.ToList());
        }


        public async Task<IActionResult> DataTable()
        {
            return View();
        }


        public async Task<IActionResult> GetAll(string movieGenre, string searchString)
        {
            IQueryable<Movie> movies = _context.Movie.AsQueryable();


            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));

            }


            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);

            }


            List<Movie> result = movies.ToList();
           
            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Movie == null)
            {

                _logger.LogError("id cannot be zero");
                return NotFound();
            }

            Movie movie = _context.Movie
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                _logger.LogError($"Movie with id: {id} not found.");
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                _logger.LogError("id cannot be zero");
                return NotFound();
            }

            Movie movie = _context.Movie.Find(id);
            if (movie == null)
            {
                _logger.LogError($"Movie with id: {id} not found.");
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                _logger.LogError("Ids must be equivalent!");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!MovieExists(movie.Id))
                    {
                        _logger.LogError($"Movie with id: {id} does not exist.");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Unexpected error.");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Id cannot be zero");
                return NotFound();
            }

            Movie movie = _context.Movie
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                _logger.LogError($"Movie with id: {id} not found.");
                return NotFound();
            }

            return View(movie);
        }


        [HttpPost, ActionName("Delete")]
     // [ValidateAntiForgeryToken] //this is commented out so that delete works from the DataTable view
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            Movie movie = _context.Movie.Find(id);

            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
             else
            {
                _logger.LogError($"Movie with id: {id} not found.");
                return NotFound();
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

       
        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
