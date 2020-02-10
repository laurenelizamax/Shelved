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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<IActionResult> Index(int page = 1, int pageSize = 2, string searchBooks = "Title")
        //{
        //    var user = await GetCurrentUserAsync();


        //    if (string.IsNullOrWhiteSpace(searchBooks))
        //    {
        //        var books = _context.Book
        //        .Where(b => b.MyBooks == true && b.ApplicationUserId == user.Id)
        //         .Include(b => b.BookGenres)
        //            .ThenInclude(bg => bg.GenresForBooks);

        //        return View(await books.ToListAsync());
        //    }
        //    else
        //    {
        //        var books = _context.Book
        //        .Where(b => b.WishList == true && b.ApplicationUserId == user.Id
        //            && (b.Title.Contains(searchBooks) || b.Author.Contains(searchBooks)))
        //                .Include(b => b.BookGenres)
        //                     .ThenInclude(bg => bg.GenresForBooks);

        //        return View(await books.ToListAsync());

        //    }
        //}




        public async Task<IActionResult> Index(string searchBooks, int page = 1, string sortBy = "Title")
        {
            var user = await GetCurrentUserAsync();

            if (string.IsNullOrWhiteSpace(searchBooks))
            {

                var books = _context.Book
                    .AsNoTracking()
                    .Where(b => b.MyBooks == true && b.ApplicationUserId == user.Id)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.GenresForBooks);

                var model = await PagingList.CreateAsync(
                             books, 10, page, sortBy, "Title");

                model.RouteValue = new RouteValueDictionary {
                    { "searchBooks", searchBooks}
                     };

                return View(model);
            }
            else
            {
                var books = _context.Book
                      .AsNoTracking()
                .Where(b => b.MyBooks == true
                    && b.ApplicationUserId == user.Id
                    && (b.Title.Contains(searchBooks) || b.Author.Contains(searchBooks)))
                        .Include(b => b.BookGenres)
                             .ThenInclude(bg => bg.GenresForBooks);

                var model = await PagingList.CreateAsync(
                                    books, 10, page, sortBy, "Title");

                model.RouteValue = new RouteValueDictionary {
                    { "SearchBooks", searchBooks}
                        };

                return View(model);
            }
        }

        // Get Books Wish List
        public async Task<IActionResult> WishList(string searchWish)
        {
            var user = await GetCurrentUserAsync();

            if (string.IsNullOrWhiteSpace(searchWish))
            {

                var booksWish = _context.Book
                    .Where(b => b.WishList == true && b.ApplicationUserId == user.Id)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.GenresForBooks);

                return View(await booksWish.ToListAsync());

            }
            else
            {
                var booksWish = _context.Book
                .Where(b => b.WishList == true
                    && b.ApplicationUserId == user.Id
                    && (b.Title.Contains(searchWish) || b.Author.Contains(searchWish)))
                        .Include(b => b.BookGenres)
                             .ThenInclude(bg => bg.GenresForBooks);

                return View(await booksWish.ToListAsync());

            }
        }


        // Get Books Read List
        public async Task<IActionResult> ReadList(string searchReadList)
        {
            var user = await GetCurrentUserAsync();

            if (searchReadList == null)
            {

                var books = _context.Book
                    .Where(b => b.ReadList == true && b.ApplicationUserId == user.Id)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.GenresForBooks);

                return View(await books.ToListAsync());

            }
            else
            {
                var books = _context.Book
                .Where(b => b.ReadList == true
                    && b.ApplicationUserId == user.Id
                    && (b.Title.Contains(searchReadList) || b.Author.Contains(searchReadList)))
                        .Include(b => b.BookGenres)
                             .ThenInclude(bg => bg.GenresForBooks);

                return View(await books.ToListAsync());
            }
        }


        // Get Books Read IT List
        public async Task<IActionResult> ReadItList(string searchReadIt)
        {
            var user = await GetCurrentUserAsync();

            if (searchReadIt == null)
            {

                var books = _context.Book
                    .Where(b => b.ReadItList == true && b.ApplicationUserId == user.Id)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.GenresForBooks);

                return View(await books.ToListAsync());

            }
            else
            {
                var books = _context.Book
                    .Where(b => b.ReadItList == true && b.ApplicationUserId == user.Id &&
                      (b.Title.Contains(searchReadIt) || b.Author.Contains(searchReadIt)))
                        .Include(b => b.BookGenres)
                             .ThenInclude(bg => bg.GenresForBooks);

                return View(await books.ToListAsync());

            }
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Where(b => b.ApplicationUserId == user.Id)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.GenresForBooks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["GenresForBooks"] = new SelectList(_context.GenresForBook, "Id", "Description");

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ApplicationUserId,Author,Year,IsRead,ImagePath,GenreIds,MyBooks,ReadList,WishList,ReadItList,File")] BookViewModel bookViewModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();

                // Add book to database
                var bookModel = new Book
                {
                    Title = bookViewModel.Title,
                    Author = bookViewModel.Author,
                    Year = bookViewModel.Year,
                    IsRead = bookViewModel.IsRead,
                    ApplicationUserId = user.Id,
                    MyBooks = bookViewModel.MyBooks,
                    ReadList = bookViewModel.ReadList,
                    WishList = bookViewModel.WishList,
                    ReadItList = bookViewModel.ReadItList
                };

                if (bookViewModel.File != null && bookViewModel.File.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(bookViewModel.File.FileName); //getting path of actual file name
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName); //creating path combining file name w/ www.root\\images directory
                    using (var fileSteam = new FileStream(filePath, FileMode.Create)) //using filestream to get the actual path
                    {
                        await bookViewModel.File.CopyToAsync(fileSteam);
                    }
                    bookModel.ImagePath = fileName;

                }

                _context.Add(bookModel);
                await _context.SaveChangesAsync();

                // Add genres to book
                bookModel.BookGenres = bookViewModel.GenreIds.Select(genreId => new BookGenre
                {
                    BookId = bookModel.Id,
                    GenreId = genreId
                }).ToList();

                // Save again to database
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", bookViewModel.ApplicationUserId);
            ViewData["GenresForBooks"] = new SelectList(_context.GenresForBook, "Id", "Description", bookViewModel.GenreIds);
            return View(bookViewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookGenres)
                 .ThenInclude(bg => bg.GenresForBooks)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                IsRead = book.IsRead,
                File = book.File,
                ImagePath = book.ImagePath,
                ApplicationUserId = user.Id,
                MyBooks = book.MyBooks,
                ReadList = book.ReadList,
                WishList = book.WishList,
                ReadItList = book.ReadItList,
                GenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList()
            };

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", book.ApplicationUserId);
            ViewData["GenresForBooks"] = new SelectList(_context.GenresForBook, "Id", "Description", bookViewModel.GenreIds);

            return View(bookViewModel);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ApplicationUserId,Author,Year,IsRead,ImagePath,GenreIds,MyBooks,ReadList,WishList,ReadItList,File")] BookViewModel bookViewModel, IFormFile file)
        {
            var user = await GetCurrentUserAsync();

            if (id != bookViewModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var bookModel = await _context.Book
                    .Include(b => b.BookGenres)
                    .FirstOrDefaultAsync(b => b.Id == id);

                bookModel.Id = bookViewModel.Id;
                bookModel.Title = bookViewModel.Title;
                bookModel.Author = bookViewModel.Author;
                bookModel.Year = bookViewModel.Year;
                bookModel.IsRead = bookViewModel.IsRead;
                bookModel.File = bookViewModel.File;
                bookModel.ImagePath = bookViewModel.ImagePath;
                bookModel.ApplicationUserId = user.Id;
                bookModel.ReadItList = bookViewModel.ReadItList;
                bookModel.WishList = bookViewModel.WishList;
                bookModel.MyBooks = bookViewModel.MyBooks;
                bookModel.ReadList = bookViewModel.ReadList;

                bookModel.BookGenres = bookViewModel.GenreIds.Select(gid => new BookGenre
                {
                    BookId = bookViewModel.Id,
                    GenreId = gid
                }).ToList();

                try
                {
                    if (bookViewModel.File != null && bookViewModel.File.Length > 0)
                    {
                        var fileName = Path.GetFileName(bookViewModel.File.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create)) //using filestream to get the actual path 
                        {
                            await bookViewModel.File.CopyToAsync(fileStream);
                        }
                        bookModel.ImagePath = fileName;
                    }
                    else
                    {
                        bookModel.ImagePath = _context.Book.AsNoTracking().Single<Book>(b => b.Id == bookViewModel.Id).ImagePath;
                    }

                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookViewModel.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", bookViewModel.ApplicationUserId);
            ViewData["GenresForBooks"] = new SelectList(_context.GenresForBook, "Id", "Description", bookViewModel.GenreIds);

            return View(bookViewModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.ApplicationUser)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.GenresForBooks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == id);
            foreach (var item in book.BookGenres)
            {
                _context.BookGenre.Remove(item);

            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
