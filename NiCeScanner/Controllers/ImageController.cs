using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class ImageController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ImageController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Image
		public async Task<IActionResult> Index(
			string currentFilter,
			string searchString,
			int? pageNumber	
		) {
			ViewData["Title"] = "Scans";
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

			var images = from i in _context.Images
						 select i;
			
			if (!string.IsNullOrEmpty(searchString))
			{
				images = images.Where(i => i.FileName.Contains(searchString));
			}

			int pageSize = 15;
			var paginatedList = await PaginatedList<ImageModel>.CreateAsync(images, pageNumber ?? 1, pageSize);
			
			return View(paginatedList);
		}

		// GET: Image/Upload
		public IActionResult Upload()
		{
			return View();
		}

		// POST: Image/Upload
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upload(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Content("file not selected");

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				var image = new ImageModel
				{
					FileName = file.FileName,
					ImageData = memoryStream.ToArray()
				};
				_context.Images.Add(image);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet("/Image/Display/{id}")]
		public async Task<IActionResult> Display(long id)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
				return NotFound();

			return File(image.ImageData, "image/jpeg");
		}

		public async Task<IActionResult> Details(long id)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
				return NotFound();

			return View(image);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(long id)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
				return NotFound();
			
			var questions = from q in _context.Questions
							where q.ImageId == id
							select q;
			foreach (var question in questions)
			{
				question.ImageId = null;
			}
			await _context.SaveChangesAsync();

			_context.Images.Remove(image);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

	}
}
