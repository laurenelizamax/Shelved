﻿using System;
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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var books = _context.Book
                .Include(b => b.ApplicationUser)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.GenresForBooks)
                .Where(b => b.ApplicationUserId == user.Id);
            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Title,ApplicationUserId,Author,Year,IsRead,ImagePath,GenreIds")] BookViewModel bookViewModel)
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
                    ImagePath = bookViewModel.ImagePath,
                    ApplicationUserId = user.Id
                };


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
                ImagePath = book.ImagePath,
                ApplicationUserId = user.Id,
                GenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList()
            };
            
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", book.ApplicationUserId);
            ViewData["GenresForBooks"] = new SelectList(_context.GenresForBook, "Id", "Description", bookViewModel.GenreIds);

            return View(bookViewModel);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ApplicationUserId,Author,Year,IsRead,ImagePath,GenreIds")] BookViewModel bookViewModel)
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
                bookModel.ImagePath = bookViewModel.ImagePath;
                bookModel.ApplicationUserId = user.Id;

                bookModel.BookGenres = bookViewModel.GenreIds.Select(gid => new BookGenre
                {
                    BookId = bookViewModel.Id,
                    GenreId = gid
                }).ToList();

                try
                {
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
