using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories.API;
using NiCeScanner.Controllers.API;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

namespace NiCEScanner_Tests.API_Tests;

[TestClass]
public class QuestionControllerTests
{
    private readonly QuestionController _questionController;
    private readonly ApplicationDbContext _context;

    public QuestionControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _questionController = new QuestionController(_context);
    }
    
    [TestMethod]
    public async Task GetQuestions_ReturnsExpectedResult()
    {
        var actionResult = await _questionController.GetQuestions();
        
        Assert.IsNotNull(actionResult);
        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        
        Assert.IsNotNull(okResult);
        
        var questions = okResult.Value as List<QuestionResource>;
        
        Assert.IsNotNull(questions);
        
        var question = _context.Questions.FirstOrDefault();
        
        Assert.IsNotNull(question);
        
        var imageBase64Data = Convert.ToBase64String(question.Image.ImageData);
        var imageDataURL = string.Format("data:image/jpeg;base64,{0}", imageBase64Data);

        Assert.AreEqual(question.Uuid, questions[0].Uuid);
        Assert.AreEqual(question.Data, questions[0].Data);
        Assert.AreEqual(question.Category.Uuid, questions[0].Category_uuid);
        Assert.AreEqual(question.Category.Data, questions[0].Category_data);
        Assert.AreEqual(question.Category.Color, questions[0].Category_color);
        Assert.AreEqual(question.Statement, questions[0].Statement);
        Assert.AreEqual(imageDataURL, questions[0].Image_data);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}