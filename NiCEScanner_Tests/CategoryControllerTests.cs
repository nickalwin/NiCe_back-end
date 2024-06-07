using Microsoft.EntityFrameworkCore;
using NiCeScanner.Controllers;
using NiCeScanner.Data;

namespace NiCEScanner_Tests;

[TestClass]
public class CategoryControllerTests : IDisposable
{
    private readonly CategoryController _categoryController;
    private readonly ApplicationDbContext _context;

    public CategoryControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        
        //_categoryController = new CategoryController(new ApplicationDbContext(_options));
    }

    [TestMethod]
    public void TestMethod1()
    {
        Assert.IsTrue(_context.Users.Any());
        Assert.IsTrue(_context.Categories.Any());
        Assert.IsTrue(_context.Advices.Any());
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }
}
