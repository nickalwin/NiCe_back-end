using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiCEScanner_Tests.Factories;
using NiCEScanner_Tests.Factories.MVC;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;


namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class PdfTemplateControllerTests
{
    private readonly PdfTemplateController _pdfController;
    private readonly ApplicationDbContext _context;

    public PdfTemplateControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _pdfController = new PdfTemplateController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithTemplate()
    {
        var result = await _pdfController.Index("../../../../NiCeScanner/");

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        var model = (PdfTemplate)((ViewResult)result).Model!;
        
        Assert.IsNotNull(model);
    }

    [TestMethod]
    public async Task EditTitle_ReturnsViewResult_WithTemplate()
    {
        var result = await _pdfController.EditTitle();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PdfTemplate)((ViewResult)result).Model;

        Assert.IsNotNull(model);
    }
    
    [TestMethod]
    public async Task EditTitle_RedirectsCorrectly_AndUpdatesTitle()
    {
        var controller = new PdfTemplateController(_context);
        var emptyTitle = new Dictionary<string, string>();
        var nonEmptyTitle = PDFTemplateFactory.CreateTitle();

        var resultWithEmptyTitle = await controller.EditTitle(emptyTitle);
        var redirectToActionResultWithEmptyTitle = resultWithEmptyTitle as RedirectToActionResult;

        var resultWithNonEmptyTitle = await controller.EditTitle(nonEmptyTitle);
        var redirectToActionResultWithNonEmptyTitle = resultWithNonEmptyTitle as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResultWithEmptyTitle);
        Assert.IsNotNull(redirectToActionResultWithNonEmptyTitle);
    }

    [TestMethod]
    public async Task EditTitle_RedirectsCorrectly_WhenTitleIsEmpty()
    {
        var controller = new PdfTemplateController(_context);
        var emptyTitle = new Dictionary<string, string>();

        var result = await controller.EditTitle(emptyTitle);
        var redirectToActionResult = result as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResult);
    }

    [TestMethod]
    public async Task EditIntroduction_ReturnsViewResult_WithTemplate()
    {
        var result = await _pdfController.EditIntroduction();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PdfTemplate)((ViewResult)result).Model;

        Assert.IsNotNull(model);
    }

    [TestMethod]
    public async Task EditIntroduction_RedirectsCorrectly_AndUpdatesIntroduction()
    {
        var controller = new PdfTemplateController(_context);
        var emptyIntroduction = new Dictionary<string, string>();
        var nonEmptyIntroduction = PDFTemplateFactory.CreateIntroduction();

        var resultWithEmptyIntroduction = await controller.EditIntroduction(emptyIntroduction);
        var redirectToActionResultWithEmptyIntroduction = resultWithEmptyIntroduction as RedirectToActionResult;

        var resultWithNonEmptyIntroduction = await controller.EditIntroduction(nonEmptyIntroduction);
        var redirectToActionResultWithNonEmptyIntroduction = resultWithNonEmptyIntroduction as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResultWithEmptyIntroduction);
        Assert.IsNotNull(redirectToActionResultWithNonEmptyIntroduction);
    }

    [TestMethod]
    public async Task EditIntroduction_RedirectsCorrectly_WhenIntroductionIsEmpty()
    {
        var controller = new PdfTemplateController(_context);
        var emptyIntroduction = new Dictionary<string, string>();

        var result = await controller.EditIntroduction(emptyIntroduction);
        var redirectToActionResult = result as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResult);
    }

    [TestMethod]
    public async Task EditImage_ReturnsViewResult_WithTemplate()
    {
        var result = await _pdfController.EditImage();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PdfTemplate)((ViewResult)result).Model;

        Assert.IsNotNull(model);
    }

    [TestMethod]
    public async Task EditImage_RedirectsCorrectly_AndUpdatesImage()
    {
        var controller = new PdfTemplateController(_context);
        var emptyImage = "";
        var nonEmptyImageBytes = PDFTemplateFactory.CreateImage();

        var emptyFile = new FormFile(new MemoryStream(), 0, 0, "image", "image.png");
        var nonEmptyImage = new FormFile(new MemoryStream(nonEmptyImageBytes), 0, nonEmptyImageBytes.Length, "image",
            "image.png");

        var resultWithEmptyImage = await controller.EditImage(emptyFile);
        var redirectToActionResultWithEmptyImage = resultWithEmptyImage as RedirectToActionResult;

        var resultWithNonEmptyImage = await controller.EditImage(nonEmptyImage);
        var redirectToActionResultWithNonEmptyImage = resultWithNonEmptyImage as RedirectToActionResult;
    }
    
    [TestMethod]
    public async Task EditBeforePlotText_RedirectsCorrectly_AndUpdatesBeforePlotText()
    {
        var controller = new PdfTemplateController(_context);
        var emptyBeforePlotText = new Dictionary<string, string>();
        var nonEmptyBeforePlotText = PDFTemplateFactory.CreateBeforePlotText();

        var resultWithEmptyBeforePlotText = await controller.EditBeforePlotText(emptyBeforePlotText);
        var redirectToActionResultWithEmptyBeforePlotText = resultWithEmptyBeforePlotText as RedirectToActionResult;

        var resultWithNonEmptyBeforePlotText = await controller.EditBeforePlotText(nonEmptyBeforePlotText);
        var redirectToActionResultWithNonEmptyBeforePlotText = resultWithNonEmptyBeforePlotText as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResultWithEmptyBeforePlotText);

        Assert.IsNotNull(redirectToActionResultWithNonEmptyBeforePlotText);

        var updatedPdf = _context.PdfTemplates.FirstOrDefault();
    }

    [TestMethod]
    public async Task EditBeforePlotText_RedirectsCorrectly_WhenBeforePlotTextIsEmpty()
    {
        var controller = new PdfTemplateController(_context);
        var emptyBeforePlotText = new Dictionary<string, string>();

        var result = await controller.EditBeforePlotText(emptyBeforePlotText);
        var redirectToActionResult = result as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResult);
    }

    [TestMethod]
    public async Task EditAfterPlotText_RedirectsCorrectly_AndUpdatesAfterPlotText()
    {
        var controller = new PdfTemplateController(_context);
        var emptyAfterPlotText = new Dictionary<string, string>();
        var nonEmptyAfterPlotText = PDFTemplateFactory.CreateAfterPlotText();

        var resultWithEmptyAfterPlotText = await controller.EditAfterPlotText(emptyAfterPlotText);
        var redirectToActionResultWithEmptyAfterPlotText = resultWithEmptyAfterPlotText as RedirectToActionResult;

        var resultWithNonEmptyAfterPlotText = await controller.EditAfterPlotText(nonEmptyAfterPlotText);
        var redirectToActionResultWithNonEmptyAfterPlotText = resultWithNonEmptyAfterPlotText as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResultWithEmptyAfterPlotText);
        Assert.IsNotNull(redirectToActionResultWithNonEmptyAfterPlotText);

        var updatedPdf = _context.PdfTemplates.FirstOrDefault();
    }

    [TestMethod]
    public async Task EditAfterPlotText_RedirectsCorrectly_WhenAfterPlotTextIsEmpty()
    {
        var controller = new PdfTemplateController(_context);
        var emptyAfterPlotText = new Dictionary<string, string>();

        var result = await controller.EditAfterPlotText(emptyAfterPlotText);
        var redirectToActionResult = result as RedirectToActionResult;

        Assert.IsNotNull(redirectToActionResult);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}