using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shelved.Data;
using Shelved.Models;
using Shelved.Models.ViewModels;

namespace Shelved.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Movie
                .Include(m => m.ApplicationUser)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.GenresForMovies)
                               .Where(b => b.ApplicationUserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.ApplicationUser)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.GenresForMovies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["GenresForMovies"] = new SelectList(_context.GenresForMovie, "Id", "Description");

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ApplicationUserId,Year,IsWatched,ImagePath,GenreIds")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();

                // Add movie to database
                var movieModel = new Movie
                {
                    Title = movieViewModel.Title,
                    Year = movieViewModel.Year,
                    IsWatched = movieViewModel.IsWatched,
                    ImagePath = movieViewModel.ImagePath,
                    ApplicationUserId = user.Id
                };

                _context.Add(movieModel);
                await _context.SaveChangesAsync();

                // Add genres to movie
                movieModel.MovieGenres = movieViewModel.GenreIds.Select(genreId => new MovieGenre
                {
                    MovieId = movieModel.Id,
                    GenreId = genreId
                }).ToList();

                // Save again to database
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", movieViewModel.ApplicationUserId);
            ViewData["GenresForMovies"] = new SelectList(_context.GenresForMovie, "Id", "Description", movieViewModel.GenreIds);

            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", movie.ApplicationUserId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ApplicationUserId,Year,IsWatched,ImagePath")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", movie.ApplicationUserId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
