﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;

namespace NiCeScanner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : Controller
	{
		private readonly ApplicationDbContext _context;

		public QuestionController(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
