using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories;
using NiCEScanner_Tests.Factories.MVC;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.MVC_Tests;

[TestClass]
public class CategoryControllerTests
{
    private readonly CategoryController _categoryController;
    private readonly ApplicationDbContext _context;

    public CategoryControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _categoryController = new CategoryController(_context);
    }

    [TestMethod]
    public async Task Index_ReturnsViewResult_WithListOfCategories()
    {
        int categoryCount = _context.Categories.Count();

        var result = await _categoryController.Index(null, null, null, null, null, null, null, null);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);

        var model = (PaginatedList<Category>)((ViewResult)result).Model!;
        Assert.AreEqual(categoryCount > 10 ? 10 : categoryCount, model.Count);        
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _categoryController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsNotFoundResult_WhenCategoryDoesNotExist()
    {
        long? id = 9999;

        var result = await _categoryController.Details(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Details_ReturnsViewResult_WhenCategoryExists()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);
    
        var result = await _categoryController.Details(category.Id);
    
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsNotNull(((ViewResult)result).Model);
        
        Category model = (Category)((ViewResult)result).Model!;
        
        Assert.AreEqual(category.Id, model.Id);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult()
    {
        var result = await _categoryController.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
    
    [TestMethod]
    public async Task Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var categoryForm = CategoryFormFactory.Create();

        var result = await _categoryController.Create(categoryForm);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }
    
    [TestMethod]
    public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var categoryForm = CategoryFormFactory.Create();
        var initialCount = _context.Categories.Count();

        _categoryController.ModelState.AddModelError("Error", "Some error");

        var result = await _categoryController.Create(categoryForm);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.AreEqual(categoryForm, ((ViewResult)result).Model);
        
        var finalCount = _context.Categories.Count();
        Assert.AreEqual(initialCount, finalCount);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _categoryController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenCategoryDoesNotExist()
    {
        long? id = 999;

        var result = await _categoryController.Edit(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenCategoryExists()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);

        var result = await _categoryController.Edit(category.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(CategoryForm));
        
        CategoryForm model = (CategoryForm)((ViewResult)result).Model!;
        
        Assert.AreEqual(category.Id, model.Id);
    }

    [TestMethod]
    public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchFormId()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);
        
        var form = CategoryFormFactory.Create();
        form.Id = category.Id + 1;

        var result = await _categoryController.Edit(category.Id, form);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    
    [TestMethod]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);

        var form = CategoryFormFactory.Create();
        form.Id = category.Id;

        var result = await _categoryController.Edit(category.Id, form);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(category.Id, redirectToActionResult.RouteValues["id"]);
        Assert.AreEqual(form.Color, category.Color);
        Assert.AreEqual(form.Show, category.Show);
    }
    
    [TestMethod]
    public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);

        var form = CategoryFormFactory.Create();
        form.Id = category.Id;
        _categoryController.ModelState.AddModelError("Error", "Some error");

        var result = await _categoryController.Edit(category.Id, form);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.AreEqual(form, viewResult.ViewData.Model);
    }
    
    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
    {
        long? id = null;

        var result = await _categoryController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Delete_ReturnsNotFoundResult_WhenCategoryDoesNotExist()
    {
        long? id = 999;

        var result = await _categoryController.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Delete_ReturnsViewResult_WhenCategoryExists()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);

        var result = await _categoryController.Delete(category.Id);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Category));
        var model = (Category)viewResult.ViewData.Model;
        Assert.AreEqual(category.Id, model.Id);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenCategoryDoesNotExist()
    {
        long id = 999;

        var result = await _categoryController.DeleteConfirmed(id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }
    
    [TestMethod]
    public async Task DeleteConfirmed_DeletesCategoryAndReturnsRedirectToActionResult_WhenCategoryExists()
    {
        var category = CategoryFactory.CreateAndSubmit(_context);

        var result = await _categoryController.DeleteConfirmed(category.Id);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        var redirectToActionResult = (RedirectToActionResult)result;
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
        category = await _context.Categories.FindAsync(category.Id);
        Assert.IsNull(category);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}