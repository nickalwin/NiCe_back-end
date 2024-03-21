using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;

namespace NiCeScanner.Controllers.API
{
	[Route("api/opinions")]
	[ApiController]
	public class OpinionController : Controller
	{
		private readonly ApplicationDbContext _context;

		public OpinionController(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
