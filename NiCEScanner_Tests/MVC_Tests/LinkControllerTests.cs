using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class LinkControllerTests
{
    private readonly LinksController _linksController;
    private readonly ApplicationDbContext _context;

    public LinkControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _linksController = new LinksController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfLinks()
    {
        int linkCount = _context.Links.Count();

        var result = await _linksController.Index(null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Link>)((ViewResult)result).Model!;
        Assert.AreEqual(linkCount > 10 ? 10 : linkCount, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _linksController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenLinkDoesNotExist()
    {
        long? id = 9999;

        var result = await _linksController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenLinkExists()
    {
        var link = LinkFactory.CreateAndSubmit(_context);
    
        var result = await _linksController.Details(link.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Link model = (Link)((ViewResult)result).Model!;
        
        Assert.AreEqual(link.Id, model.Id);
    }
    
    [TestMethod]
    public void Create_ReturnsViewResult_WithCategoryId()
    {
        var result = _linksController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsTrue(viewResult.ViewData.ContainsKey("CategoryId"));
        Assert.IsInstanceOfType(viewResult.ViewData["CategoryId"], typeof(SelectList));
    }
    
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var linkForm = LinkFormFactory.Create();

        var result = await _linksController.Create(linkForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        
        Assert.IsTrue(_context.Links.Any(l => l.Name == linkForm.Name));
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var linkForm = LinkFormFactory.Create();
        var initialCount = _context.Links.Count();

        _linksController.ModelState.AddModelError("Error", "Some error");

        var result = await _linksController.Create(linkForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(linkForm, ((ViewResult)result).Model);
        
        var finalCount = _context.Links.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _linksController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenLinkDoesNotExist()
    {
        long? id = 999;

        var result = await _linksController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenLinkExists()
    {
        var link = LinkFactory.CreateAndSubmit(_context);

        var result = await _linksController.Edit(link.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(LinkForm));
        
        LinkForm model = (LinkForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(link.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchFormId()
    {
        var link = LinkFactory.CreateAndSubmit(_context);
        
        var form = LinkFormFactory.Create();
        form.Id = link.Id + 1;

        var result = await _linksController.Edit(link.Id, form);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var link = LinkFactory.CreateAndSubmit(_context);

        var form = LinkFormFactory.Create();
        form.Id = link.Id;

        var result = await _linksController.Edit(link.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(link.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.Name, link.Name);
        Assert.AreEqual(form.Href, link.Href);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var link = LinkFactory.CreateAndSubmit(_context);

        var form = LinkFormFactory.Create();
        form.Id = link.Id;
        _linksController.ModelState.AddModelError("Error", "Some error");

        var result = await _linksController.Edit(link.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
        
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _linksController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenLinkDoesNotExist()
    {
        long? id = 999;

        var result = await _linksController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenLinkExists()
    {
        var link = LinkFactory.CreateAndSubmit(_context);

        var result = await _linksController.Delete(link.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Link));
        var model = (Link)viewResult.ViewData.Model;
        Assert.AreEqual(link.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenLinkDoesNotExist()
    {
        long id = 999;

        var result = await _linksController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesCategoryAndReturnsRedirectToActionResult_WhenLinkExists()
    {
        var link = LinkFactory.CreateAndSubmit(_context);

        var result = await _linksController.DeleteConfirmed(link.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        link = await _context.Links.FindAsync(link.Id);
        Assert.IsNull(link);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}