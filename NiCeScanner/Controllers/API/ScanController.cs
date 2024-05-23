using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.Request.Scan;
using NiCeScanner.Resources.API;
using NiCeScanner.Utilities;
using Newtonsoft.Json;

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
						.ThenInclude(q => q.Advice)
				.Include(s => s.Answers)
					.ThenInclude(a => a.Question)
						.ThenInclude(q => q.Category)
							.ThenInclude(c => c.Links)
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
					Category_links = g.First().Question.Category.Links.Select(l => new LinkResource
					{
						Name = l.Name,
						Href = l.Href
					}),
					Grouped_answers = g.Select(a => new GroupedCategoryQuestionsResource
					{
						Question_data = a.Question.Data,
						Question_uuid = a.Question.Uuid,
						Answer = a.Score,
						Comment = a.Comment,
						Advice = a.Question.Advice != null
							? (a.Score <= a.Question.Advice.Condition ? a.Question.Advice.Data : "") : ""
					})
				});

			// get the average results for those categories
			var scans = await _context.Scans.ToListAsync();
			var allResults = new List<ScanResultElement>();

			if (scans.Count > 5) // TODO Take a look at this limit.
			{
				foreach (var s in scans)
				{
					if (s.Results is null)
						continue;
					var results = JsonConvert.DeserializeObject<IEnumerable<ScanResultElement>>(s.Results);

					foreach (var r in results!)
					{
						allResults.Add(r);
					}
				}
			}

			var averagesOfCategories = allResults
				.GroupBy(r => r.Category_uuid)
				.Select(g => new
				{
					Category_uuid = g.Key,
					g.First().Category_name,
					Mean = g.Average(r => r.Mean)
				});

			string averages = JsonConvert.SerializeObject(averagesOfCategories);
				
			var scanResource = new ScanResource
			{
				Uuid = scan.Uuid,
				Contact_name = scan.ContactName,
				Contact_email = scan.ContactEmail,
				Results = scan.Results,
				Average_results = averages,
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

			ScanCode editCode = new ScanCode
			{
				Code = Guid.NewGuid(),
				CanEdit = true,
				ScanId = newScan.Id,
			};

			ScanCode viewCode = new ScanCode
			{
				Code = Guid.NewGuid(),
				CanEdit = false,
				ScanId = newScan.Id,
			};

			_context.ScanCodes.AddRange(editCode, viewCode);
			await _context.SaveChangesAsync();

			return new PostScanRequestResult 
			{ 
				Uuid = newScan.Uuid,
				Edit_code = editCode.Code,
				View_code = viewCode.Code,
			};
		}

		[HttpDelete]
		[Route("{uuid}/deleteContactInfo")]
		public async Task<ActionResult<object>> DeleteContactInfo(string uuid)
		{
			Guid guid = Guid.Parse(uuid);

			Scan? scan = await _context.Scans.FirstOrDefaultAsync(s => s.Uuid == guid);

			if (scan is null)
			{
				return NotFound("Scan not found");
			}

			scan.ContactName = "";
			scan.ContactEmail = "";

			_context.Scans.Update(scan);

			// from answers remove comments for this scan
			List<Answer> answers = await _context.Answers.Where(a => a.ScanId == scan.Id).ToListAsync();
			foreach (Answer answer in answers)
			{
				answer.Comment = "";
				_context.Answers.Update(answer);
			}

			await _context.SaveChangesAsync();

			return Ok();
		}

		[Route("{scanUuid}/updateAnswer/{questionUuid}")]
		[HttpPut]
		public async Task<ActionResult<object>> UpdateAnswer(string scanUuid, string questionUuid, [FromBody] PutScanUpdateAnswerRequest request)
		{
			Guid scanGuid = Guid.Parse(scanUuid);
			Guid questionGuid = Guid.Parse(questionUuid);

			Scan? scan = await _context.Scans
				.Include(s => s.Answers)
					.ThenInclude(a => a.Question)
						.ThenInclude(q => q.Category)
				.FirstOrDefaultAsync(s => s.Uuid == scanGuid);
			
			if (scan is null)
			{
				return NotFound("Scan not found");
			}

			Answer? answer = _context.Answers.FirstOrDefault(a => a.ScanId == scan.Id && a.Question.Uuid == questionGuid);

			if (answer is null)
			{
				return NotFound("Answer not found");
			}

			answer.Score = request.Answer;
			answer.Comment = request.Comment ?? "";

			_context.Answers.Update(answer);
			await _context.SaveChangesAsync();

			_context.Entry(scan).Collection(s => s.Answers).Load();

			Dictionary<Guid, double> results = ScanResultCalculator.CalculateResults(scan);
			List<Category> categories = await _context.Categories.Where(c => results.Keys.Contains(c.Uuid)).ToListAsync();

			scan.Results = ScanResultCalculator.SerializeResults(results, categories);

			_context.Scans.Update(scan);
			await _context.SaveChangesAsync();

			var data = new {
				answer = request.Answer,
				comment = request.Comment ?? ""
			};

			return Ok(data);
		}
	}
}
