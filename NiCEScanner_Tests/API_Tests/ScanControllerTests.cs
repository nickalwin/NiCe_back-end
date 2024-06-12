using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiCEScanner_Tests.Factories.API;
using NiCeScanner.Controllers.API;
using NiCeScanner.Data;
using NiCeScanner.Resources.Request.Scan;
using NiCeScanner.Models;
using NiCeScanner.Resources.API;

namespace NiCEScanner_Tests.API_Tests;

[TestClass]
public class ScanControllerTests
{
    private readonly ScanController _scanController;
    private readonly ApplicationDbContext _context;

    public ScanControllerTests()
    {
        _context = DummyDatabaseFactory.Create();
        _scanController = new ScanController(_context);
    }

    [TestMethod]
    public async Task GetScan_InvalidScan_ReturnsNotFound()
    {
        var actionResult = await _scanController.GetScan(Guid.NewGuid().ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan not found", notFoundResult.Value);
    }

    [TestMethod]
    public async Task GetScan_ValidScan_ReturnsScan()
    {
        ScanComposite composite = ScanCompositeFactory.CreateAndSubmit(_context);

        var actionResult = await _scanController.GetScan(composite.Scan.Uuid.ToString());

        Assert.IsNotNull(actionResult);

        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(okResult);

        var scanResource = okResult.Value as ScanResource;

        Assert.IsNotNull(scanResource);

        Assert.AreEqual(composite.Scan.Uuid.ToString(), scanResource.Uuid.ToString());
    }

    [TestMethod]
    public async Task StoreScan_ValidData_CreatesScan()
    {
        PostScanRequest request = new PostScanRequest
        {
            Sector_id = 1,
            Contact_name = "Test",
            Contact_email = "test@gmail.com",
            Answers = new List<AnswerElement>
            {
                new AnswerElement
                {
                    Category_uuid = _context.Categories.First().Uuid,
                    Question_uuid = _context.Questions.First().Uuid,
                    Answer = 1,
                    Comment = "Test"
                },
            }
        };

        var actionResult = await _scanController.StoreScan(request);

        Assert.IsNotNull(actionResult);

        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(okResult);

        var result = okResult.Value as PostScanRequestResult;

        Assert.IsNotNull(result);

        var scan = await _context.Scans.Where(s => s.Uuid == result.Uuid).Include(s => s.Answers).FirstOrDefaultAsync();

        Assert.IsNotNull(scan);

        Assert.AreEqual(request.Contact_name, scan.ContactName);
        Assert.AreEqual(request.Contact_email, scan.ContactEmail);
        Assert.AreEqual(request.Sector_id, scan.SectorId);
        Assert.AreEqual(1, scan.Answers.Count);

        Answer answer = scan.Answers.First();

        Assert.IsNotNull(answer);

        Assert.AreEqual(request.Answers.First().Answer, answer.Score);
        Assert.AreEqual(request.Answers.First().Comment, answer.Comment);
    }

    [TestMethod]
    public async Task UpdateAnswer_InvalidScan_ReturnsNotFound()
    {
        var actionResult = await _scanController.DeleteContactInfo(Guid.NewGuid().ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan not found", notFoundResult.Value);
    }

    [TestMethod]
    public async Task UpdateAnswer_InvalidAnswer_ReturnsNotFound()
    {
        ScanComposite composite = ScanCompositeFactory.CreateAndSubmit(_context);

        PutScanUpdateAnswerRequest request = new PutScanUpdateAnswerRequest
        {
            Answer = 1,
            Comment = "Test"
        };

        var actionResult = await _scanController.UpdateAnswer(composite.Scan.Uuid.ToString(), Guid.NewGuid().ToString(), request);

        Assert.IsNotNull(actionResult);

        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);

        var notFoundResult = actionResult.Result as NotFoundObjectResult;

        Assert.IsNotNull(notFoundResult);

        Assert.AreEqual("Answer not found", notFoundResult.Value);
    }

    [TestMethod]
    public async Task UpdateAnswer_ValidAnswer_UpdatesAnswer()
    {
        ScanComposite composite = ScanCompositeFactory.CreateAndSubmit(_context);
        Answer answer = composite.Scan.Answers.First();
        Question question = _context.Questions.Find(answer.QuestionId);

        Assert.IsNotNull(answer);
        Assert.IsNotNull(question);

        PutScanUpdateAnswerRequest request = new PutScanUpdateAnswerRequest
        {
            Answer = 10,
            Comment = "Test123"
        };

        var actionResult = await _scanController.UpdateAnswer(composite.Scan.Uuid.ToString(), question.Uuid.ToString(), request);

        Assert.IsNotNull(actionResult);

        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(okResult);

        Scan updatedScan = await _context.Scans.FindAsync(composite.Scan.Id);

        Assert.IsNotNull(updatedScan);

        Answer updatedAnswer = updatedScan.Answers.FirstOrDefault(a => a.QuestionId == question.Id);

        Assert.IsNotNull(updatedAnswer);

        Assert.AreEqual(request.Answer, updatedAnswer.Score);
        Assert.AreEqual(request.Comment, updatedAnswer.Comment);
    }
    
    [TestMethod]
    public async Task DeleteContactInfo_InvalidScan_ReturnsNotFound()
    {
        var actionResult = await _scanController.DeleteContactInfo(Guid.NewGuid().ToString());
        
        Assert.IsNotNull(actionResult);
        
        Assert.IsTrue(actionResult.Result is NotFoundObjectResult);
        
        var notFoundResult = actionResult.Result as NotFoundObjectResult;
        
        Assert.IsNotNull(notFoundResult);
        
        Assert.AreEqual("Scan not found", notFoundResult.Value);

    }

    [TestMethod]
    public async Task DeleteContactInfo_ValidScan_ClearsOutTheContactInfoAndComments()
    {
        ScanComposite composite = ScanCompositeFactory.CreateAndSubmit(_context);

        var actionResult = await _scanController.DeleteContactInfo(composite.Scan.Uuid.ToString());

        Assert.IsNotNull(actionResult);

        Assert.IsTrue(actionResult.Result is OkResult);

        var okResult = actionResult.Result as OkResult;

        Assert.IsNotNull(okResult);

        var scan = await _context.Scans.FindAsync(composite.Scan.Id);

        Assert.IsNotNull(scan);

        Assert.IsTrue(scan.Answers.All(a => a.Comment == ""));

        Assert.IsTrue(scan.ContactName == "");
        Assert.IsTrue(scan.ContactEmail == "");
    }
        
    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }
}