using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NiCeScanner.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

		public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}

		public IActionResult Index()
		{
			var users = _userManager.Users.Select(c => new UserRolesViewModel()
			{
				Id = c.Id,
				UserName = c.UserName,
				Roles = string.Join(",", _userManager.GetRolesAsync(c).Result)
			}).ToList();

			return View(users);
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
				Roles = userRoles.FirstOrDefault() // Set the current role of the user
			};

			// Create a list of SelectListItem for the dropdown
			ViewBag.RoleList = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

			return View(viewModel);
		}

		// POST: UserRolesView/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Roles")] UserRolesViewModel viewModel)
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

					// Update user roles
					await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
					await _userManager.AddToRoleAsync(user, viewModel.Roles);

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

	}
}
