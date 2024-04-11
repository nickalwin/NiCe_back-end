﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Migrations;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
    public class ScansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Scans
        public async Task<IActionResult> Index(
			string currentFilter,
			string searchString,
			int? pageNumber
			)
		{
			ViewData["SearchString"] = searchString;

			if (searchString != null)
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["CurrentFilter"] = searchString;

			var scans = from s in _context.Scans.Include(s => s.Sector)
						select s;
			if (!string.IsNullOrEmpty(searchString))
			{
				scans = scans.Where(s => s.ContactName.Contains(searchString)
									   || s.ContactEmail.Contains(searchString)
									   || s.Results.Contains(searchString)
									   || s.Sector.Data.Contains(searchString));
			}
			int pageSize = 10;
			var model = await PaginatedList<Scan>.CreateAsync(scans.AsNoTracking(), pageNumber ?? 1, pageSize);
			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_ScanTable", model);
			}
			return View(model);
        }

		// GET: Scans/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scan = await _context.Scans
				.Include(s => s.Sector)
				.Include(s => s.Answers)
					.ThenInclude(a => a.Question)
					.ThenInclude(q => q.Category)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (scan == null)
			{
				return NotFound();
			}

			return View(scan);
		}


		// GET: Scans/Create
		public IActionResult Create()
        {
            ViewData["SectorId"] = new SelectList(_context.Sectors, "Id", "Id");
            return View();
        }

        // POST: Scans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Uuid,ContactName,ContactEmail,SectorId,Results,CreatedAt,UpdatedAt")] Scan scan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectorId"] = new SelectList(_context.Sectors, "Id", "Id", scan.SectorId);
            return View(scan);
        }

        // GET: Scans/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scan = await _context.Scans.FindAsync(id);
            if (scan == null)
            {
                return NotFound();
            }
            ViewData["SectorId"] = new SelectList(_context.Sectors, "Id", "Id", scan.SectorId);
            return View(scan);
        }

        // POST: Scans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Uuid,ContactName,ContactEmail,SectorId,Results,CreatedAt,UpdatedAt")] Scan scan)
        {
            if (id != scan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScanExists(scan.Id))
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
            ViewData["SectorId"] = new SelectList(_context.Sectors, "Id", "Id", scan.SectorId);
            return View(scan);
        }

        // GET: Scans/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scan = await _context.Scans
                .Include(s => s.Sector)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scan == null)
            {
                return NotFound();
            }

            return View(scan);
        }

        // POST: Scans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var scan = await _context.Scans.FindAsync(id);
            if (scan != null)
            {
                _context.Scans.Remove(scan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScanExists(long id)
        {
            return _context.Scans.Any(e => e.Id == id);
        }
    }
}
