using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiCEScanner_Tests.Factories;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class QuestionControllerTests
{
    private readonly QuestionsController _questionsController;
    private readonly ApplicationDbContext _context;

    public QuestionControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _questionsController = new QuestionsController(_context);
    }
    
    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfQuestions()
    {
        int linkCount = _context.Links.Count();

        var result = await _questionsController.Index(null, null, null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Question>)((ViewResult)result).Model!;
        Assert.AreEqual(linkCount > 10 ? 10 : linkCount, model.Count);        
    }
    
    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _questionsController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenQuestionDoesNotExist()
    {
        long? id = 9999;

        var result = await _questionsController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenQuestionExists()
    {
        var question = QuestionFactory.CreateAndSubmit(_context);
    
        var result = await _questionsController.Details(question.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Question model = (Question)((ViewResult)result).Model!;
        
        Assert.AreEqual(question.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WithCategoryId()
    {
        var result = await _questionsController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsTrue(viewResult.ViewData.ContainsKey("CategoryId"));
        Assert.IsInstanceOfType(viewResult.ViewData["CategoryId"], typeof(SelectList));
    }
    
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var questionForm = QuestionFormFactory.Create();

        var result = await _questionsController.Create(questionForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var questionForm = QuestionFormFactory.Create();
        var initialCount = _context.Questions.Count();

        _questionsController.ModelState.AddModelError("Error", "Some error");

        var result = await _questionsController.Create(questionForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(questionForm, ((ViewResult)result).Model);
        
        var finalCount = _context.Questions.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _questionsController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenQuestionDoesNotExist()
    {
        long? id = 999;

        var result = await _questionsController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenQuestionExists()
    {
        var question = QuestionFactory.CreateAndSubmit(_context);

        var result = await _questionsController.Edit(question.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(QuestionForm));
        
        QuestionForm model = (QuestionForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(question.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var question = QuestionFactory.CreateAndSubmit(_context);
        var form = QuestionFormFactory.Create();        
        form.Id = question.Id;

        var result = await _questionsController.Edit(question.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(question.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.CategoryId, question.CategoryId);
        Assert.AreEqual(form.Weight, question.Weight);
        Assert.AreEqual(form.Statement, question.Statement);
        Assert.AreEqual(form.Show, question.Show);
        Assert.AreEqual(form.ImageId, question.ImageId);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var question = QuestionFactory.CreateAndSubmit(_context);
        var form = QuestionFormFactory.Create();        
        form.Id = question.Id;
        
        _questionsController.ModelState.AddModelError("Error", "Some error");

        var result = await _questionsController.Edit(question.Id, form);

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