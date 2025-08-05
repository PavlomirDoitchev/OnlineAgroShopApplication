namespace AgroShopApp.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using AgroShopApp.Data.Models;
    using AgroShopApp.Data.Repository.Contracts;
    using AgroShopApp.Services.Core;
    using AgroShopApp.Web.ViewModels.Cart;

    [TestFixture]
    public class CartServiceTests
    {
        private Mock<IProductRepository> productRepoMock;
        private Mock<ICartRepository> cartRepoMock;
        private CartService cartService;

        private readonly Guid userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private readonly Guid productId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        [SetUp]
        public void Setup()
        {
            this.productRepoMock = new Mock<IProductRepository>(MockBehavior.Strict);
            this.cartRepoMock = new Mock<ICartRepository>(MockBehavior.Strict);
            this.cartService = new CartService(cartRepoMock.Object, productRepoMock.Object);
        }

        [Test]
        public async Task AddToCartAsync_ShouldAddNewItem_WhenProductIsAvailable()
        {
            var product = new Product { Id = productId, IsAvailable = true, IsDeleted = false, StockQuantity = 5 };
            var cart = new Cart { UserId = userId, Items = new List<CartItem>() };

            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            cartRepoMock.Setup(r => r.GetOrCreateCartAsync(userId)).ReturnsAsync(cart);
            cartRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await cartService.AddToCartAsync(userId, productId);

            Assert.That(1, Is.EqualTo(cart.Items.Count));
            Assert.That(productId, Is.EqualTo(cart.Items.First().ProductId));
        }

        [Test]
        public async Task RemoveFromCartAsync_ShouldRemoveItem_WhenExists()
        {
            var cart = new Cart { UserId = userId, Items = new List<CartItem> { new CartItem { ProductId = productId } } };
            cartRepoMock.Setup(r => r.GetOrCreateCartAsync(userId)).ReturnsAsync(cart);
            cartRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await cartService.RemoveFromCartAsync(userId, productId);

            Assert.IsEmpty(cart.Items);
        }

        [Test]
        public async Task DecreaseQuantityAsync_ShouldRemoveItem_WhenQuantityIsOne()
        {
            var cart = new Cart { UserId = userId, Items = new List<CartItem> { new CartItem { ProductId = productId, Quantity = 1 } } };
            cartRepoMock.Setup(r => r.GetOrCreateCartAsync(userId)).ReturnsAsync(cart);
            cartRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await cartService.DecreaseQuantityAsync(userId, productId);

            Assert.IsEmpty(cart.Items);
        }

        [Test]
        public async Task GetCartItemsAsync_ShouldReturnOnlyItemsWithValidProducts()
        {
            var cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = productId, Quantity = 2, Product = new Product { Name = "AppleSeeds", Price = 1.5m, ImageUrl = "img.jpg", StockQuantity = 10 } },
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 0, Product = null }
                }
            };

            cartRepoMock.Setup(r => r.GetOrCreateCartAsync(userId)).ReturnsAsync(cart);

            var result = await cartService.GetCartItemsAsync(userId);

            Assert.That(1, Is.EqualTo(result.Count()));
            Assert.That("AppleSeeds", Is.EqualTo(result.First().Name));
        }

      
        [Test]
        public async Task GetStockForProductAsync_ShouldReturnStock_WhenProductExists()
        {
            var product = new Product { Id = productId, StockQuantity = 10 };
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var stock = await cartService.GetStockForProductAsync(productId);

            Assert.That(10, Is.EqualTo(stock));
        }

        [Test]
        public async Task GetCartTotalAsync_ShouldReturnCorrectTotal()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { Quantity = 2, Product = new Product { Price = 3.0m, IsDeleted = false } },
                    new CartItem { Quantity = 1, Product = new Product { Price = 2.0m, IsDeleted = true } }
                }
            };

            cartRepoMock.Setup(r => r.GetWithItemsAsync(userId)).ReturnsAsync(cart);

            var total = await cartService.GetCartTotalAsync(userId);

            Assert.That(6.0m, Is.EqualTo(total));
        }
    }
}
