using Microsoft.AspNetCore.Mvc;
using NiCEScanner_Tests.Factories.API;
using NiCeScanner.Controllers.API;
using NiCeScanner.Data;
using NiCeScanner.Resources.API;

namespace NiCEScanner_Tests.API_Tests;

[TestClass]
public class SectorControllerTests
{
    private readonly SectorController _sectorController;
    private readonly ApplicationDbContext _context;

    public SectorControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _sectorController = new SectorController(_context);
    }

    [TestMethod]
    public async Task GetSectors_ReturnsExpectedResult()
    {
        var actionResult = await _sectorController.GetSectors();
        
        Assert.IsNotNull(actionResult);
        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        var returnedSectors = okResult.Value as IEnumerable<SectorResource>;
        Assert.IsNotNull(returnedSectors);

        var sectors = _context.Sectors.ToList();

        Assert.AreEqual(sectors.Count, returnedSectors.Count());

        foreach (var sector in sectors)
        {
            var returnedSector = returnedSectors.FirstOrDefault(s => s.Id == sector.Id);
            Assert.IsNotNull(returnedSector);
            Assert.AreEqual(sector.Data, returnedSector.Data);
        }
    }
        
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}