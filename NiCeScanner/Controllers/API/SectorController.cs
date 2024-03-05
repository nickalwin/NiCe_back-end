using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Data;

namespace NiCeScanner.Controllers.API
{
	[Route("api/sectors")]
	[ApiController]
	public class SectorController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SectorController(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
