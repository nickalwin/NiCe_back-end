using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NiCeScanner.Data;
using NiCeScanner.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NiCeScanner.Areas.Identity.Pages.Account.Manage
{
	public class RoleRequestModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ILogger<RoleRequestModel> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		private readonly ApplicationDbContext _context;

		public RoleRequestModel(
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			ILogger<RoleRequestModel> logger,
			RoleManager<IdentityRole> roleManager,
			IEmailSender emailSender,
			ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_roleManager = roleManager;
			_emailSender = emailSender;
			_context = context;

			Roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string Username { get; set; } 
		public List<SelectListItem> Roles { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			Username = user.UserName; 

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var userRoles = await _userManager.GetRolesAsync(user);
			if (userRoles.Contains(Input.Role))
			{
				TempData["StatusMessage"] = "You already have this role.";
				return RedirectToPage();
			}

			var existingRequest = _context.UserRoleRequests
				.FirstOrDefault(r => r.User == user.UserName && r.Role == Input.Role && r.Status == null);
			if (existingRequest != null)
			{
				TempData["StatusMessage"] = "There is already a pending request for this role.";
				return RedirectToPage();
			}

			var roleRequest = new UserRoleRequests
			{
				Id = Guid.NewGuid().ToString(),
				User = user.UserName,
				Role = Input.Role,
				RequestedOn = DateTime.UtcNow,
				Reason = Input.Reason,
				Status = null
			};

			_context.UserRoleRequests.Add(roleRequest);
			await _context.SaveChangesAsync();

			var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
			var callbackUrl = Url.Action("Index", "Users", new { }, protocol: Request.Scheme);

			foreach (var adminUser in adminUsers)
			{
				await _emailSender.SendEmailAsync(adminUser.Email, "Role Request",
					$"A role has been requested by {user.UserName} <br>" +
					$"Reason: {Input.Reason} <br><br>" +
					$"Please review the request: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>");
			}

			await _signInManager.RefreshSignInAsync(user);
			TempData["StatusMessage"] = "Your role has been requested";

			return RedirectToPage();
		}


		public class InputModel
		{
			[Required]
			[Display(Name = "Role")]
			public string Role { get; set; }

			[Required]
			[Display(Name = "Reason")]
			public string Reason { get; set; }
		}
	}
}
