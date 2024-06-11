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
		public async Task<IActionResult> Index(
			string sortOrder,
			string sortOrderName,
			string sortOrderHref,
			string sortOrderCategory,
			string currentFilter,
			string searchString,
			int pageNumber = 1
		) {
			ViewData["Title"] = "Links";
			ViewData["CurrentSort"] = sortOrder;
			ViewData["CurrentFilter"] = searchString;
			
			ViewData["NameSortParam"] = sortOrderName switch
			{
				"Name" => "Name_desc",
				"Name_desc" => "",
				_ => "Name"
			};
			ViewData["SortOrderName"] = sortOrderName;
			
			ViewData["HrefSortParam"] = sortOrderHref switch
			{
				"Href" => "Href_desc",
				"Href_desc" => "",
				_ => "Href"
			};
			ViewData["SortOrderHref"] = sortOrderHref;
			
			ViewData["CategorySortParam"] = sortOrderCategory switch
			{
				"Category" => "Category_desc",
				"Category_desc" => "",
				_ => "Category"
			};
			ViewData["SortOrderCategory"] = sortOrderCategory;
			
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["SearchString"] = searchString;
			
			var links = from s in _context.Links
				select s;
			
			if (!string.IsNullOrEmpty(searchString))
			{
				links = links.Where(s => s.Name.Contains(searchString));
			}
			
			switch (sortOrderName)
			{
				case "Name_desc":
					links = links.OrderByDescending(s => s.Name);
					break;
				case "Name":
					links = links.OrderBy(s => s.Name);
					break;
				default:
					links = links.OrderBy(s => s.Id);
					break;
			}
			
			switch (sortOrderHref)
			{
				case "Href_desc":
					links = links.OrderByDescending(s => s.Href);
					break;
				case "Href":
					links = links.OrderBy(s => s.Href);
					break;
			}
			
			switch (sortOrderCategory)
			{
				case "Category_desc":
					links = links.OrderByDescending(s => s.Category.Data);
					break;
				case "Category":
					links = links.OrderBy(s => s.Category.Data);
					break;
			}
			
			links = links.Include(l => l.Category);
			
			int pageSize = 10;
			var paginatedList = await PaginatedList<Link>.CreateAsync(links.AsNoTracking(), pageNumber, pageSize);

			return View(paginatedList);
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
				Id = link.Id,
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
			if (id != form.Id)
				return NotFound();
			
			var link = await _context.Links.FindAsync(id);
			
			if (link == null)
				return NotFound();

			if (ModelState.IsValid)
			{
				link.Name = form.Name;
				link.Href = form.Href;
				link.CategoryId = form.CategoryId;
				link.UpdatedAt = DateTime.Now;
				
				_context.Update(link);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Details), new { id });
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
