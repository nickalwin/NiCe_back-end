using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;

namespace NiCeScanner.Controllers
{
	[Route("api/[controller]")]
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
