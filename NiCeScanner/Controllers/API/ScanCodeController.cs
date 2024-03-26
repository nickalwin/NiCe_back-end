using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.API;

namespace NiCeScanner.Controllers.API
{
	[Route("api/scan-codes")]
	[ApiController]
	public class ScanCodeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ScanCodeController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("{uuid}")]
		public async Task<ActionResult<ValidatedScanCodeResource>> ValidateScanCode(string uuid)
		{
			Guid guid = Guid.Parse(uuid);

			ScanCode? scanCode = await _context.ScanCodes
				.FirstOrDefaultAsync(s => s.Code == guid);

			if (scanCode is null)
			{
				return NotFound("Scan code not valid!");
			}

			Scan? scan = await _context.Scans
				.FirstOrDefaultAsync(s => s.Id == scanCode.ScanId);

			if (scan is null)
			{
				return NotFound("Scan not found for this code!");
			}

			return new ValidatedScanCodeResource
			{
				Scan_uuid = scan.Uuid,
				Editable = scanCode.CanEdit
			};
		}
	}
}
