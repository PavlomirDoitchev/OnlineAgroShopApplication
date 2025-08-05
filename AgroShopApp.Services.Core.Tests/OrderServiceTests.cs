
namespace AgroShopApp.Services.Tests
{
    using AgroShopApp.Data.Models;
    using AgroShopApp.Data.Repository.Contracts;
    using AgroShopApp.Services.Core;
    using MockQueryable;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> orderRepoMock;
        private Mock<ICartRepository> cartRepoMock;
        private Mock<IProductRepository> productRepoMock;
        private OrderService orderService;

        private readonly Guid userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        private readonly Guid orderId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        [SetUp]
        public void Setup()
        {
            orderRepoMock = new Mock<IOrderRepository>();
            cartRepoMock = new Mock<ICartRepository>();
            productRepoMock = new Mock<IProductRepository>();
            orderService = new OrderService(orderRepoMock.Object, cartRepoMock.Object, productRepoMock.Object);
        }

        [Test]
        public async Task GetPaginatedUserOrdersAsync_ShouldReturnCorrectPage()
        {
            var orders = Enumerable.Range(1, 5).Select(i => new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderedOn = DateTime.Now.AddDays(-i),
                Status = "Pending",
                TotalAmount = 10 * i
            }).ToList();

            orderRepoMock.Setup(r => r.GetAllByUserAsync(userId)).ReturnsAsync(orders);

            var result = await orderService.GetPaginatedUserOrdersAsync(userId, 1, 2);

            Assert.AreEqual(2, result.Orders.Count);
            Assert.AreEqual(3, result.TotalPages);
        }

        [Test]
        public async Task GetDetailsAsync_ShouldReturnDetails_WhenUserOwnsOrder()
        {
            var order = new Order
            {
                Id = orderId,
                UserId = userId,
                OrderedOn = DateTime.Now,
                Status = "Completed",
                DeliveryAddress = "Test Address",
                TotalAmount = 100,
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 2, UnitPrice = 5, Product = new Product { Name = "Test Product" } }
                }
            };

            orderRepoMock.Setup(r => r.GetWithItemsAsync(orderId)).ReturnsAsync(order);

            var result = await orderService.GetDetailsAsync(orderId, userId);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test Product", result.Items.First().ProductName);
        }

        [Test]
        public async Task UpdateStatusAsync_ShouldUpdateToCancelledAndRestock_WhenPending()
        {
            var order = new Order
            {
                Id = orderId,
                Status = "Pending",
                Items = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, Product = new Product { StockQuantity = 0 } }
                }
            };

            var orderList = new List<Order> { order }.AsQueryable();
            var mockQueryable = new List<Order> { order }.BuildMock();

            orderRepoMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);
            orderRepoMock.Setup(r => r.UpdateAsync(order)).ReturnsAsync(true);

            var result = await orderService.UpdateStatusAsync(orderId, "Cancelled");

            Assert.IsTrue(result);
            Assert.AreEqual("Cancelled", order.Status);
            Assert.AreEqual(1, order.Items.First().Product.StockQuantity);
        }


        [Test]
        public async Task TryCancelOrderAsync_ShouldCancel_WhenUserOwnsPendingOrder()
        {
            var product = new Product { StockQuantity = 5 };
            var orderItem = new OrderItem { Quantity = 2, Product = product };
            var order = new Order
            {
                Id = orderId,
                UserId = userId,
                User = new ApplicationUser { Id = userId },
                Status = "Pending",
                Items = new List<OrderItem> { orderItem }
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims);
            var userPrincipal = new ClaimsPrincipal(identity);

            var mockQueryable = new List<Order> { order }.BuildMock();

            orderRepoMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);
            orderRepoMock.Setup(r => r.UpdateAsync(order)).ReturnsAsync(true);

            var result = await orderService.TryCancelOrderAsync(orderId, userPrincipal);

            Assert.IsTrue(result);
            Assert.AreEqual("Cancelled", order.Status);
            Assert.AreEqual(7, order.Items.First().Product.StockQuantity);
        }

        [Test]
        public async Task PlaceOrderAsync_ShouldPlaceOrderAndClearCart_WhenValid()
        {
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "TomatoSeeds",
                Price = 3.0m,
                StockQuantity = 10
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "AvocadoSeeds",
                Price = 2.5m,
                StockQuantity = 5
            };

            var cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = product1.Id, Quantity = 2, Product = product1 },
                    new CartItem { ProductId = product2.Id, Quantity = 1, Product = product2 }
                }
            };

            productRepoMock.Setup(r => r.GetByIdAsync(product1.Id)).ReturnsAsync(product1);
            productRepoMock.Setup(r => r.GetByIdAsync(product2.Id)).ReturnsAsync(product2);

            cartRepoMock.Setup(r => r.GetWithItemsAsync(userId)).ReturnsAsync(cart);

            Order? capturedOrder = null;

            orderRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Callback<Order>(o => capturedOrder = o)
                .Returns(Task.CompletedTask);

            cartRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var deliveryAddress = "123 Test Street";

            await orderService.PlaceOrderAsync(userId, deliveryAddress);

            Assert.IsNotNull(capturedOrder);
            Assert.AreEqual(userId, capturedOrder.UserId);
            Assert.AreEqual(deliveryAddress, capturedOrder.DeliveryAddress);
            Assert.AreEqual(8.5m, capturedOrder.TotalAmount); 
            Assert.AreEqual(2, capturedOrder.Items.Count);

            Assert.AreEqual(8, product1.StockQuantity); 
            Assert.AreEqual(4, product2.StockQuantity); 

            Assert.IsEmpty(cart.Items);

            orderRepoMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
            cartRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
