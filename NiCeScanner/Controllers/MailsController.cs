using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCeScanner.Controllers
{
	[Authorize(Policy = "RequireManagerRole")]
	public class MailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mails
        public async Task<IActionResult> Index(
	        string sortOrder,
	        string sortOrderFirstName,
	        string sortOrderLastName,
	        string sortOrderEmail,
	        string sortOrderPhone,
	        string sortOrderSubject,
	        string sortOrderMessage,
	        string currentFilter,
	        string searchString,
	        int? pageNumber = 1  
	    ) {
	        ViewData["Title"] = "Mails";
	        
	        ViewData["FirstNameSortParam"] = sortOrderFirstName switch
	        {
		        "FirstName_desc" => "FirstName",
		        "FirstName" => "",
		        _ => "FirstName_desc"
	        };
	        ViewData["SortOrderFirstName"] = sortOrderFirstName;
	        
	        ViewData["LastNameSortParam"] = sortOrderLastName switch
	        {
		        "LastName_desc" => "LastName",
		        "LastName" => "",
		        _ => "LastName_desc"
	        };
	        ViewData["SortOrderLastName"] = sortOrderLastName;
	        
	        ViewData["EmailSortParam"] = sortOrderEmail switch
	        {
		        "Email_desc" => "Email",
		        "Email" => "",
		        _ => "Email_desc"
	        };
	        ViewData["SortOrderEmail"] = sortOrderEmail;
	        
	        ViewData["PhoneSortParam"] = sortOrderPhone switch
	        {
		        "Phone_desc" => "Phone",
		        "Phone" => "",
		        _ => "Phone_desc"
	        };
	        ViewData["SortOrderPhone"] = sortOrderPhone;
	        
	        ViewData["SubjectSortParam"] = sortOrderSubject switch
	        {
		        "Subject_desc" => "Subject",
		        "Subject" => "",
		        _ => "Subject_desc"
	        };
	        ViewData["SortOrderSubject"] = sortOrderSubject;
	        
	        ViewData["MessageSortParam"] = sortOrderMessage switch
	        {
		        "Message_desc" => "Message",
		        "Message" => "",
		        _ => "Message_desc"
	        };
	        ViewData["SortOrderMessage"] = sortOrderMessage;
	        
	        if (searchString != null)
	        {
		        pageNumber = 1;
	        }
	        else
	        {
		        searchString = currentFilter;
	        }

	        ViewData["CurrentFilter"] = searchString;
	        
	        var mails = from m in _context.Mail select m;
	        
	        if (!string.IsNullOrEmpty(searchString))
	        {
		        mails = mails.Where(s => s.Email.Contains(searchString) || s.FirsName.Contains(searchString) || s.LastName.Contains(searchString) || s.Subject.Contains(searchString) || s.Message.Contains(searchString));
	        }

	        switch (sortOrderFirstName)
	        {
		        case "FirstName_desc":
			        mails = mails.OrderByDescending(s => s.FirsName);
			        break;
		        case "FirstName":
			        mails = mails.OrderBy(s => s.FirsName);
			        break;
	        }
	        
	        switch (sortOrderLastName)
	        {
		        case "LastName_desc":
			        mails = mails.OrderByDescending(s => s.LastName);
			        break;
		        case "LastName":
			        mails = mails.OrderBy(s => s.LastName);
			        break;
	        }
	        
	        switch (sortOrderEmail)
	        {
		        case "Email_desc":
			        mails = mails.OrderByDescending(s => s.Email);
			        break;
		        case "Email":
			        mails = mails.OrderBy(s => s.Email);
			        break;
	        }
	        
	        switch (sortOrderPhone)
	        {
		        case "Phone_desc":
			        mails = mails.OrderByDescending(s => s.Phone);
			        break;
		        case "Phone":
			        mails = mails.OrderBy(s => s.Phone);
			        break;
	        }
	        
	        switch (sortOrderSubject)
	        {
		        case "Subject_desc":
			        mails = mails.OrderByDescending(s => s.Subject);
			        break;
		        case "Subject":
			        mails = mails.OrderBy(s => s.Subject);
			        break;
	        }
	        
	        switch (sortOrderMessage)
	        {
		        case "Message_desc":
			        mails = mails.OrderByDescending(s => s.Message);
			        break;
		        case "Message":
			        mails = mails.OrderBy(s => s.Message);
			        break;
	        }

	        int pageSize = 10;
	        var paginatedList = await PaginatedList<Mail>.CreateAsync(mails.AsNoTracking(), pageNumber ?? 1, pageSize);
	        
            return View(paginatedList);
        }

        // GET: Mails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mail = await _context.Mail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }
        
        // GET: Mails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mail = await _context.Mail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }

        // POST: Mails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mail = await _context.Mail.FindAsync(id);
            if (mail != null)
            {
                _context.Mail.Remove(mail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailExists(int id)
        {
            return _context.Mail.Any(e => e.Id == id);
        }
    }
}
