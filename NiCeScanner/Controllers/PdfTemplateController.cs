using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(template.Title);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
			);
			ViewBag.Titles = originalLanguages;
			
			return View(template);
		}
		
		// POST: PdfTemplate/EditTitle
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditTitle(Dictionary<string, string> Languages)
		{
			var pdf = _context.PdfTemplates.FirstOrDefault();
			
			var languagesWithName = Languages.ToDictionary(
				language => language.Key,
				language => new { data = language.Value }
			);

			string jsonString = JsonConvert.SerializeObject(languagesWithName);
			
			pdf.Title = jsonString;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/EditIntroduction
		public async Task<IActionResult> EditIntroduction()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
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
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(template.Introduction);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
			);
			ViewBag.Introductions = originalLanguages;
			
			return View(template);
		}
		
		// POST: PdfTemplate/EditIntroduction
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditIntroduction(Dictionary<string, string> Languages)
		{
			var pdf = _context.PdfTemplates.FirstOrDefault();
			
			var languagesWithName = Languages.ToDictionary(
				language => language.Key,
				language => new { data = language.Value }
			);

			string jsonString = JsonConvert.SerializeObject(languagesWithName);
			
			pdf.Introduction = jsonString;
			
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
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(template.BeforePlotText);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
			);
			ViewBag.BeforePlotTexts = originalLanguages;
			
			return View(template);
		}
		
		// POST: PdfTemplate/BeforePlotText
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditBeforePlotText(Dictionary<string, string> Languages)
		{
			var pdf = _context.PdfTemplates.FirstOrDefault();
			
			var languagesWithName = Languages.ToDictionary(
				language => language.Key,
				language => new { data = language.Value }
			);

			string jsonString = JsonConvert.SerializeObject(languagesWithName);
			
			pdf.BeforePlotText = jsonString;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
		
		// GET: PdfTemplate/AfterPlotText
		public async Task<IActionResult> EditAfterPlotText()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			
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
			
			var languagesWithName = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(template.AfterPlotText);

			var originalLanguages = languagesWithName.ToDictionary(
				language => language.Key,
				language => language.Value["data"]
			);
			ViewBag.AfterPlotTexts = originalLanguages;
			
			return View(template);
		}
		
		// POST: PdfTemplate/AfterPlotText
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAfterPlotText(Dictionary<string, string> Languages)
		{
			var pdf = _context.PdfTemplates.FirstOrDefault();
			
			var languagesWithName = Languages.ToDictionary(
				language => language.Key,
				language => new { data = language.Value }
			);

			string jsonString = JsonConvert.SerializeObject(languagesWithName);
			
			pdf.AfterPlotText = jsonString;
			
			_context.Update(pdf);
			await _context.SaveChangesAsync();
			
			return RedirectToAction(nameof(Index));
		}
	}
}
