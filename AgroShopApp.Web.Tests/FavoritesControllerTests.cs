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
    public class FavoritesControllerTests
    {
        private Mock<IFavoritesService> favoriteServiceMock;
        private Mock<ICompositeViewEngine> viewEngineMock;
        private Mock<ILogger<FavoritesController>> loggerMock;
        private FavoritesController controller;

        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid productId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            favoriteServiceMock = new Mock<IFavoritesService>();
            viewEngineMock = new Mock<ICompositeViewEngine>();
            loggerMock = new Mock<ILogger<FavoritesController>>();

            controller = new FavoritesController(
                favoriteServiceMock.Object,
                viewEngineMock.Object,
                loggerMock.Object);

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
        public async Task Index_ShouldReturnSafeViewWithModel()
        {
            var favorites = new List<FavoriteProductViewModel>
            {
                new FavoriteProductViewModel { ProductId = productId, Name = "Test Product" }
            };

            favoriteServiceMock.Setup(s => s.GetUserFavoritesAsync(userId)).ReturnsAsync(favorites);

            var result = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreSame(favorites, viewResult?.Model);
        }

        [Test]
        public async Task Add_ShouldAddFavoriteAndRedirect()
        {
            var returnUrl = "/Product";

            var result = await controller.Add(productId, returnUrl);

            favoriteServiceMock.Verify(s => s.AddToFavoritesAsync(userId, productId), Times.Once);

            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual(returnUrl, ((RedirectResult)result).Url);
        }

        [Test]
        public async Task Remove_ShouldRemoveFavoriteAndRedirectToIndex()
        {
            var result = await controller.Remove(productId);

            favoriteServiceMock.Verify(s => s.RemoveFromFavoritesAsync(userId, productId), Times.Once);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirect.ActionName);
            Assert.AreEqual("Favorites", redirect.ControllerName);
        }
    }
}
