using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;

namespace NiCeScanner.Controllers
{
	[Route("api/[controller]")]
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
