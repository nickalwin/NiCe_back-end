using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.Request.Scan;
using NiCeScanner.Resources.API;
using NiCeScanner.Utilities;

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

		[HttpGet("{uuid}")]
		public async Task<ActionResult<ScanResource>> GetScan(string uuid)
		{
			Guid guid = Guid.Parse(uuid);

			var scan = await _context.Scans
				.Include(s => s.Answers)
					.ThenInclude(a => a.Question)
						.ThenInclude(q => q.Category)
				.FirstOrDefaultAsync(s => s.Uuid == guid);

			if (scan == null)
			{
				return NotFound("Scan not found");
			}

			var groupedAnswers = scan.Answers
				.GroupBy(a => a.Question.Category.Uuid)
				.Select(g => new ScanResultDataResource
				{
					Category_uuid = g.Key,
					Grouped_answers = g.Select(a => new GroupedCategoryQuestionsResource
					{
						Question_data = a.Question.Data,
						Question_uuid = a.Question.Uuid,
						Answer = a.Score,
						Comment = a.Comment
					})
				});

			var scanResource = new ScanResource
			{
				Uuid = scan.Uuid,
				Contact_name = scan.ContactName,
				Contact_email = scan.ContactEmail,
				Results = scan.Results,
				Created_at = scan.CreatedAt,
				Updated_at = scan.UpdatedAt,
				Data = groupedAnswers
			};

			return scanResource;
		}

		[HttpPost]
		public async Task<ActionResult<PostScanRequestResult>> StoreScan(PostScanRequest scan)
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
			List<Question> questions = await _context.Questions.Where(q => questionUuids.Contains(q.Uuid)).ToListAsync();
			Dictionary<Guid, Question> questionDictionary = questions.ToDictionary(q => q.Uuid, q => q);

			var newAnswers = scan.Answers.Select(answer => new Answer
			{
				ScanId = newScan.Id,
				QuestionId = questionDictionary[answer.Question_uuid].Id,
				Score = answer.Answer,
				Comment = answer.Comment ?? "",
			}).ToList();

			_context.Answers.AddRange(newAnswers);

			Dictionary<Guid, double> categoryWeightedMeans = ScanResultCalculator.CalculateResults(scan.Answers, questionDictionary);
			List<Category> categories = await _context.Categories.Where(c => categoryWeightedMeans.Keys.Contains(c.Uuid)).ToListAsync();

			newScan.Results = ScanResultCalculator.SerializeResults(categoryWeightedMeans, categories);

			_context.Scans.Update(newScan);
			await _context.SaveChangesAsync();

			return new PostScanRequestResult 
			{ 
				Uuid = newScan.Uuid 
			};
		}
	}
}
