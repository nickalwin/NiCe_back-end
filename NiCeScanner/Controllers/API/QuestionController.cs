using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;

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
	}
}
