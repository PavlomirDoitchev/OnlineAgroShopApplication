namespace AgroShopApp.Services.Tests
{
    using AgroShopApp.Data.Models;
    using AgroShopApp.Data.Repository.Contracts;
    using AgroShopApp.Services.Core;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    [TestFixture]
    public class FavoritesServiceTests
    {
        private Mock<IFavoriteRepository> favoriteRepoMock;
        private FavoritesService favoritesService;

        private readonly Guid userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private readonly Guid productId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        [SetUp]
        public void Setup()
        {
            this.favoriteRepoMock = new Mock<IFavoriteRepository>(MockBehavior.Strict);
            this.favoritesService = new FavoritesService(favoriteRepoMock.Object);
        }

        [Test]
        public async Task AddToFavoritesAsync_ShouldAdd_WhenNotExists()
        {
            favoriteRepoMock.Setup(r => r.ExistsAsync(userId, productId)).ReturnsAsync(false);
            favoriteRepoMock.Setup(r => r.AddAsync(It.Is<Favorite>(f => f.UserId == userId && f.ProductId == productId))).Returns(Task.CompletedTask);

            await favoritesService.AddToFavoritesAsync(userId, productId);
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_ShouldRemove_WhenExists()
        {
            var favorite = new Favorite { UserId = userId, ProductId = productId };
            favoriteRepoMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Favorite, bool>>>()))
                .ReturnsAsync(favorite);
            favoriteRepoMock.Setup(r => r.HardDeleteAsync(favorite)).ReturnsAsync(true);

            await favoritesService.RemoveFromFavoritesAsync(userId, productId);
        }

        [Test]
        public async Task GetUserFavoritesAsync_ShouldReturnMappedViewModels()
        {
            var favorite = new Favorite
            {
                Product = new Product
                {
                    Id = productId,
                    Name = "Apple",
                    Description = "Fresh",
                    Price = 1.5m,
                    ImageUrl = "img.jpg",
                    Category = new Category { Name = "Fruits" }
                }
            };

            favoriteRepoMock.Setup(r => r.GetUserFavoritesAsync(userId)).ReturnsAsync(new List<Favorite> { favorite });

            var result = await favoritesService.GetUserFavoritesAsync(userId);

            var vm = result.First();
            Assert.AreEqual("Apple", vm.Name);
            Assert.AreEqual("Fruits", vm.Category);
        }

        [Test]
        public async Task IsFavoriteAsync_ShouldReturnTrue_WhenExists()
        {
            favoriteRepoMock.Setup(r => r.ExistsAsync(userId, productId)).ReturnsAsync(true);

            var result = await favoritesService.IsFavoriteAsync(userId, productId);

            Assert.IsTrue(result);
        }
    }
}
