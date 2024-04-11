using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
    public class ScanCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScanCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: ScanCodes
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
			var codes = from s in _context.ScanCodes.Include(s => s.Scan)
						select s;

			if (!string.IsNullOrEmpty(searchString))
			{
				codes = codes.Where(s => s.Scan.ContactName.Contains(searchString)
									   || s.Scan.ContactEmail.Contains(searchString)
									   || s.CanEdit.ToString().Contains(searchString));
			}
			int pageSize = 10;
			var model = await PaginatedList<ScanCode>.CreateAsync(codes.AsNoTracking(), pageNumber ?? 1, pageSize);
			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_CodesTable", model);
			}
			return View(model);
		}

        // GET: ScanCodes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scanCode = await _context.ScanCodes
                .Include(s => s.Scan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scanCode == null)
            {
                return NotFound();
            }

            return View(scanCode);
        }

        // GET: ScanCodes/Create
        public IActionResult Create()
        {
            ViewData["ScanId"] = new SelectList(_context.Scans, "Id", "Id");
            return View();
        }

        // POST: ScanCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,CanEdit,ScanId")] ScanCode scanCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scanCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScanId"] = new SelectList(_context.Scans, "Id", "Id", scanCode.ScanId);
            return View(scanCode);
        }

        // GET: ScanCodes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scanCode = await _context.ScanCodes.FindAsync(id);
            if (scanCode == null)
            {
                return NotFound();
            }
            ViewData["ScanId"] = new SelectList(_context.Scans, "Id", "Id", scanCode.ScanId);
            return View(scanCode);
        }

        // POST: ScanCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,CanEdit,ScanId")] ScanCode scanCode)
        {
            if (id != scanCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scanCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScanCodeExists(scanCode.Id))
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
            ViewData["ScanId"] = new SelectList(_context.Scans, "Id", "Id", scanCode.ScanId);
            return View(scanCode);
        }

        // GET: ScanCodes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scanCode = await _context.ScanCodes
                .Include(s => s.Scan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scanCode == null)
            {
                return NotFound();
            }

            return View(scanCode);
        }

        // POST: ScanCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var scanCode = await _context.ScanCodes.FindAsync(id);
            if (scanCode != null)
            {
                _context.ScanCodes.Remove(scanCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScanCodeExists(long id)
        {
            return _context.ScanCodes.Any(e => e.Id == id);
        }
    }
}
