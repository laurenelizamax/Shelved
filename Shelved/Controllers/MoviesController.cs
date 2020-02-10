using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
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
        public async Task<IActionResult> Index(string searchMovies, int page = 1, string sortBy = "Title")
        {
            var user = await GetCurrentUserAsync();


            if (searchMovies == null)
            {
                var movies = _context.Movie
                      .AsNoTracking()
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.GenresForMovies)
                  .Where(m => m.MyMovies == true && m.ApplicationUserId == user.Id);


                var model = await PagingList.CreateAsync(
                                movies, 10, page, sortBy, "Title");

                model.RouteValue = new RouteValueDictionary {
                    { "searchMovies", searchMovies}
                     };

                return View(model);
            }
            else
            {
                var movies = _context.Movie
                          .AsNoTracking()
               .Where(m => m.MyMovies == true && m.ApplicationUserId == user.Id &&
                     (m.Title.Contains(searchMovies) || m.Year.Contains(searchMovies)))
                    .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.GenresForMovies);

                var model = await PagingList.CreateAsync(
                                              movies, 5, page, sortBy, "Title");

                model.RouteValue = new RouteValueDictionary {
                    { "searchMovies", searchMovies}
                     };

                return View(model);
            }
        }


        // GET: Wish List Movies
        public async Task<IActionResult> WishList(string searchWishList)
        {
            var user = await GetCurrentUserAsync();

            if (searchWishList == null)
            {
                var movies = _context.Movie
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.GenresForMovies)
                    .Where(m => m.ApplicationUserId == user.Id && m.WishList == true);
                return View(await movies.ToListAsync());
            }
            else
            {
                var movies = _context.Movie
                    .Where(m => m.WishList == true && m.ApplicationUserId == user.Id &&
                          (m.Title.Contains(searchWishList) || m.Year.Contains(searchWishList)))
                        .Include(m => m.MovieGenres)
                            .ThenInclude(mg => mg.GenresForMovies);
                return View(await movies.ToListAsync());
            }
        }

        // GET: Watch List Movies
        public async Task<IActionResult> WatchList(string searchWatchList)
        {
            var user = await GetCurrentUserAsync();

            if (searchWatchList == null)
            {
                var movies = _context.Movie
                .Include(m => m.MovieGenres)
                     .ThenInclude(mg => mg.GenresForMovies)
                .Where(m => m.ApplicationUserId == user.Id && m.WatchList == true);
                return View(await movies.ToListAsync());
            }
            else
            {
                var movies = _context.Movie
                    .Where(m => m.WatchList == true && m.ApplicationUserId == user.Id &&
                          (m.Title.Contains(searchWatchList) || m.Year.Contains(searchWatchList)))
                        .Include(m => m.MovieGenres)
                             .ThenInclude(mg => mg.GenresForMovies);
                return View(await movies.ToListAsync());
            }
        }

        // GET: Seen List Movies
        public async Task<IActionResult> SeenList(string searchSeenList)
        {
            var user = await GetCurrentUserAsync();

            if (searchSeenList == null)
            {
                var movies = _context.Movie
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.GenresForMovies)
                .Where(m => m.ApplicationUserId == user.Id && m.SeenList == true);
                return View(await movies.ToListAsync());
            }
            else
            {
                var movies = _context.Movie
                    .Where(m => m.SeenList == true && m.ApplicationUserId == user.Id &&
                         (m.Title.Contains(searchSeenList) || m.Year.Contains(searchSeenList)))
                        .Include(m => m.MovieGenres)
                            .ThenInclude(mg => mg.GenresForMovies);
                return View(await movies.ToListAsync());
            }
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Where(m => m.ApplicationUserId == user.Id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ApplicationUserId,Year,IsWatched,ImagePath,GenreIds,WatchList,WishList,SeenList,MyMovies,File")] MovieViewModel movieViewModel, IFormFile image)
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
                    //ImagePath = movieViewModel.ImagePath,
                    ApplicationUserId = user.Id,
                    MyMovies = movieViewModel.MyMovies,
                    WatchList = movieViewModel.WatchList,
                    WishList = movieViewModel.WishList,
                    SeenList = movieViewModel.SeenList
                };

                if (movieViewModel.File != null && movieViewModel.File.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(movieViewModel.File.FileName); //getting path of actual file name
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName); //creating path combining file name w/ www.root\\images directory
                    using (var fileSteam = new FileStream(filePath, FileMode.Create)) //using filestream to get the actual path
                    {
                        await movieViewModel.File.CopyToAsync(fileSteam);
                    }
                    movieModel.ImagePath = fileName;

                }

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
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.MovieGenres)
                 .ThenInclude(mg => mg.GenresForMovies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieViewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                IsWatched = movie.IsWatched,
                File = movie.File,
                ImagePath = movie.ImagePath,
                ApplicationUserId = user.Id,
                MyMovies = movie.MyMovies,
                WatchList = movie.WatchList,
                WishList = movie.WishList,
                SeenList = movie.SeenList,
                GenreIds = movie.MovieGenres.Select(mg => mg.GenreId).ToList()
            };

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", movieViewModel.ApplicationUserId);
            ViewData["GenresForMovies"] = new SelectList(_context.GenresForMovie, "Id", "Description", movieViewModel.GenreIds);

            return View(movieViewModel);
        }

        // POST: Movies/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ApplicationUserId,Year,IsWatched,ImagePath,GenreIds,MyMovies,SeenList,WatchList,WishList,File")] MovieViewModel movieViewModel, IFormFile file)
        {
            var user = await GetCurrentUserAsync();

            if (id != movieViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var movieModel = await _context.Movie
                   .Include(m => m.MovieGenres)
                   .FirstOrDefaultAsync(b => b.Id == id);

                movieModel.Id = movieViewModel.Id;
                movieModel.Title = movieViewModel.Title;
                movieModel.Year = movieViewModel.Year;
                movieModel.IsWatched = movieViewModel.IsWatched;
                movieModel.File = movieViewModel.File;
                movieModel.ImagePath = movieViewModel.ImagePath;
                movieModel.ApplicationUserId = user.Id;
                movieModel.MyMovies = movieViewModel.MyMovies;
                movieModel.WatchList = movieViewModel.WatchList;
                movieModel.WishList = movieViewModel.WishList;
                movieModel.SeenList = movieViewModel.SeenList;

                movieModel.MovieGenres = movieViewModel.GenreIds.Select(gid => new MovieGenre
                {
                    MovieId = movieViewModel.Id,
                    GenreId = gid
                }).ToList();

                try
                {
                    if (movieViewModel.File != null && movieViewModel.File.Length > 0)
                    {
                        var fileName = Path.GetFileName(movieViewModel.File.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create)) //using filestream to get the actual path 
                        {
                            await movieViewModel.File.CopyToAsync(fileStream);
                        }
                        movieModel.ImagePath = fileName;
                    }
                    else
                    {
                        movieModel.ImagePath = _context.Movie.AsNoTracking().Single<Movie>(b => b.Id == movieViewModel.Id).ImagePath;
                    }

                    _context.Update(movieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movieViewModel.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", movieViewModel.ApplicationUserId);
            ViewData["GenresForMovies"] = new SelectList(_context.GenresForMovie, "Id", "Description", movieViewModel.GenreIds);

            return View(movieViewModel);
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
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.GenresForMovies)
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
            var movie = await _context.Movie
                .Include(m => m.MovieGenres)
                 .FirstOrDefaultAsync(m => m.Id == id);
            foreach (var item in movie.MovieGenres)
            {
                _context.MovieGenre.Remove(item);

            }
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
