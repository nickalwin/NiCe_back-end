using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

		[Authorize(Policy = "RequireStudentRole")]
		public async Task<IActionResult> Index(
			string sortOrder,
			string sortOrderCategory,
			string sortOrderShow,
			string sortOrderCreatedAt,
			string sortOrderUpdatedAt,
			string currentFilter,
			string searchString,
			int? pageNumber
		) {
			ViewData["Title"] = "Categories";
			ViewData["CurrentSort"] = sortOrder;
			ViewData["CurrentFilter"] = searchString;
			
			ViewData["CategorySortParm"] = sortOrderCategory switch
			{
				"Category" => "Category_desc",
				"Category_desc" => "",
				_ => "Category"
			};
			ViewData["SortOrderCategory"] = sortOrderCategory;

			ViewData["ShowSortParm"] = sortOrderShow switch
			{
				"Show" => "Show_desc",
				"Show_desc" => "",
				_ => "Show"
			};
			ViewData["SortOrderShow"] = sortOrderShow;

			ViewData["CreatedAtSortParm"] = sortOrderCreatedAt switch
			{
				"CreatedAt_desc" => "CreatedAt",
				"CreatedAt" => "",
				_ => "CreatedAt_desc"
			};
			ViewData["SortOrderCreatedAt"] = sortOrderCreatedAt;

			ViewData["UpdatedAtSortParm"] = sortOrderUpdatedAt switch
			{
				"UpdatedAt_desc" => "UpdatedAt",
				"UpdatedAt" => "",
				_ => "UpdatedAt_desc"
			};
			ViewData["SortOrderUpdatedAt"] = sortOrderUpdatedAt;

			if (!string.IsNullOrWhiteSpace(searchString))
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["SearchString"] = searchString;

			var categories = from s in _context.Categories
							 select s;

			if (!string.IsNullOrEmpty(searchString))
			{
				categories = categories.Where(s => s.Data.Contains(searchString));
			}

			switch (sortOrderCategory)
			{
				case "Category_desc":
					categories = categories.OrderByDescending(s => s.Data);
					break;
				case "Category":
					categories = categories.OrderBy(s => s.Data);
					break;
				default:
					categories = categories.OrderBy(s => s.Id);
					break;
			}
			
			switch (sortOrderShow)
			{
				case "Show_desc":
					categories = categories.OrderByDescending(s => s.Show);
					break;
				case "Show":
					categories = categories.OrderBy(s => s.Show);
					break;
			}
			
			switch (sortOrderCreatedAt)
			{
				case "CreatedAt_desc":
					categories = categories.OrderByDescending(s => s.CreatedAt);
					break;
				case "CreatedAt":
					categories = categories.OrderBy(s => s.CreatedAt);
					break;
			}
			
			switch (sortOrderUpdatedAt)
			{
				case "UpdatedAt_desc":
					categories = categories.OrderByDescending(s => s.UpdatedAt);
					break;
				case "UpdatedAt":
					categories = categories.OrderBy(s => s.UpdatedAt);
					break;
			}

			int pageSize = 10;
			var model = await PaginatedList<Category>.CreateAsync(categories.AsNoTracking(), pageNumber ?? 1, pageSize);

			return View(model);
		}

		// GET: Category/Details/5
		[Authorize(Policy = "RequireStudentRole")]
		public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (category == null)
            {
                return NotFound();
            }
            
            var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(category.Data);

            var originalLanguages = languagesWithName.ToDictionary(
	            language => language.Key,
	            language => language.Value["name"]
            );        
			
            ViewBag.Languages = originalLanguages;

            return View(category);
        }

		// GET: Category/Create
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Create()
		{
			if (ServiceLocator.ServiceProvider is not null)
			{
				var languages = await ServiceLocator.ServiceProvider.GetService<LanguagesService>()!.FetchLanguagesAsync();
				languages = languages.Where(l => l.LangCode != "en" && l.LangCode != "nl").ToList();
				ViewBag.Languages = languages;
				
			}
			else
			{
				ViewBag.Languages = new List<Language>();
			}

            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Create([Bind("Data,Show,Color,Languages")] CategoryForm categoryForm)
		{
			var languages = categoryForm.Languages;
			
			if (ModelState.IsValid)
			{
				var languagesWithName = languages.ToDictionary(
					language => language.Key,
					language => new { name = language.Value }
				);

				string jsonString = JsonConvert.SerializeObject(languagesWithName);

				var category = new Category
				{
					Data = jsonString,
					Show = categoryForm.Show,
					Color = categoryForm.Color,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};

				_context.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(categoryForm);
		}
		
		// GET: Category/Edit/5
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Edit(long? id)
        {
            if (id is null)
                return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();

			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(category.Data);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["name"]
			);

			if (ServiceLocator.ServiceProvider is not null)
			{
				var languages = await ServiceLocator.ServiceProvider.GetService<LanguagesService>()!.FetchLanguagesAsync();
				languages = languages.Where(l => l.LangCode != "en" && l.LangCode != "nl").ToList();
				ViewBag.Languages = languages;
			}
			else
			{
				ViewBag.Languages = new List<Language>();
			}

            return View(new CategoryForm
			{
				Id = category.Id,
				Languages = originalLanguages,
				Color = category.Color,
				Show = category.Show
			});
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Edit(long id, [Bind("Id,Data,Color,Show,Languages")] CategoryForm form)
        {
            if (id != form.Id)
                return NotFound();

            var languages = form.Languages;
			
            if (ModelState.IsValid)
            {
	            var languagesWithName = languages.ToDictionary(
		            language => language.Key,
		            language => new { name = language.Value }
	            );

	            string jsonString = JsonConvert.SerializeObject(languagesWithName);
				var category = await _context.Categories.FindAsync(id);
				if (category is null)
					return NotFound();

				category.Data = jsonString;
				category.Color = form.Color;
				category.Show = form.Show;
				category.UpdatedAt = DateTime.Now;

				_context.Update(category);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Details), new { id });
            }

            return View(form);
        }

		// GET: Category/Delete/5
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
