using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class AdviceControllerTests
{
    private readonly AdvicesController _advicesController;
    private readonly ApplicationDbContext _context;

    public AdviceControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _advicesController = new AdvicesController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfAdvices()
    {
        int linkCount = _context.Links.Count();

        var result = await _advicesController.Index(null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Advice>)((ViewResult)result).Model!;
        Assert.AreEqual(linkCount > 10 ? 10 : linkCount, model.Count);        
    }
        
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _advicesController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenAdviceDoesNotExist()
    {
        long? id = 9999;

        var result = await _advicesController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenAdviceExists()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);
    
        var result = await _advicesController.Details(advice.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Advice model = (Advice)((ViewResult)result).Model!;
        
        Assert.AreEqual(advice.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WithQuestionId()
    {
        var result = await _advicesController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsTrue(viewResult.ViewData.ContainsKey("QuestionId"));
        Assert.IsInstanceOfType(viewResult.ViewData["QuestionId"], typeof(SelectList));
    }
    
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var adviceForm = AdviceFormFactory.Create();

        var result = await _advicesController.Create(adviceForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var adviceForm = AdviceFormFactory.Create();
        var initialCount = _context.Advices.Count();

        _advicesController.ModelState.AddModelError("Error", "Some error");

        var result = await _advicesController.Create(adviceForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(adviceForm, ((ViewResult)result).Model);
        
        var finalCount = _context.Advices.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _advicesController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenAdviceDoesNotExist()
    {
        long? id = 999;

        var result = await _advicesController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenAdviceExists()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);

        var result = await _advicesController.Edit(advice.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(AdviceForm));
        
        AdviceForm model = (AdviceForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(advice.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);
        
        var form = AdviceFormFactory.Create();
        form.Id = advice.Id;

        var result = await _advicesController.Edit(advice.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(advice.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.AdditionalLink, advice.AdditionalLink);
        Assert.AreEqual(form.AdditionalLinkName, advice.AdditionalLinkName);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);
        
        var form = AdviceFormFactory.Create();
        form.Id = advice.Id;
        _advicesController.ModelState.AddModelError("Error", "Some error");

        var result = await _advicesController.Edit(advice.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _advicesController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenAdviceDoesNotExist()
    {
        long? id = 999;

        var result = await _advicesController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenAdviceExists()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);

        var result = await _advicesController.Delete(advice.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Advice));
        var model = (Advice)viewResult.ViewData.Model;
        Assert.AreEqual(advice.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenAdviceDoesNotExist()
    {
        long id = 999;

        var result = await _advicesController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesCategoryAndReturnsRedirectToActionResult_WhenAdviceExists()
    {
        var advice = AdviceFactory.CreateAndSubmit(_context);

        var result = await _advicesController.DeleteConfirmed(advice.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        advice = await _context.Advices.FindAsync(advice.Id);
        Assert.IsNull(advice);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}