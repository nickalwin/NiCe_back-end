using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

namespace NiCeScanner.Controllers.API
{
	[Route("api/pdf")]
	public class PdfTemplateController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PdfTemplateController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<PdfTemplateResource>> GetPdf()
		{
			var template = _context.PdfTemplates.FirstOrDefault();
			if (template == null)
			{
				return NotFound();
			}
			
			var imageBase64Data = Convert.ToBase64String(template.ImageData);
			var imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

			return Ok(new PdfTemplateResource()
			{
				Title = template.Title,
				Introduction = template.Introduction,
				ImageData = imageDataURL,
				BeforePlotText = template.BeforePlotText,
				AfterPlotText = template.AfterPlotText
			});
		}
	}
}
