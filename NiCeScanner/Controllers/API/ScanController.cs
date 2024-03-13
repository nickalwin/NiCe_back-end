using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Migrations;
using NiCeScanner.Models;
using NiCeScanner.Resources.Request.Scan;
using NiCeScanner.Utilities;
using System;

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
