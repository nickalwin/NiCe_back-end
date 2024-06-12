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
				.Where(q => q.Category.Show)
				.Include(q => q.Image)
				.Select(q => new QuestionResource
				{
					Uuid = q.Uuid,
					Data = q.Data,
					Category_uuid = q.Category.Uuid,
					Category_data = q.Category.Data,
					Category_color = q.Category.Color,
					Statement = q.Statement,
					Image_data = q.Image.ImageData.Length > 0 ? "data:image/jpeg;base64," + Convert.ToBase64String(q.Image.ImageData) : null,
				})
				.ToListAsync();

			return Ok(questions);
		}
	}
}
