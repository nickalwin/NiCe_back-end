using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.API;

namespace NiCeScanner.Controllers
{
	[Route("api/[controller]")]
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
				.Select(q => new QuestionResource
				{
					Uuid = q.Uuid,
					Data = q.Data,
					Category_uuid = q.Category.Uuid,
					Category_name = q.Category.Name,
					Statement = q.Statement,
					Image = q.Image,
				})
				.ToListAsync();

			return questions;
		}
	}
}
