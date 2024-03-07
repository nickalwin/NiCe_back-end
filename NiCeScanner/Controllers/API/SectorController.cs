using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

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

		[HttpGet]
        public async Task<ActionResult<IEnumerable<SectorResource>>> GetSectors()
        {
            var sectors = await _context.Sectors.ToListAsync();

			return Ok(sectors.Select(s => new SectorResource
			{
				Name = s.Name
			}));
        }
	}
}
