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
			string sortOrder,
			string currentFilter,
			string searchString,
			string sortOrderCode,
			string sortOrderContactName,
			string sortOrderContactEmail,
			string sortOrderCanEdit,
			int? pageNumber
		) {
			ViewData["Title"] = "Scan Codes";
			ViewData["CurrentSort"] = sortOrder;

			ViewData["SearchString"] = searchString;

			ViewData["ContactNameParam"] = sortOrderContactName switch
			{
				"ContactName" => "ContactName_desc",
				"ContactName_desc" => "",
				_ => "ContactName"
			};
			ViewData["SortOrderContactName"] = sortOrderContactName;
			
			ViewData["ContactEmailParam"] = sortOrderContactEmail switch
			{
				"ContactEmail" => "ContactEmail_desc",
				"ContactEmail_desc" => "",
				_ => "ContactEmail"
			};
			ViewData["SortOrderContactEmail"] = sortOrderContactEmail;
			
			ViewData["CanEditParam"] = sortOrderCanEdit switch
			{
				"CanEdit" => "CanEdit_desc",
				"CanEdit_desc" => "",
				_ => "CanEdit"
			};
			ViewData["SortOrderCanEdit"] = sortOrderCanEdit;
			
			ViewData["CodeParam"] = sortOrderCode switch
			{
				"Code" => "Code_desc",
				"Code_desc" => "",
				_ => "Code"
			};
			ViewData["SortOrderCode"] = sortOrderCode;
			
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
									   || s.CanEdit.ToString().Contains(searchString)
									   || s.Code.ToString().Contains(searchString));
			}
			
			codes = sortOrderContactName switch
			{
				"ContactName_desc" => codes.OrderByDescending(s => s.Scan.ContactName),
				"ContactName" => codes.OrderBy(s => s.Scan.ContactName),
				_ => codes
			};
			
			codes = sortOrderContactEmail switch
			{
				"ContactEmail_desc" => codes.OrderByDescending(s => s.Scan.ContactEmail),
				"ContactEmail" => codes.OrderBy(s => s.Scan.ContactEmail),
				_ => codes
			};
			
			codes = sortOrderCanEdit switch
			{
				"CanEdit_desc" => codes.OrderByDescending(s => s.CanEdit),
				"CanEdit" => codes.OrderBy(s => s.CanEdit),
				_ => codes
			};
			
			codes = sortOrderCode switch
			{
				"Code_desc" => codes.OrderByDescending(s => s.Code),
				"Code" => codes.OrderBy(s => s.Code),
				_ => codes
			};
			
			int pageSize = 10;
			
			var model = await PaginatedList<ScanCode>.CreateAsync(codes.AsNoTracking(), pageNumber ?? 1, pageSize);

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
        public async Task<IActionResult> Create([Bind("Code,CanEdit,ScanId")] ScanCodeForm form)
        {
            if (ModelState.IsValid)
            {
				var scanCode = new ScanCode
				{
					Code = form.Code,
					CanEdit = form.CanEdit,
					ScanId = form.ScanId
				};
                _context.Add(scanCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScanId"] = new SelectList(_context.Scans, "Id", "Id", form.ScanId);
            return View(form);
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
            
            return View(new ScanCodeForm()
            {
	            Id = scanCode.Id,
	            Code = scanCode.Code,
	            ScanId = scanCode.ScanId,
	            CanEdit = scanCode.CanEdit
            });
        }

        // POST: ScanCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,CanEdit,ScanId")] ScanCodeForm form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
	            var scanCode = _context.ScanCodes.Find(id);
	            scanCode.Code = form.Code;
	            scanCode.CanEdit = form.CanEdit;
	            scanCode.ScanId = form.ScanId;
	            
	            _context.Update(scanCode);
	            await _context.SaveChangesAsync();

	            return RedirectToAction(nameof(Details), new { id });
            }
            
            return View(form);
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
