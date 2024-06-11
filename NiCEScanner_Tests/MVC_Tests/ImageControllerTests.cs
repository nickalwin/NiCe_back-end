using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class ImageControllerTests
{
    private readonly ImageController _imageController;
    private readonly ApplicationDbContext _context;

    public ImageControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _imageController = new ImageController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfCategories()
    {
        int categoryCount = _context.Images.Count();

        var result = await _imageController.Index(null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<ImageModel>)((ViewResult)result).Model!;
        Assert.AreEqual(categoryCount > 15 ? 15 : categoryCount, model.Count);        
    }

    [TestMethod]
    public void Upload_ReturnsViewResult()
    {
        var result = _imageController.Upload();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public async Task Upload_ValidFile_RedirectsToIndex()
    {
        var image = ImageFactory.Create();

        var bytes = image.ImageData;
        var file = new FormFile(new System.IO.MemoryStream(bytes), 0, bytes.Length, "file", "image.jpg");
        
        var result = await _imageController.Upload(file);
        
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }
    
    [TestMethod]
    public async Task Upload_NullFile_ReturnsContentResult()
    {
        IFormFile file = null;

        var result = await _imageController.Upload(file);

        Assert.IsInstanceOfType(result, typeof(ContentResult));
        Assert.AreEqual("file not selected", ((ContentResult)result).Content);
    }

    [TestMethod]
    public async Task Upload_EmptyFile_ReturnsContentResult()
    {
        var file = new FormFile(new MemoryStream(), 0, 0, "file", "image.jpg");

        var result = await _imageController.Upload(file);

        Assert.IsInstanceOfType(result, typeof(ContentResult));
        Assert.AreEqual("file not selected", ((ContentResult)result).Content);
    }
    
    [TestMethod]
    public async Task Display_ImageExists_ReturnsFileResult()
    {
        var image = ImageFactory.CreateAndSubmit(_context);

        var result = await _imageController.Display(image.Id);

        Assert.IsInstanceOfType(result, typeof(FileContentResult));
    }

    [TestMethod]
    public async Task Display_ImageDoesNotExist_ReturnsNotFoundResult()
    {
        long nonExistentId = 12345;

        var result = await _imageController.Display(nonExistentId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ImageExists_ReturnsViewResult()
    {
        var image = ImageFactory.CreateAndSubmit(_context);

        var result = await _imageController.Details(image.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(image, ((ViewResult)result).Model);
    }

    [TestMethod]
    public async Task Details_ImageDoesNotExist_ReturnsNotFoundResult()
    {
        long nonExistentId = 12345;

        var result = await _imageController.Details(nonExistentId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Delete_ImageExistsWithQuestions_RedirectsToIndex()
    {
        var image = ImageFactory.CreateAndSubmit(_context);
        var question = QuestionFactory.CreateAndSubmit(_context);
        question.ImageId = image.Id;
        await _context.SaveChangesAsync();

        var result = await _imageController.Delete(image.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        Assert.IsNull(_context.Questions.Find(question.Id).ImageId);
        Assert.IsNull(_context.Images.Find(image.Id));
    }

    [TestMethod]
    public async Task Delete_ImageExistsWithoutQuestions_RedirectsToIndex()
    {
        var image = ImageFactory.CreateAndSubmit(_context);

        var result = await _imageController.Delete(image.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        Assert.IsNull(_context.Images.Find(image.Id));
    }

    [TestMethod]
    public async Task Delete_ImageDoesNotExist_ReturnsNotFoundResult()
    {
        long nonExistentId = 12345;

        var result = await _imageController.Delete(nonExistentId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}