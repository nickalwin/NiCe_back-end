using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories.API;
using NiCeScanner.Controllers.API;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

namespace NiCEScanner_Tests.API_Tests;

[TestClass]
public class PdfTemplateControllerTests
{
    private readonly PdfTemplateController _pdfTemplateController;
    private readonly ApplicationDbContext _context;

    public PdfTemplateControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _pdfTemplateController = new PdfTemplateController(_context);
    }

    [TestMethod]
    public async Task GetPdf_ReturnsExpectedResult()
    {
        var actionResult = await _pdfTemplateController.GetPdf();
        
        Assert.IsNotNull(actionResult);
        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        
        Assert.IsNotNull(okResult);
        
        var pdfTemplateResource = okResult.Value as PdfTemplateResource;
        
        Assert.IsNotNull(pdfTemplateResource);
        
        var pdfTemplate = _context.PdfTemplates.FirstOrDefault();
        
        Assert.IsNotNull(pdfTemplate);
        
        var imageBase64Data = Convert.ToBase64String(pdfTemplate.ImageData);
        var imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

        Assert.AreEqual(pdfTemplate.Title, pdfTemplateResource.Title);
        Assert.AreEqual(pdfTemplate.Introduction, pdfTemplateResource.Introduction);
        Assert.AreEqual(imageDataURL, pdfTemplateResource.ImageData);
        Assert.AreEqual(pdfTemplate.BeforePlotText, pdfTemplateResource.BeforePlotText);
        Assert.AreEqual(pdfTemplate.AfterPlotText, pdfTemplateResource.AfterPlotText);
    }
        
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}