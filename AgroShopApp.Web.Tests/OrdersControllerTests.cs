using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Controllers;
using AgroShopApp.Web.ViewModels.Order;
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
    public class OrdersControllerTests
    {
        private Mock<IOrderService> orderServiceMock;
        private Mock<ICompositeViewEngine> viewEngineMock;
        private Mock<ILogger<OrdersController>> loggerMock;
        private OrdersController controller;

        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid orderId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            orderServiceMock = new Mock<IOrderService>();
            viewEngineMock = new Mock<ICompositeViewEngine>();
            loggerMock = new Mock<ILogger<OrdersController>>();

            controller = new OrdersController(
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

            viewEngineMock.Setup(v => v.FindView(It.IsAny<ActionContext>(), It.IsAny<string>(), false))
                          .Returns(ViewEngineResult.Found("Any", Mock.Of<IView>()));
        }

        [Test]
        public async Task Index_ShouldReturnPaginatedOrders()
        {
            var resultModel = new PaginatedOrderListViewModel
            {
                Orders = new List<OrderSummaryViewModel>
                {
                    new OrderSummaryViewModel
                    {
                        Id = orderId,
                        OrderedOn = DateTime.Now,
                        Status = "Completed",
                        TotalAmount = 50
                    }
                },
                CurrentPage = 1,
                TotalPages = 1
            };

            orderServiceMock
                .Setup(s => s.GetPaginatedUserOrdersAsync(userId, 1, 5))
                .ReturnsAsync(resultModel);

            var result = await controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.AreSame(resultModel, view?.Model);
        }
        [TearDown]
        public void TearDown()
        {
            controller?.Dispose();
        }

        [Test]
        public async Task Details_ShouldReturnSafeView_WhenOrderExists()
        {
            var orderDetails = new OrderDetailsViewModel
            {
                Id = orderId,
                Status = "Completed",
                Items = new List<OrderItemViewModel>()
            };

            orderServiceMock
                .Setup(s => s.GetDetailsAsync(orderId, userId))
                .ReturnsAsync(orderDetails);

            var result = await controller.Details(orderId);

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.AreSame(orderDetails, view?.Model);
        }

        [Test]
        public async Task Details_ShouldReturnNotFound_WhenOrderIsNull()
        {
            orderServiceMock
                .Setup(s => s.GetDetailsAsync(orderId, userId))
                .ReturnsAsync((OrderDetailsViewModel?)null);

            var result = await controller.Details(orderId);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Cancel_ShouldSetSuccessTempData_WhenSuccessful()
        {
            orderServiceMock
                .Setup(s => s.TryCancelOrderAsync(orderId, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(true);

            var result = await controller.Cancel(orderId);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Order cancelled successfully.", controller.TempData["Message"]);
        }

        [Test]
        public async Task Cancel_ShouldSetFailureTempData_WhenUnsuccessful()
        {
            orderServiceMock
                .Setup(s => s.TryCancelOrderAsync(orderId, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(false);

            var result = await controller.Cancel(orderId);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Order could not be cancelled.", controller.TempData["Message"]);
        }
    }
}
