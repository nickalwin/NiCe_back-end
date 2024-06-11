using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCEScanner_Tests.Factories.MVC;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class MailControllerTests
{
    private readonly MailsController _mailsController;
    private readonly ApplicationDbContext _context;

    public MailControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _mailsController = new MailsController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfMails()
    {
        int mailCount = _context.Mail.Count();

        var result = await _mailsController.Index(null, null, null, null, null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Mail>)((ViewResult)result).Model!;
        Assert.AreEqual(mailCount > 10 ? 10 : mailCount, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _mailsController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenMailDoesNotExist()
    {
        long? id = 9999;

        var result = await _mailsController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenMailExists()
    {
        var mail = MailFactory.CreateAndSubmit(_context);
    
        var result = await _mailsController.Details(mail.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Mail model = (Mail)((ViewResult)result).Model!;
        
        Assert.AreEqual(mail.Id, model.Id);
    }
        
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _mailsController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenMailDoesNotExist()
    {
        long? id = 999;

        var result = await _mailsController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenCategoryExists()
    {
        var mail = MailFactory.CreateAndSubmit(_context);

        var result = await _mailsController.Delete(mail.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Mail));
        var model = (Mail)viewResult.ViewData.Model;
        Assert.AreEqual(mail.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenMailDoesNotExist()
    {
        int id = 999;

        var result = await _mailsController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesCategoryAndReturnsRedirectToActionResult_WhenCategoryExists()
    {
        var mail = MailFactory.CreateAndSubmit(_context);

        var result = await _mailsController.DeleteConfirmed(mail.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        mail = await _context.Mail.FindAsync(mail.Id);
        Assert.IsNull(mail);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}