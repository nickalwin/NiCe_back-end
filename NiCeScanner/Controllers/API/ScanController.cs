using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.API;
using NiCeScanner.Resources.Request;

namespace NiCeScanner.Controllers.API
{
	[Route("api/scans")]
	[ApiController]
	public class ScanController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ScanController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
        [Route("getQuestions")]
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

		[HttpPost]
		public async Task<ActionResult<PostScanRequest>> StoreScan(PostScanRequest scan)
		{
			var newScan = new Scan
			{
				ContactName = scan.Contact_name,
				ContactEmail = scan.Contact_email,
				SectorId = scan.Sector_id,
				CreatedAt = DateTime.Now,
			};

			_context.Scans.Add(newScan);

			await _context.SaveChangesAsync();

			List<Guid> questionUuids = scan.Answers.Select(a => a.Question_uuid).ToList();

			Dictionary<Guid, long> questions = await _context.Questions.Where(q => questionUuids.Contains(q.Uuid))
																	   .ToDictionaryAsync(q => q.Uuid, q => q.Id);

			var newAnswers = scan.Answers.Select(answer => new Answer
			{
				ScanId = newScan.Id,
				QuestionId = questions[answer.Question_uuid],
				Score = answer.Answer,
			}).ToList();

			_context.Answers.AddRange(newAnswers);

			await _context.SaveChangesAsync();

			return Ok("Scan created successfully");
		}
	}
}
