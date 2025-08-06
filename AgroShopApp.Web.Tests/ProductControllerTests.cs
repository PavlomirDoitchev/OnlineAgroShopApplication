using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace AgroShopApp.Web.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> productServiceMock;
        private Mock<ICompositeViewEngine> viewEngineMock;
        private Mock<ILogger<ProductController>> loggerMock;
        private ProductController controller;

        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid productId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            productServiceMock = new Mock<IProductService>();
            viewEngineMock = new Mock<ICompositeViewEngine>();
            loggerMock = new Mock<ILogger<ProductController>>();

            controller = new ProductController(
                productServiceMock.Object,
                viewEngineMock.Object,
                loggerMock.Object
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    }, "mock"))
                }
            };

            var tempDataProvider = new Mock<ITempDataProvider>();
            controller.TempData = new TempDataDictionary(controller.ControllerContext.HttpContext, tempDataProvider.Object);

            viewEngineMock.Setup(ve => ve.FindView(It.IsAny<ActionContext>(), It.IsAny<string>(), false))
                          .Returns(ViewEngineResult.Found("Index", Mock.Of<IView>()));
        }
        [TearDown]
        public void TearDown()
        {
            controller?.Dispose();
        }

        [Test]
        public async Task Index_ShouldReturnSafeView_WhenNotAjax()
        {
            var model = new PaginatedProductListViewModel
            {
                Products = new List<AllProductsViewModel>(),
                CurrentPage = 1,
                TotalPages = 1
            };

            productServiceMock
                .Setup(s => s.GetPaginatedAsync(1, 9, null, null, It.IsAny<Guid?>()))
                .ReturnsAsync(model);

            controller.ControllerContext.HttpContext.Request.Headers["X-Requested-With"] = ""; 

            var result = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.AreSame(model, view?.Model);
        }

        [Test]
        public async Task Index_ShouldReturnPartialView_WhenAjaxRequest()
        {
            var model = new PaginatedProductListViewModel
            {
                Products = new List<AllProductsViewModel>(),
                CurrentPage = 1,
                TotalPages = 1
            };

            productServiceMock
                .Setup(s => s.GetPaginatedAsync(1, 9, null, null, It.IsAny<Guid?>()))
                .ReturnsAsync(model);

            controller.ControllerContext.HttpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest"; 

            var result = await controller.Index();

            Assert.IsInstanceOf<PartialViewResult>(result);
            var partial = result as PartialViewResult;
            Assert.AreEqual("_ProductGridPartial", partial?.ViewName);
            Assert.AreSame(model, partial?.Model);
        }

        [Test]
        public async Task Details_ShouldReturnSafeView_WhenProductExists()
        {
            var viewModel = new AllProductsViewModel
            {
                Id = productId,
                Name = "Test Product"
            };

            productServiceMock
                .Setup(s => s.GetDetailsAsync(productId, It.IsAny<Guid?>()))
                .ReturnsAsync(viewModel);

            var result = await controller.Details(productId);

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.AreSame(viewModel, view?.Model);
        }

        [Test]
        public async Task Details_ShouldReturnNotFound_WhenProductIsNull()
        {
            productServiceMock
                .Setup(s => s.GetDetailsAsync(productId, It.IsAny<Guid?>()))
                .ReturnsAsync((AllProductsViewModel?)null);

            var result = await controller.Details(productId);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
