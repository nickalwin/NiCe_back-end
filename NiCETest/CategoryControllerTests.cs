
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using NiCeScanner.Controllers;
using NiCeScanner.Data;
using NiCeScanner.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NiCeScanner.Tests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private CategoryController _controller;
        private Mock<DbSet<Category>> _mockSet;
        private List<Category> _categories;

        [SetUp]
        public void Setup()
        {
            _mockSet = new Mock<DbSet<Category>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1", CreatedAt = System.DateTime.Now, UpdatedAt = System.DateTime.Now },
                new Category { Id = 2, Name = "Category2", CreatedAt = System.DateTime.Now, UpdatedAt = System.DateTime.Now }
            };
            var queryableCategories = _categories.AsQueryable();

            _mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(queryableCategories.Provider);
            _mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(queryableCategories.Expression);
            _mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(queryableCategories.ElementType);
            _mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(queryableCategories.GetEnumerator());

            _mockContext.Setup(c => c.Categories).Returns(_mockSet.Object);

            _controller = new CategoryController(_mockContext.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Test]
        public async Task Index_ReturnsViewResultWithCorrectModel()
        {
            var result = await _controller.Index(null, null, null, null, null, null, null, null) as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as PaginatedList<Category>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public async Task Details_IdIsNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(null);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Details_IdIsValid_ReturnsViewResultWithCategory()
        {
            var result = await _controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
            var category = result.Model as Category;
            Assert.AreEqual("Category1", category.Name);
        }

        [Test]
        public async Task Details_IdIsInvalid_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(99);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Create_POST_ValidModelState_RedirectsToIndex()
        {
            var categoryForm = new CategoryForm { Name = "New Category", Show = true };

            var result = await _controller.Create(categoryForm) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
            _mockSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task Create_POST_InvalidModelState_ReturnsViewWithModel()
        {
            _controller.ModelState.AddModelError("Error", "ModelError");
            var categoryForm = new CategoryForm { Name = "New Category", Show = true };

            var result = await _controller.Create(categoryForm) as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as CategoryForm;
            Assert.AreEqual("New Category", model.Name);
        }

        [Test]
        public async Task Edit_GET_IdIsNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(null);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_GET_IdIsValid_ReturnsViewResultWithCategory()
        {
            var result = await _controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            var category = result.Model as Category;
            Assert.AreEqual(1, category.Id);
        }

        [Test]
        public async Task Edit_POST_InvalidModelState_ReturnsViewWithModel()
        {
            _controller.ModelState.AddModelError("Error", "ModelError");
            var category = new Category { Id = 1, Name = "Updated Category", Show = true };

            var result = await _controller.Edit(1, category) as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as Category;
            Assert.AreEqual(1, model.Id);
        }

        [Test]
        public async Task Delete_GET_IdIsNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(null);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Delete_GET_IdIsValid_ReturnsViewResultWithCategory()
        {
            var result = await _controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            var category = result.Model as Category;
            Assert.AreEqual(1, category.Id);
        }

        [Test]
        public async Task DeleteConfirmed_ValidId_DeletesCategoryAndRedirects()
        {
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
            _mockSet.Verify(m => m.Remove(It.IsAny<Category>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
