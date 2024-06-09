using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class PdfTemplateController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PdfTemplateController(ApplicationDbContext context)
		{
			_context = context;
		}
		
		// GET: PdfTemplate
		public async Task<IActionResult> Index(string path = "")
		{
			@ViewData["Title"] = "Pdf Template";

			var template = _context.PdfTemplates.FirstOrDefault();
			if (template == null)
			{
				return NotFound();
			}
			
			var imageBase64Data = Convert.ToBase64String(template.ImageData);
			var imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
			ViewBag.ImageDataUrl = imageDataURL;
			
			var vpath = path == "" ? "" : path + "/";
			var examplePlotBase64Data = Convert.ToBase64String(System.IO.File.ReadAllBytes(path + "wwwroot/images/ExamplePlot.png"));
			var examplePlotDataURL = string.Format("data:image/png;base64,{0}", examplePlotBase64Data);
			ViewBag.ExamplePlot = examplePlotDataURL;
			
			return View(template);
		}
		
		// GET: PdfTemplate/EditTitle
		public async Task<IActionResult> EditTitle()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
			return View(template);
		}
		
		// POST: PdfTemplate/EditTitle
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditTitle(string title)
		{
			if (title == "")
			{
				return RedirectToAction(nameof(EditTitle));
			}
			
			var pdf = _context.PdfTemplates.FirstOrDefault();
			pdf.Title = title;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/EditIntroduction
		public async Task<IActionResult> EditIntroduction()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
			return View(template);
		}
		
		// POST: PdfTemplate/EditIntroduction
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditIntroduction(string introduction)
		{
			if (introduction == "")
			{
				return RedirectToAction(nameof(EditIntroduction));
			}
			
			var pdf = _context.PdfTemplates.FirstOrDefault();
			pdf.Introduction = introduction;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/EditImage
		public async Task<IActionResult> EditImage()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
			return View(template);
		}
		
		// POST: PdfTemplate/EditImage
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditImage(IFormFile image)
		{
			if (image == null || image.Length == 0)
				return Content("file not selected");
			
			var pdf = _context.PdfTemplates.FirstOrDefault();
			
			using (var memoryStream = new MemoryStream())
			{
				await image.CopyToAsync(memoryStream);
				pdf.ImageData = memoryStream.ToArray();
			}
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/BeforePlotText
		public async Task<IActionResult> EditBeforePlotText()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
			return View(template);
		}
		
		// POST: PdfTemplate/BeforePlotText
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditBeforePlotText(string beforePlotText)
		{
			if (beforePlotText == "")
			{
				return RedirectToAction(nameof(EditBeforePlotText));
			}
			
			var pdf = _context.PdfTemplates.FirstOrDefault();
			pdf.BeforePlotText = beforePlotText;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/AfterPlotText
		public async Task<IActionResult> EditAfterPlotText()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
			return View(template);
		}
		
		// POST: PdfTemplate/AfterPlotText
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAfterPlotText(string afterPlotText)
		{
			if (afterPlotText == "")
			{
				return RedirectToAction(nameof(EditAfterPlotText));
			}
			
			var pdf = _context.PdfTemplates.FirstOrDefault();
			pdf.AfterPlotText = afterPlotText;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
	}
}
