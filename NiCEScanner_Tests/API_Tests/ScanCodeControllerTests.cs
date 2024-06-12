using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories.API;
using NiCeScanner.Controllers.API;
using NiCeScanner.Data;
using NiCeScanner.Models;
using NiCeScanner.Resources.API;

namespace NiCEScanner_Tests.API_Tests;

[TestClass]
public class ScanCodeControllerTests
{
    private readonly ScanCodeController _scanCodeController;
    private readonly ApplicationDbContext _context;

    public ScanCodeControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _scanCodeController = new ScanCodeController(_context);
    }

    [TestMethod]
    public async Task ValidateScanCode_ValidEditCode_ReturnsExpectedResult()
    {
        ScanComposite scanComposite = ScanCompositeFactory.CreateAndSubmit(_context);
        
        var actionResult = await _scanCodeController.ValidateScanCode(scanComposite.EditCode.Code.ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is OkObjectResult);
        
        var okResult = actionResult.Result as OkObjectResult;
        
        Assert.IsNotNull(okResult);
        
        var validatedScanCodeResource = okResult.Value as ValidatedScanCodeResource;
        
        Assert.IsNotNull(validatedScanCodeResource);
        
        Assert.AreEqual(scanComposite.Scan.Uuid, validatedScanCodeResource.Scan_uuid);
        Assert.AreEqual(scanComposite.EditCode.Code, validatedScanCodeResource.Scan_code);
        Assert.IsTrue(validatedScanCodeResource.Editable);
    }
    
    [TestMethod]
    public async Task ValidateScanCode_InvalidEditCode_ReturnsNotFound()
    {
        var actionResult = await _scanCodeController.ValidateScanCode(Guid.NewGuid().ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan code not valid!", notFoundResult.Value);
    }
    
    [TestMethod]
    public async Task ValidateScanCode_NotValidCode_ReturnsNotFound()
    {
        ScanCode code = new ScanCode()
        {
            Code = Guid.NewGuid(),
            ScanId = 1
        };
        var actionResult = await _scanCodeController.ValidateScanCode(code.Code.ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan code not valid!", notFoundResult.Value);
    }
    
    [TestMethod]
    public async Task ValidateScanCode_ValidViewCode_ReturnsExpectedResult()
    {
        ScanComposite scanComposite = ScanCompositeFactory.CreateAndSubmit(_context);
        
        var actionResult = await _scanCodeController.ValidateScanCode(scanComposite.ViewCode.Code.ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is OkObjectResult);
        
        var okResult = actionResult.Result as OkObjectResult;
        
        Assert.IsNotNull(okResult);
        
        var validatedScanCodeResource = okResult.Value as ValidatedScanCodeResource;
        
        Assert.IsNotNull(validatedScanCodeResource);
        
        Assert.AreEqual(scanComposite.Scan.Uuid, validatedScanCodeResource.Scan_uuid);
        Assert.AreEqual(scanComposite.ViewCode.Code, validatedScanCodeResource.Scan_code);
        Assert.IsFalse(validatedScanCodeResource.Editable);
    }
    
    [TestMethod]
    public async Task ValidateScanCode_InvalidViewCode_ReturnsNotFound()
    {
        var actionResult = await _scanCodeController.ValidateScanCode(Guid.NewGuid().ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan code not valid!", notFoundResult.Value);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}