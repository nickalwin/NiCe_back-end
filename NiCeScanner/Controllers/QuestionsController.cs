﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NiCeScanner.Data;
using NiCeScanner.Migrations;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: Questions
		[Authorize(Policy = "RequireStudentRole")]
		public async Task<IActionResult> Index(
			string sortOrderQuestion,
			string sortOrderWeight,
			string sortOrderCategory,
			string sortOrderShow,
			string sortOrderStatement,
			string currentFilter,
			string searchString,
			int? pageNumber)
		{
			ViewData["Title"] = "Questions";

			ViewData["QuestionIDSortParm"] = sortOrderQuestion switch
			{
				"QuestionID_desc" => "QuestionID",
				"QuestionID" => "",
				_ => "QuestionID_desc"
			};
			ViewData["SortOrderQuestion"] = sortOrderQuestion;

			ViewData["WeightSortParm"] = sortOrderWeight switch
			{
				"Weight" => "Weight_desc",
				"Weight_desc" => "",
				_ => "Weight"
			};
			ViewData["SortOrderWeight"] = sortOrderWeight;

			ViewData["CategorySortParm"] = sortOrderCategory switch
			{
				"category" => "category_desc",
				"category_desc" => "",
				_ => "category"
			};
			ViewData["SortOrderCategory"] = sortOrderCategory;

			ViewData["ShowSortParm"] = sortOrderShow switch
			{
				"Show_desc" => "Show",
				"Show" => "",
				_ => "Show_desc"
			};
			ViewData["SortOrderShow"] = sortOrderShow;

			ViewData["StatementSortParm"] = sortOrderStatement switch
			{
				"Statement_desc" => "Statement",
				"Statement" => "",
				_ => "Statement_desc"
			};
			ViewData["SortOrderStatement"] = sortOrderStatement;

			ViewData["SearchString"] = searchString;

			if (searchString != null)
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["CurrentFilter"] = searchString;


			var questions = from s in _context.Questions.Include(q => q.Category)
							select s;

			if (!string.IsNullOrEmpty(searchString))
			{
				questions = questions.Where(s => s.Data.Contains(searchString) || s.Category.Data.Contains(searchString));
			}
			switch (sortOrderCategory)
			{
				case "category_desc":
					questions = questions.OrderByDescending(s => s.Category.Data);
					break;
				case "category":
					questions = questions.OrderBy(s => s.Category.Data);
					break;
			}
			switch (sortOrderQuestion)
			{
				case "QuestionID_desc":
					questions = questions.OrderByDescending(s => s.Id);
					break;
				case "QuestionID":
					questions = questions.OrderBy(s => s.Id);
					break;
				default:
					questions = questions.OrderBy(s => s.Data);
					break;
			}
			switch (sortOrderWeight)
			{
				case "Weight_desc":
					questions = questions.OrderByDescending(s => s.Weight);
					break;
				case "Weight":
					questions = questions.OrderBy(s => s.Weight);
					break;
			}
			switch (sortOrderShow)
			{
				case "Show_desc":
					questions = questions.OrderByDescending(s => s.Show);
					break;
				case "Show":
					questions = questions.OrderBy(s => s.Show);
					break;
			}
			switch (sortOrderStatement)
			{
				case "Statement_desc":
					questions = questions.OrderByDescending(s => s.Statement);
					break;
				case "Statement":
					questions = questions.OrderBy(s => s.Statement);
					break;
			}

			int pageSize = 10;
			var model = await PaginatedList<Question>.CreateAsync(questions.AsNoTracking(), pageNumber ?? 1, pageSize);

			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_QuestionTable", model);
			}

			return View(model);
		}

		// GET: Questions/Details/5
		public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Category)
				.Include(q => q.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

			return View(question);
        }

		// GET: Questions/Create
		[Authorize(Policy = "RequireResearcherRole")]
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
			return View();
		}

		// POST: Questions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Uuid,Data,CategoryId,Weight,Statement,Show,Image,CreatedAt,UpdatedAt")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			var categories = _context.Categories
				.Select(c => new { c.Id, CategoryName = JObject.Parse(c.Data)["nl"]["name"].ToString() })
				.ToList();

			ViewBag.Category = new SelectList(categories, "Id", "CategoryName", question.CategoryId);
			return View(question);
        }

		// GET: Question/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var question = await _context.Questions.FindAsync(id);
			if (question == null)
			{
				return NotFound();
			}

			var categories = _context.Categories
				.Select(c => new { c.Id, CategoryName = JObject.Parse(c.Data)["nl"]["name"].ToString() })
				.ToList();

			ViewBag.Category = new SelectList(categories, "Id", "CategoryName", question.CategoryId);
			ViewBag.Images = new SelectList(await _context.Images.ToListAsync(), "Id", "FileName", question.ImageId);
			return View(question);
		}

		//POST: Question/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Data,CategoryId,Weight,Statement,Show,ImageId")] Question question)
		{
			question.Id = id;
			question.UpdatedAt = DateTime.UtcNow;

			ModelState["Category"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
			ModelState["Image"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

			var categoryIsValid = await _context.Categories.AnyAsync(c => c.Id == question.CategoryId);
			var imageIsValid = await _context.Images.AnyAsync(i => i.Id == question.ImageId);

			if (ModelState.IsValid && categoryIsValid && (imageIsValid || question.ImageId == null))
			{
				try
				{
					_context.Attach(question).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					TempData["SuccessMessage"] = "Question updated successfully.";
					return RedirectToAction("Index");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!QuestionExists(id))
					{
						return NotFound();
					}
					else
					{
						var categories = _context.Categories
							.Select(c => new { c.Id, CategoryName = JObject.Parse(c.Data)["nl"]["name"].ToString() })
							.ToList();

						ViewBag.Category = new SelectList(categories, "Id", "CategoryName", question.CategoryId);
						ViewBag.Images = new SelectList(await _context.Images.ToListAsync(), "Id", "FileName", question.ImageId);
						TempData["ErrorMessage"] = "Failed to update question. Please check the input.";
						return View(question);
						//return BadRequest(new { error = "Failed to update question. Please check the input." });
					}
				}
			}
			else
			{
				var categories = _context.Categories
					.Select(c => new { c.Id, CategoryName = JObject.Parse(c.Data)["nl"]["name"].ToString() })
					.ToList();

				ViewBag.Category = new SelectList(categories, "Id", "CategoryName", question.CategoryId);
				ViewBag.Images = new SelectList(await _context.Images.ToListAsync(), "Id", "FileName", question.ImageId);
				TempData["ErrorMessage"] = "Technical issue, Model not valid.";
				return View(question);
				//var errors = ModelState.Values.SelectMany(v => v.Errors);
				//return BadRequest(new { error = "Technical issue, Model not valid.", model = question, errors = errors });
			}
		}

		// GET: Questions/Delete/5
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Policy = "RequireResearcherRole")]
		public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(long id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

		public IActionResult DownloadExcel()
		{
			var questions = _context.Questions.Include(q => q.Category).ToList();

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Questions NL");

				worksheet.Cell(1, 1).Value = "Question";
				worksheet.Cell(1, 2).Value = "Category";
				worksheet.Cell(1, 3).Value = "Weight";
				worksheet.Cell(1, 4).Value = "Statement";
				worksheet.Cell(1, 5).Value = "Show";

				int rowIndex = 2;
				foreach (var question in questions)
				{
					var questionData = question.Data;
					var questionText = JsonConvert.DeserializeObject<JObject>(questionData)["nl"]["question"].ToString();
					var categoryData = question.Category.Data;
					var categoryText = JsonConvert.DeserializeObject<JObject>(categoryData)["nl"]["name"].ToString();

					worksheet.Cell(rowIndex, 1).Value = questionText;
					worksheet.Cell(rowIndex, 2).Value = categoryText;
					worksheet.Cell(rowIndex, 3).Value = question.Weight;
					worksheet.Cell(rowIndex, 4).Value = question.Statement;
					worksheet.Cell(rowIndex, 5).Value = question.Show;

					rowIndex++;
				}

				worksheet.Columns("A","B").AdjustToContents();

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Questions.xlsx");
				}
			}
		}
    }
}
