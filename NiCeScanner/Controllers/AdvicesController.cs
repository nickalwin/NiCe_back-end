using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class AdvicesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdvicesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Advices
		public async Task<IActionResult> Index(
			string sortOrder,
			string sortOrderContent,
			string sortOrderAdditionalLink,
			string sortOrderAdditionalLinkName,
			string currentFilter,
			string searchString,
			int? pageNumber = 1
		) {
			ViewData["Title"] = "Advices";
			ViewData["CurrentSort"] = sortOrder;
			ViewData["CurrentFilter"] = searchString;
			
			ViewData["ContentSortParam"] = sortOrderContent switch
			{
				"Content" => "Content_desc",
				"Content_desc" => "",
				_ => "Content"
			};
			ViewData["SortOrderContent"] = sortOrderContent;
			
			ViewData["AdditionalLinkSortParam"] = sortOrderAdditionalLink switch
			{
				"AdditionalLink" => "AdditionalLink_desc",
				"AdditionalLink_desc" => "",
				_ => "AdditionalLink"
			};
			ViewData["SortOrderAdditionalLink"] = sortOrderAdditionalLink;
			
			ViewData["AdditionalLinkNameSortParam"] = sortOrderAdditionalLinkName switch
			{
				"AdditionalLinkName" => "AdditionalLinkName_desc",
				"AdditionalLinkName_desc" => "",
				_ => "AdditionalLinkName"
			};
			ViewData["SortOrderAdditionalLinkName"] = sortOrderAdditionalLinkName;
			
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["SearchString"] = searchString;
			
			var advices = from a in _context.Advices
				select a;
			
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				advices = advices.Where(a => a.Data.Contains(searchString));
			}
			
			switch (sortOrderContent)
			{
				case "Content_desc":
					advices = advices.OrderByDescending(a => a.Data);
					break;
				case "Content":
					advices = advices.OrderBy(a => a.Data);
					break;
				default:
					advices = advices.OrderBy(a => a.Id);
					break;
			}
			
			switch (sortOrderAdditionalLink)
			{
				case "AdditionalLink_desc":
					advices = advices.OrderByDescending(a => a.AdditionalLink);
					break;
				case "AdditionalLink":
					advices = advices.OrderBy(a => a.AdditionalLink);
					break;
			}
			
			switch (sortOrderAdditionalLinkName)
			{
				case "AdditionalLinkName_desc":
					advices = advices.OrderByDescending(a => a.AdditionalLinkName);
					break;
				case "AdditionalLinkName":
					advices = advices.OrderBy(a => a.AdditionalLinkName);
					break;
			}
			
			advices = advices.Include(a => a.Question);

			int pageSize = 10;
			var model = await PaginatedList<Advice>.CreateAsync(advices.AsNoTracking(), pageNumber ?? 1, pageSize);

			return View(model);
		}

		// GET: Advices/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices
				.FirstOrDefaultAsync(m => m.Id == id);
			if (advice == null)
			{
				return NotFound();
			}
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(advice.Data);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
			);        
			
			ViewBag.Languages = originalLanguages;

			return View(advice);
		}

		// GET: Advices/Create
		public async Task<IActionResult> Create()
		{
			ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");

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

		// POST: Advices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Data,Condition,QuestionId,AdditionalLink,AdditionalLinkName,Languages")] AdviceForm form)
		{
			var languages = form.Languages;

			if (ModelState.IsValid)
			{
				var languagesWithName = languages.ToDictionary(
					language => language.Key,
					language => new { data = language.Value }
				);

				string jsonString = JsonConvert.SerializeObject(languagesWithName);
				
				var advice = new Advice
				{
					Data = jsonString,
					Condition = form.Condition,
					QuestionId = form.QuestionId,
					AdditionalLink = form.AdditionalLink,
					AdditionalLinkName = form.AdditionalLinkName,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};

				_context.Add(advice);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(form);
		}

		// GET: Advices/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices.FindAsync(id);
			if (advice == null)
			{
				return NotFound();
			}

			ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(advice.Data);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
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

			return View(new AdviceForm
			{
				Id = advice.Id,
				Languages = originalLanguages,
				Condition = advice.Condition,
				QuestionId = advice.QuestionId,
				AdditionalLink = advice.AdditionalLink,
				AdditionalLinkName = advice.AdditionalLinkName
			});
		}

		// POST: Advices/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Data,Condition,QuestionId,AdditionalLink,AdditionalLinkName,Languages")] AdviceForm form)
		{
			if (ModelState.IsValid)
			{
				var languagesWithName = form.Languages.ToDictionary(
					language => language.Key,
					language => new { data = language.Value }
				);

				string jsonString = JsonConvert.SerializeObject(languagesWithName);
				
				var advice = await _context.Advices.FindAsync(id);
				if (advice == null)
				{
					return NotFound();
				}
				
				advice.Data = jsonString;
				advice.Condition = form.Condition;
				advice.QuestionId = form.QuestionId;
				advice.AdditionalLink = form.AdditionalLink;
				advice.AdditionalLinkName = form.AdditionalLinkName;
				advice.UpdatedAt = DateTime.Now;
				
				_context.Update(advice);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Details), new { id });
			}
			return View(form);
		}

		// GET: Advices/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices
				.FirstOrDefaultAsync(m => m.Id == id);
			if (advice == null)
			{
				return NotFound();
			}

			return View(advice);
		}

		// POST: Advices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var advice = await _context.Advices.FindAsync(id);
			if (advice != null)
			{
				_context.Advices.Remove(advice);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AdviceExists(long id)
		{
			return _context.Advices.Any(e => e.Id == id);
		}
	}
}
