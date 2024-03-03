using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

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
		public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
		{
			return await _context.Questions.ToListAsync();
		}
	}
}
