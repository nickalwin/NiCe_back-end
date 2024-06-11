using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class ScanControllerTests
{
    private readonly ScansController _scansController;
    private readonly ApplicationDbContext _context;

    public ScanControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _scansController = new ScansController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfScans()
    {
        int scanCount = _context.Scans.Count();

        var result = await _scansController.Index(null, null, null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Scan>)((ViewResult)result).Model!;
        Assert.AreEqual(scanCount > 10 ? 10 : scanCount, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scansController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenScanDoesNotExist()
    {
        long? id = 9999;

        var result = await _scansController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenScanExists()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);
    
        var result = await _scansController.Details(scan.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Scan model = (Scan)((ViewResult)result).Model!;
        
        Assert.AreEqual(scan.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scansController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenScanDoesNotExist()
    {
        long? id = 999;

        var result = await _scansController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenScanExists()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);

        var result = await _scansController.Edit(scan.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(ScanForm));
        
        ScanForm model = (ScanForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(scan.ContactEmail, model.ContactEmail);
        Assert.AreEqual(scan.ContactName, model.ContactName);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchFormId()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);
        
        var form = ScanFormFactory.Create();
        form.Id = scan.Id + 1;

        var result = await _scansController.Edit(scan.Id, form);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);
        
        var form = ScanFormFactory.Create();
        form.Id = scan.Id;

        var result = await _scansController.Edit(scan.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(scan.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.ContactName, scan.ContactName);
        Assert.AreEqual(form.ContactEmail, scan.ContactEmail);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);
        
        var form = ScanFormFactory.Create();
        form.Id = scan.Id;

        _scansController.ModelState.AddModelError("Error", "Some error");

        var result = await _scansController.Edit(scan.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _scansController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenScanDoesNotExist()
    {
        long? id = 999;

        var result = await _scansController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenScanExists()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);

        var result = await _scansController.Delete(scan.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Scan));
        var model = (Scan)viewResult.ViewData.Model;
        Assert.AreEqual(scan.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenScanDoesNotExist()
    {
        long id = 999;

        var result = await _scansController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesScanAndReturnsRedirectToActionResult_WhenScanExists()
    {
        var scan = ScanFactory.CreateAndSubmit(_context);

        var result = await _scansController.DeleteConfirmed(scan.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        scan = await _context.Scans.FindAsync(scan.Id);
        Assert.IsNull(scan);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}