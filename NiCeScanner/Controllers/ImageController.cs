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
		public IActionResult Index()
		{
			var images = _context.Images.ToList();
			return View(images);
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

		// GET: Image/Display/id
		public async Task<IActionResult> Display(int id)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
				return NotFound();

			return File(image.ImageData, "image/jpeg");
		}

		public async Task<IActionResult> Details(int id)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
				return NotFound();

			return View(image);
		}

		[HttpPost]
		public async Task<IActionResult> EditName(int id, string newName)
		{
			var image = await _context.Images.FindAsync(id);
			if (image == null)
			{
				return NotFound();
			}

			image.FileName = newName;
			_context.Images.Update(image);
			await _context.SaveChangesAsync();

			return RedirectToAction("Details", new { id = image.Id }); // Redirect to image details page or any other appropriate action
		}

	}
}
