using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

namespace NiCeScanner.Controllers.API
{
	[Route("api/questions")]
	[ApiController]
	public class QuestionController : Controller
	{
		private readonly ApplicationDbContext _context;

		public QuestionController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<QuestionResource>>> GetQuestions()
		{
			var questions = await _context.Questions
				.Where(q => q.Show)
				.Include(q => q.Category)
				.Include(q => q.Image)
				.Select(q => new QuestionResource
				{
					Uuid = q.Uuid,
					Data = q.Data,
					Category_uuid = q.Category.Uuid,
					Category_data = q.Category.Data,
					Statement = q.Statement,
					Image_uuid = q.Image.Uuid,
					Image_data = Convert.ToBase64String(q.Image.ImageData)
				})
				.ToListAsync();

			return questions;
		}
	}
}
