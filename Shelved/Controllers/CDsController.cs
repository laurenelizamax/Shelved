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
    public class CDsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CDsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CDs
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var cds = _context.CD.Include(c => c.ApplicationUser)
                .Where(c => c.ApplicationUserId == user.Id)
                .Include(c => c.CDGenres)
                .ThenInclude(cg => cg.GenresForCDs);
            return View(await cds.ToListAsync());
        }

        // GET: CDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD
                .Include(c => c.ApplicationUser)
                .Include(c => c.CDGenres)
                .ThenInclude(cg => cg.GenresForCDs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cD == null)
            {
                return NotFound();
            }

            return View(cD);
        }

        // GET: CDs/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["GenresForCDs"] = new SelectList(_context.GetGenresForCD, "Id", "Description");

            return View();
        }

        // POST: CDs/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ApplicationUserId,Artist,Year,IsHeard,ImagePath,GenreIds")] CDViewModel cdViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();

                // Add CD to database
                var cdModel = new CD
                {
                    Title = cdViewModel.Title,
                    Artist = cdViewModel.Artist,
                    Year = cdViewModel.Year,
                    IsHeard = cdViewModel.IsHeard,
                    ImagePath = cdViewModel.ImagePath,
                    ApplicationUserId = user.Id
                };

                _context.Add(cdModel);
                await _context.SaveChangesAsync();

                // Add genres to CD
                cdModel.CDGenres = cdViewModel.GenreIds.Select(genreId => new CDGenre
                {
                    CDId = cdModel.Id,
                    GenreId = genreId
                }).ToList();

                // Save again to database
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cdViewModel.ApplicationUserId);
            ViewData["GenresForCDs"] = new SelectList(_context.GetGenresForCD, "Id", "Description", cdViewModel.GenreIds);
            return View(cdViewModel);
        }

        // GET: CDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD
                .Include(c => c.CDGenres)
                 .ThenInclude(cg => cg.GenresForCDs)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            if (cD == null)
            {
                return NotFound();
            }
            var cDViewModel = new CDViewModel
            {
                Id = cD.Id,
                Title = cD.Title,
                Artist = cD.Artist,
                Year = cD.Year,
                IsHeard = cD.IsHeard,
                ImagePath = cD.ImagePath,
                ApplicationUserId = user.Id,
                GenreIds = cD.CDGenres.Select(bg => bg.GenreId).ToList()
            };

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cDViewModel.ApplicationUserId);
            ViewData["GenresForCDs"] = new SelectList(_context.GetGenresForCD, "Id", "Description", cDViewModel.GenreIds);

            return View(cDViewModel);
        }

        // POST: CDs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ApplicationUserId,Artist,Year,IsHeard,ImagePath,GenreIds")] CDViewModel cDViewModel)
        {
            var user = await GetCurrentUserAsync();

            if (id != cDViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var cDModel = await _context.CD
                  .Include(c => c.CDGenres)
                  .FirstOrDefaultAsync(c => c.Id == id);

                cDModel.Id = cDViewModel.Id;
                cDModel.Title = cDViewModel.Title;
                cDModel.Artist = cDViewModel.Artist;
                cDModel.Year = cDViewModel.Year;
                cDModel.IsHeard = cDViewModel.IsHeard;
                cDModel.ImagePath = cDViewModel.ImagePath;
                cDModel.ApplicationUserId = user.Id;

                cDModel.CDGenres = cDViewModel.GenreIds.Select(gid => new CDGenre
                {
                    CDId = cDViewModel.Id,
                    GenreId = gid
                }).ToList();


                try
                {
                    _context.Update(cDModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDExists(cDViewModel.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cDViewModel.ApplicationUserId);
            ViewData["GenresForCDs"] = new SelectList(_context.GetGenresForCD, "Id", "Description", cDViewModel.GenreIds);

            return View(cDViewModel);
        }

        // GET: CDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD
                .Include(c => c.ApplicationUser)
                .Include(c => c.CDGenres)
                .ThenInclude(cg => cg.GenresForCDs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cD == null)
            {
                return NotFound();
            }

            return View(cD);
        }

        // POST: CDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cD = await _context.CD
                .Include(c => c.CDGenres)
             .FirstOrDefaultAsync(c => c.Id == id);
            foreach (var item in cD.CDGenres)
            {
                _context.CdGenre.Remove(item);

            }
            _context.CD.Remove(cD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CDExists(int id)
        {
            return _context.CD.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
