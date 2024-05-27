using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	[Authorize]
	public class LinksController : Controller
	{
		private readonly ApplicationDbContext _context;

		public LinksController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Links
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Links.Include(l => l.Category);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Links/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var link = await _context.Links
				.Include(l => l.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (link == null)
			{
				return NotFound();
			}

			return View(link);
		}

		// GET: Links/Create
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
			return View();
		}

		// POST: Links/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Href,CategoryId")] LinkForm form)
		{
			if (ModelState.IsValid)
			{
				var link = new Link
				{
					Name = form.Name,
					Href = form.Href,
					CategoryId = form.CategoryId,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};

				_context.Add(link);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", form.CategoryId);
			
			return View(form);
		}

		// GET: Links/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var link = await _context.Links.FindAsync(id);
			if (link == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", link.CategoryId);
			var form = new LinkForm
			{
				Name = link.Name,
				Href = link.Href,
			};

			return View(form);
		}

		// POST: Links/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Name,Href,CategoryId")] LinkForm form)
		{
			var link = new Link
			{
				Id = id,
				Name = form.Name,
				Href = form.Href,
				CategoryId = form.CategoryId,
				UpdatedAt = DateTime.Now
			};

			if (ModelState.IsValid)
			{
				_context.Update(link);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", link.CategoryId);

			return View(form);
		}

		// GET: Links/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var link = await _context.Links
				.Include(l => l.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (link == null)
			{
				return NotFound();
			}

			return View(link);
		}

		// POST: Links/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var link = await _context.Links.FindAsync(id);
			if (link != null)
			{
				_context.Links.Remove(link);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool LinkExists(long id)
		{
			return _context.Links.Any(e => e.Id == id);
		}
	}
}
