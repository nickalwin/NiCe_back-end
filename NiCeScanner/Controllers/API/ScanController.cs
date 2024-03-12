using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Migrations;
using NiCeScanner.Models;
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

		[HttpGet("{uuid}")]
		public async Task<ActionResult<object>> GetScan(string uuid) // TODO
		{
			Guid guid = Guid.Parse(uuid);

			var scan = await _context.Scans.Where(s => s.Uuid == guid)
											.Include(s => s.Answers)
											.ThenInclude(a => a.Question)
											.FirstOrDefaultAsync();

			// Scan? scan = await _context.Scans.Where(s => s.Uuid == Guid.Parse(uuid))
											// .Include(s => s.Answers)
											// .ThenInclude(a => a.Question)
											// .FirstOrDefaultAsync();


			// if (scan is null)
			// {
			// 	return NotFound("Scan not found");
			// }


			return new { uuid };
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

			List<Question> questions = await _context.Questions.Where(q => questionUuids.Contains(q.Uuid))
															   .ToListAsync();

			var newAnswers = scan.Answers.Select(answer => new Answer
			{
				ScanId = newScan.Id,
				QuestionId = questions.First(q => q.Uuid == answer.Question_uuid).Id,
				Score = answer.Answer,
				Comment = answer.Comment ?? "",
			}).ToList();

			_context.Answers.AddRange(newAnswers);

			await _context.SaveChangesAsync();

			Dictionary<Guid, double> totalWeightedScores = new Dictionary<Guid, double>();
			Dictionary<Guid, double> totalWeights = new Dictionary<Guid, double>();

			foreach (var answer in scan.Answers)
			{
				Guid questionUuid = answer.Question_uuid;
				Guid categoryUuid = answer.Category_uuid;

				short score = answer.Answer;
				short weight = questions.First(q => q.Uuid == questionUuid).Weight;

				double weightedScore = score * weight;

				if (totalWeightedScores.ContainsKey(categoryUuid))
				{
					totalWeightedScores[categoryUuid] += weightedScore;
					totalWeights[categoryUuid] += weight;
				}
				else
				{
					totalWeightedScores[categoryUuid] = weightedScore;
					totalWeights[categoryUuid] = weight;
				}
			}

			Dictionary<Guid, double> categoryWeightedMeans = new Dictionary<Guid, double>();
			foreach (var categoryUuid in totalWeightedScores.Keys)
			{
				categoryWeightedMeans[categoryUuid] = totalWeightedScores[categoryUuid] / totalWeights[categoryUuid];
			}

			var categories = _context.Categories.ToList();

			var categoryResults = (
				from c in categories
				join m in categoryWeightedMeans on c.Uuid equals m.Key
				select new
				{
					category_name = c.Name,
					category_uuid = c.Uuid,
					mean = m.Value
				}
			).ToList();

			newScan.Results = JsonConvert.SerializeObject(categoryResults);

			_context.Scans.Update(newScan);

			await _context.SaveChangesAsync();

			return Ok("Scan created successfully");
		}
	}
}
