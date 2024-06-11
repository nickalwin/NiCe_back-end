using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCEScanner_Tests.Factories.MVC;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Views;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class SectorControllerTests
{
    private readonly SectorsController _sectorController;
    private readonly ApplicationDbContext _context;

    public SectorControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _sectorController = new SectorsController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfSectors()
    {
        int sectorCount = _context.Sectors.Count();

        var result = await _sectorController.Index(null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Sector>)((ViewResult)result).Model!;
        Assert.AreEqual(sectorCount > 10 ? 10 : sectorCount, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _sectorController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenSectorDoesNotExist()
    {
        long? id = 9999;

        var result = await _sectorController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenSectorExists()
    {
        var sector = SectorFactory.CreateAndSubmit(_context);
    
        var result = await _sectorController.Details(sector.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Sector model = (Sector)((ViewResult)result).Model!;
        
        Assert.AreEqual(sector.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult()
    {
        var result = await _sectorController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
        
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var sectorForm = SectorFormFactory.Create();

        var result = await _sectorController.Create(sectorForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var sectorForm = SectorFormFactory.Create();
        var initialCount = _context.Sectors.Count();

        _sectorController.ModelState.AddModelError("Error", "Some error");

        var result = await _sectorController.Create(sectorForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(sectorForm, ((ViewResult)result).Model);
        
        var finalCount = _context.Sectors.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
        
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _sectorController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenSectorDoesNotExist()
    {
        long? id = 999;

        var result = await _sectorController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchFormId()
    {
        var sector = SectorFactory.CreateAndSubmit(_context);
        
        var form = SectorFormFactory.Create();
        form.Id = sector.Id + 1;

        var result = await _sectorController.Edit(sector.Id, form);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
        
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var sector = SectorFactory.CreateAndSubmit(_context);

        var form = SectorFormFactory.Create();
        form.Id = sector.Id;

        var result = await _sectorController.Edit(sector.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(sector.Id, redirectToActionResult.RouteValues["id"]);
    }
        
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var sector = SectorFactory.CreateAndSubmit(_context);

        var form = SectorFormFactory.Create();
        form.Id = sector.Id;
        _sectorController.ModelState.AddModelError("Error", "Some error");

        var result = await _sectorController.Edit(sector.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}