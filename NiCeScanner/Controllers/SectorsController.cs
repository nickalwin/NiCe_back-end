using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Views
{
    public class SectorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sectors
        public async Task<IActionResult> Index(
	        string sortOrder,
	        string sortOrderSector,
	        string currentFilter,
	        string searchString,
	        int? pageNumber
	    ) {
	        ViewData["Title"] = "Categories";
	        ViewData["CurrentSort"] = sortOrder;
	        ViewData["CurrentFilter"] = searchString;
	        
	        ViewData["SectorSortParam"] = sortOrderSector switch
	        {
		        "Sector" => "Sector_desc",
		        "Sector_desc" => "",
		        _ => "Sector"
	        };
	        ViewData["SortOrderSector"] = sortOrderSector;
	        
	        if (!string.IsNullOrWhiteSpace(searchString))
	        {
		        pageNumber = 1;
	        }
	        else
	        {
		        searchString = currentFilter;
	        }

	        ViewData["SearchString"] = searchString;

	        var sectors = from s in _context.Sectors
		        select s;
	        
	        if (!string.IsNullOrEmpty(searchString))
	        {
		        sectors = sectors.Where(s => s.Data.Contains(searchString));
	        }

	        switch (sortOrderSector)
	        {
		        case "Sector_desc":
			        sectors = sectors.OrderByDescending(s => s.Data);
			        break;
		        case "Sector":
			        sectors = sectors.OrderBy(s => s.Data);
			        break;
		        default:
			        sectors = sectors.OrderBy(s => s.Id);
			        break;
	        }
	        
	        
	        int pageSize = 10;
	        var model = await PaginatedList<Sector>.CreateAsync(sectors.AsNoTracking(), pageNumber ?? 1, pageSize);

	        return View(model);        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            
            var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(sector.Data);

            var originalLanguages = languagesWithName.ToDictionary(
	            language => language.Key,
	            language => language.Value["name"]
            );        
			
            ViewBag.Languages = originalLanguages;

            return View(sector);
        }

        // GET: Sectors/Create
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

        // POST: Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Languages")] SectorForm form)
        {
	        var languages = form.Languages;

            if (ModelState.IsValid)
            {
	            var languagesWithName = languages.ToDictionary(
		            language => language.Key,
		            language => new { name = language.Value }
	            );

	            string jsonString = JsonConvert.SerializeObject(languagesWithName);

	            var sector = new Sector()
	            {
		            Data = jsonString
	            };
	            
                _context.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors.FindAsync(id);
            if (sector == null)
            {
                return NotFound();
            }
            
            var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(sector.Data);

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
            
            return View(new SectorForm()
            {
	            Languages = originalLanguages
            });
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Languages")] SectorForm form)
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
	            var sector = await _context.Sectors.FindAsync(id);
	            if (sector is null)
		            return NotFound();

	            sector.Data = jsonString;

	            _context.Update(sector);
	            await _context.SaveChangesAsync();

	            return RedirectToAction(nameof(Details), new { id });
            }
            
            return View(form);
        }
        private bool SectorExists(long id)
        {
            return _context.Sectors.Any(e => e.Id == id);
        }
    }
}
