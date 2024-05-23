using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class AdvicesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdvicesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Advices
		public async Task<IActionResult> Index()
		{
			return View(await _context.Advices.ToListAsync());
		}

		// GET: Advices/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices
				.FirstOrDefaultAsync(m => m.Id == id);
			if (advice == null)
			{
				return NotFound();
			}

			return View(advice);
		}

		// GET: Advices/Create
		public IActionResult Create()
		{
			ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");

			return View();
		}

		// POST: Advices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Data,Condition,QuestionId,AdditionalLink,AdditionalLinkName")] AdviceForm form)
		{
			var advice = new Advice
			{
				Data = form.Data,
				Condition = form.Condition,
				QuestionId = form.QuestionId,
				AdditionalLink = form.AdditionalLink,
				AdditionalLinkName = form.AdditionalLinkName,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};

			if (ModelState.IsValid)
			{
				_context.Add(advice);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(form);
		}

		// GET: Advices/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices.FindAsync(id);
			if (advice == null)
			{
				return NotFound();
			}

			ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");

			return View(new AdviceForm
			{
				Data = advice.Data,
				Condition = advice.Condition,
				QuestionId = advice.QuestionId,
				AdditionalLink = advice.AdditionalLink,
				AdditionalLinkName = advice.AdditionalLinkName
			});
		}

		// POST: Advices/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Data,Condition,QuestionId,AdditionalLink,AdditionalLinkName")] AdviceForm form)
		{
			if (ModelState.IsValid)
			{
				var advice = await _context.Advices.FindAsync(id);
				if (advice == null)
				{
					return NotFound();
				}
				advice.Data = form.Data;
				advice.Condition = form.Condition;
				advice.QuestionId = form.QuestionId;
				advice.AdditionalLink = form.AdditionalLink;
				advice.AdditionalLinkName = form.AdditionalLinkName;
				advice.UpdatedAt = DateTime.Now;

				try
				{
					_context.Update(advice);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AdviceExists(advice.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(form);
		}

		// GET: Advices/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var advice = await _context.Advices
				.FirstOrDefaultAsync(m => m.Id == id);
			if (advice == null)
			{
				return NotFound();
			}

			return View(advice);
		}

		// POST: Advices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var advice = await _context.Advices.FindAsync(id);
			if (advice != null)
			{
				_context.Advices.Remove(advice);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AdviceExists(long id)
		{
			return _context.Advices.Any(e => e.Id == id);
		}
	}
}
