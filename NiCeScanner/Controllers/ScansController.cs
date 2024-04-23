using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NiCeScanner.Data;
using NiCeScanner.Migrations;
using NiCeScanner.Models;
using OfficeOpenXml;

namespace NiCeScanner.Controllers
{
	[Authorize(Policy = "RequireStudentRole")]
	public class ScansController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ScansController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Scans
		public async Task<IActionResult> Index(
	string sortOrder,
	string sortOrderContactName,
	string sortOrderContactEmail,
	string sortOrderSector,
	string sortOrderCreatedAt,
	string sortOrderUpdatedAt,
	string currentFilter,
	string searchString,
	int? pageNumber
)
		{
			ViewData["Title"] = "scans";
			ViewData["CurrentSort"] = sortOrder;

			ViewData["SearchString"] = searchString;

			ViewData["ContactNameParm"] = sortOrderContactName switch
			{
				"ContactName" => "ContactName_desc",
				"ContactName_desc" => "",
				_ => "ContactName"
			};
			ViewData["SortOrderContactName"] = sortOrderContactName;

			ViewData["ContactEmailParm"] = sortOrderContactEmail switch
			{
				"ContactEmail" => "ContactEmail_desc",
				"ContactEmail_desc" => "",
				_ => "ContactEmail"
			};
			ViewData["SortOrderContactEmail"] = sortOrderContactEmail;

			ViewData["SectorSortParm"] = sortOrderSector switch
			{
				"Sector" => "Sector_desc",
				"Sector_desc" => "",
				_ => "Sector"
			};
			ViewData["SortOrderSector"] = sortOrderSector;

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

			if (searchString != null)
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["CurrentFilter"] = searchString;

			var scans = _context.Scans.Include(s => s.Sector).AsQueryable();
			var sectors = await _context.Scans.Select(s => s.Sector).Distinct().ToListAsync();
			var sectorData = sectors.Select(s => JsonConvert.DeserializeObject<JObject>(s.Data)).ToList();
			var sectorNames = sectorData.Select(s => s["nl"]["name"].ToString()).ToList();
			ViewBag.Sectors = sectorNames;


			if (!string.IsNullOrEmpty(searchString))
			{
				scans = scans.Where(s => s.ContactName.Contains(searchString)
										|| s.ContactEmail.Contains(searchString)
										|| s.Results.Contains(searchString)
										|| s.Sector.Data.Contains(searchString));
			}

			scans = sortOrderContactName switch
			{
				"ContactName_desc" => scans.OrderByDescending(s => s.ContactName),
				"ContactName" => scans.OrderBy(s => s.ContactName),
				_ => scans
			};

			scans = sortOrderContactEmail switch
			{
				"ContactEmail_desc" => scans.OrderByDescending(s => s.ContactEmail),
				"ContactEmail" => scans.OrderBy(s => s.ContactEmail),
				_ => scans
			};

			scans = sortOrderSector switch
			{
				"Sector_desc" => scans.OrderByDescending(s => s.Sector.Data),
				"Sector" => scans.OrderBy(s => s.Sector.Data),
				_ => scans
			};

			scans = sortOrderCreatedAt switch
			{
				"CreatedAt_desc" => scans.OrderByDescending(s => s.CreatedAt),
				"CreatedAt" => scans.OrderBy(s => s.CreatedAt),
				_ => scans
			};

			scans = sortOrderUpdatedAt switch
			{
				"UpdatedAt_desc" => scans.OrderByDescending(s => s.UpdatedAt),
				"UpdatedAt" => scans.OrderBy(s => s.UpdatedAt),
				_ => scans
			};

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
		[Authorize(Policy = "RequireManagerRole")]
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
		[Authorize(Policy = "RequireManagerRole")]
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

		public IActionResult DownloadExcel()
		{
			var scans = _context.Scans
			   .Include(s => s.Sector)
			   .Include(s => s.Answers)
				   .ThenInclude(a => a.Question)
			   .ToList();

			var questions = scans
				   .SelectMany(s => s.Answers.Select(a => a.Question))
				   .Distinct()
				   .ToList();

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Scans NL");

				var lastColumnAddress = GetColumnName(questions.Count+3);
				var tableRange = worksheet.Range($"A1:{lastColumnAddress}{scans.Count+1}");
				var table = tableRange.CreateTable();
				table.ShowAutoFilter = true;

				worksheet.Cell(1, 1).Value = "Contact naam";
				//worksheet.Cell(1, 2).Value = "Contact email";
				worksheet.Cell(1, 3).Value = "Sector";

				int columnIndex = 4;
				foreach (var question in questions)
				{
					var questionData = JsonConvert.DeserializeObject<JObject>(question.Data)["nl"]["question"].ToString();
					worksheet.Cell(1, columnIndex).Value = questionData;
					columnIndex++;
				}
				columnIndex = 4;

				int rowIndex = 2;
				foreach (var scan in scans)
				{
					worksheet.Cell(rowIndex, 1).Value = scan.ContactName;
					//worksheet.Cell(rowIndex, 2).Value = scan.ContactEmail;
					worksheet.Cell(rowIndex, 3).Value = JsonConvert.DeserializeObject<JObject>(scan.Sector.Data)["nl"]["name"].ToString();

					var answerDict = scan.Answers.ToDictionary(a => a.QuestionId, a => a);

					foreach (var question in questions)
					{
						if (answerDict.TryGetValue(question.Id, out var answer))
						{
							worksheet.Cell(rowIndex, columnIndex).Value = answer.Score.ToString();
						}
						columnIndex++;
					}

					rowIndex++;
					columnIndex = 4;
				}

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Scans.xlsx");
				}
			}
		}

		private string GetColumnName(int columnIndex)
		{
			const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var columnName = "";

			while (columnIndex > 0)
			{
				var remainder = (columnIndex - 1) % 26;
				columnName = letters[remainder] + columnName;
				columnIndex = (columnIndex - 1) / 26;
			}

			return columnName;
		}

	}
}
