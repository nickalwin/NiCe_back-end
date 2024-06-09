using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	public class PaginatedViewModel<T>
	{
		public List<T> Items { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }

		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
	}
	
	[Authorize(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;
		private readonly ILogger<UsersController> _logger;
		private readonly IEmailSender _emailSender;

		public UsersController(
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbContext context,
			ILogger<UsersController> logger,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
			_logger = logger;
			_emailSender = emailSender;
		}
		
		public async Task<IActionResult> Index(
			string sortOrder,
			string sortOrderEmail,
			string currentFilter,
			string searchString,
			int? pageNumber	
		) {
			ViewData["Title"] = "Users";
			ViewData["CurrentSort"] = sortOrder;
			ViewData["CurrentFilter"] = searchString;
			
			ViewData["EmailSortParm"] = sortOrderEmail switch
			{
				"Email" => "Email_desc",
				"Email_desc" => "",
				_ => "Email"
			};
			ViewData["SortOrderEmail"] = sortOrderEmail;
			
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["SearchString"] = searchString;
			
			var users = from u in _userManager.Users
				select u;
			
			if (!string.IsNullOrEmpty(searchString))
			{
				users = users.Where(s => s.UserName.Contains(searchString));
			}
			
			switch (sortOrderEmail)
			{
				case "Email_desc":
					users = users.OrderByDescending(s => s.UserName);
					break;
				case "Email":
					users = users.OrderBy(s => s.UserName);
					break;
				default:
					users = users.OrderBy(s => s.Id);
					break;
			}
			
			var roleRequests = _context.UserRoleRequests.Where(r => r.Status == null).ToList();

			var userRolesViewModel = new List<UserRolesViewModel>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				var pendingRequest = roleRequests.FirstOrDefault(r => r.User == user.UserName);

				userRolesViewModel.Add(new UserRolesViewModel
				{
					Id = user.Id,
					UserName = user.UserName,
					Roles = string.Join(", ", roles),
					IsPendingRoleRequest = pendingRequest != null,
					RequestedRole = pendingRequest?.Role,
					RequestReason = pendingRequest?.Reason,
				});
			}
    
			int pn = pageNumber ?? 1;
			
			int pageSize = 10;
			
			var paginatedUsers = new PaginatedViewModel<UserRolesViewModel>
			{
				Items = userRolesViewModel
					.Skip((pn - 1) * pageSize)
					.Take(pageSize)
					.ToList(),
				PageNumber = pn,
				TotalPages = (int)Math.Ceiling(userRolesViewModel.Count / (double)pageSize)
			};
			
			return View(paginatedUsers);
		}

		// GET: UserRolesView/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			var roles = await _roleManager.Roles.ToListAsync(); // Fetch roles from the database
			var userRoles = await _userManager.GetRolesAsync(user);

			var viewModel = new UserRolesViewModel
			{
				Id = id,
				UserName = user.UserName,
				Roles = string.Join(", ", userRoles)
			};

			ViewBag.RoleList = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

			return View(viewModel);
		}

		// POST: UserRolesView/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, UserRolesViewModel viewModel)
		{
			if (id != viewModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByIdAsync(id.ToString());
					if (user == null)
					{
						return NotFound();
					}

					// Remove all existing roles of the user
					var userRoles = await _userManager.GetRolesAsync(user);
					await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());

					// Add selected roles to the user
					await _userManager.AddToRolesAsync(user, viewModel.Roles.Split(',').Select(r => r.Trim()));

					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
			}
			return View(viewModel);
		}



		// GET: UserRolesView/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			var viewModel = new UserRolesViewModel
			{
				Id = id,
				UserName = user.UserName,
				Roles = string.Join(", ", _userManager.GetRolesAsync(user).Result)
			};

			return View(viewModel);
		}

		// POST: UserRolesView/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			await _userManager.DeleteAsync(user);
			return RedirectToAction(nameof(Index));
		}

		// GET: UserRolesView/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			var viewModel = new UserRolesViewModel
			{
				Id = id,
				UserName = user.UserName,
				Roles = string.Join(", ", _userManager.GetRolesAsync(user).Result)
			};

			return View(viewModel);
		}


		[HttpPost]
		public async Task<IActionResult> HandleRoleRequest(string userId, bool isApproved)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound($"Unable to find user with ID '{userId}'.");
			}

			var pendingRequest = _context.UserRoleRequests.FirstOrDefault(r => r.User == user.UserName && r.Status == null);
			if (pendingRequest != null)
			{
				pendingRequest.Status = isApproved;
				pendingRequest.HandledBy = User.Identity.Name;
				_context.Update(pendingRequest);
				await _context.SaveChangesAsync();

				if (isApproved)
				{
					await _userManager.AddToRoleAsync(user, pendingRequest.Role);
				}

				await _emailSender.SendEmailAsync(user.Email, "Role Request Update", isApproved ? "Your role request has been approved." : "Your role request has been denied.");
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
