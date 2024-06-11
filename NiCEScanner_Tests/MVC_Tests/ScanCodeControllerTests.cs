using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]

public class ScanCodeControllerTests
{
    private readonly ScanCodesController _scanCodeController;
    private readonly ApplicationDbContext _context;

    public ScanCodeControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _scanCodeController = new ScanCodesController(_context);
    }

    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfScanCodes()
    {
        int category = _context.ScanCodes.Count();

        var result = await _scanCodeController.Index(null, null, null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<ScanCode>)((ViewResult)result).Model!;
        Assert.AreEqual(category > 10 ? 10 : category, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scanCodeController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenScanCodeDoesNotExist()
    {
        long? id = 9999;

        var result = await _scanCodeController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenScanCodeExists()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);
    
        var result = await _scanCodeController.Details(scanCode.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        ScanCode model = (ScanCode)((ViewResult)result).Model!;
        
        Assert.AreEqual(scanCode.Id, model.Id);
    }
    
    [TestMethod]
    public void Create_ReturnsViewResult()
    {
        var result = _scanCodeController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
    
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var scanCodeForm = ScanCodeFormFactory.Create();

        var result = await _scanCodeController.Create(scanCodeForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        
        Assert.IsTrue(_context.ScanCodes.Any(c => c.Code == scanCodeForm.Code));
    }
    
        
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var scanCodeForm = ScanCodeFormFactory.Create();
        var initialCount = _context.ScanCodes.Count();

        _scanCodeController.ModelState.AddModelError("Error", "Some error");

        var result = await _scanCodeController.Create(scanCodeForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(scanCodeForm, ((ViewResult)result).Model);
        
        var finalCount = _context.ScanCodes.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
        
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scanCodeController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
        
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenScanCodeDoesNotExist()
    {
        long? id = 999;

        var result = await _scanCodeController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenScanCodeExists()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);

        var result = await _scanCodeController.Edit(scanCode.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(ScanCodeForm));
        
        ScanCodeForm model = (ScanCodeForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(scanCode.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchFormId()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);
        
        var form = ScanCodeFormFactory.Create();
        form.Id = scanCode.Id + 1;

        var result = await _scanCodeController.Edit(scanCode.Id, form);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);

        var form = ScanCodeFormFactory.Create();
        form.Id = scanCode.Id;

        var result = await _scanCodeController.Edit(scanCode.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(scanCode.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.Code, scanCode.Code);
        Assert.AreEqual(form.CanEdit, scanCode.CanEdit);
        Assert.AreEqual(form.ScanId, scanCode.ScanId);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);

        var form = ScanCodeFormFactory.Create();
        form.Id = scanCode.Id;
        _scanCodeController.ModelState.AddModelError("Error", "Some error");

        var result = await _scanCodeController.Edit(scanCode.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scanCodeController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenScanCodeDoesNotExist()
    {
        long? id = 999;

        var result = await _scanCodeController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenScanCodeExists()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);

        var result = await _scanCodeController.Delete(scanCode.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ScanCode));
        var model = (ScanCode)viewResult.ViewData.Model;
        Assert.AreEqual(scanCode.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenScanCodeDoesNotExist()
    {
        long id = 999;

        var result = await _scanCodeController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesScanCodeAndReturnsRedirectToActionResult_WhenScanCodeExists()
    {
        var scanCode = ScanCodeFactory.CreateAndSubmit(_context);

        var result = await _scanCodeController.DeleteConfirmed(scanCode.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        scanCode = await _context.ScanCodes.FindAsync(scanCode.Id);
        Assert.IsNull(scanCode);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}