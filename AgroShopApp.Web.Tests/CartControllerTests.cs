using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Areas.Admin.Controllers;
using AgroShopApp.Web.Controllers;
using AgroShopApp.Web.ViewModels.Cart;
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
    public class CartControllerTests
    {
        private Mock<ICartService> cartServiceMock;
        private Mock<IOrderService> orderServiceMock;
        private Mock<ICompositeViewEngine> viewEngineMock;
        private Mock<ILogger<UsersController>> loggerMock;
        private CartController controller;

        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid productId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            cartServiceMock = new Mock<ICartService>();
            orderServiceMock = new Mock<IOrderService>();
            viewEngineMock = new Mock<ICompositeViewEngine>();
            loggerMock = new Mock<ILogger<UsersController>>();

            controller = new CartController(
                cartServiceMock.Object,
                orderServiceMock.Object,
                viewEngineMock.Object,
                loggerMock.Object
            );

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
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
        public async Task Index_ShouldReturnSafeViewWithCartItems()
        {
            var items = new List<CartItemViewModel>
            {
                new CartItemViewModel { ProductId = productId, Name = "Test Item", Quantity = 1, Price = 2 }
            };

            cartServiceMock.Setup(s => s.GetCartItemsAsync(userId)).ReturnsAsync(items);

            var result = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreSame(items, viewResult?.Model);
        }

        [Test]
        public async Task Add_ShouldCallServiceAndRedirectToReturnUrl()
        {
            var returnUrl = "/Product";

            var result = await controller.Add(productId, returnUrl);

            cartServiceMock.Verify(s => s.AddToCartAsync(userId, productId), Times.Once);
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual(returnUrl, ((RedirectResult)result).Url);
        }

        [Test]
        public async Task Increase_ShouldCatchInvalidOperationExceptionAndSetTempData()
        {
            cartServiceMock.Setup(s => s.AddToCartAsync(userId, productId))
                .ThrowsAsync(new InvalidOperationException("Out of stock"));

            var result = await controller.Increase(productId);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
            Assert.AreEqual("Out of stock", controller.TempData["Message"]);
        }
    }
}
