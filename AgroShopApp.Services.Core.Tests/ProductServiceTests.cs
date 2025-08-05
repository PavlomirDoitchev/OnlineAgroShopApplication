namespace AgroShopApp.Services.Tests
{
    using AgroShopApp.Data.Models;
    using AgroShopApp.Data.Repository.Contracts;
    using AgroShopApp.Services.Core;
    using AgroShopApp.Web.ViewModels.Product;
    using MockQueryable;
    using Moq;
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> productRepoMock;
        private Mock<ICategoryRepository> categoryRepoMock;
        private Mock<IFavoriteRepository> favoriteRepoMock;
        private Mock<ICartRepository> cartRepoMock;

        private ProductService productService;

        private readonly Guid userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private readonly Guid productId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        [SetUp]
        public void Setup()
        {
            productRepoMock = new Mock<IProductRepository>();
            categoryRepoMock = new Mock<ICategoryRepository>();
            favoriteRepoMock = new Mock<IFavoriteRepository>();
            cartRepoMock = new Mock<ICartRepository>();

            productService = new ProductService(
                productRepoMock.Object,
                categoryRepoMock.Object,
                favoriteRepoMock.Object,
                cartRepoMock.Object);
        }

        [Test]
        public async Task GetDetailsAsync_ShouldReturnViewModel_WhenProductIsAvailableAndNotDeleted()
        {
            var product = new Product
            {
                Id = productId,
                Name = "Tomato",
                Description = "Fresh tomatoes",
                Price = 1.5m,
                ImageUrl = "tomato.jpg",
                StockQuantity = 10,
                IsAvailable = true,
                IsDeleted = false,
                Category = new Category { Name = "Vegetables" }
            };

            productRepoMock.Setup(r => r.GetWithCategoryByIdAsync(productId)).ReturnsAsync(product);
            favoriteRepoMock.Setup(r => r.ExistsAsync(userId, productId)).ReturnsAsync(true);

            var result = await productService.GetDetailsAsync(productId, userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
            Assert.AreEqual("Tomato", result.Name);
            Assert.AreEqual("Vegetables", result.Category);
            Assert.IsTrue(result.IsFavorite);
        }
        [Test]
        public async Task IsOutOfStockAsync_ShouldReturnTrue_WhenProductIsNull()
        {
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product)null!);

            var result = await productService.IsOutOfStockAsync(productId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsOutOfStockAsync_ShouldReturnTrue_WhenStockIsZero()
        {
            var product = new Product { StockQuantity = 0, IsDeleted = false, IsAvailable = true };
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await productService.IsOutOfStockAsync(productId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsOutOfStockAsync_ShouldReturnTrue_WhenProductIsDeleted()
        {
            var product = new Product { StockQuantity = 5, IsDeleted = true, IsAvailable = true };
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await productService.IsOutOfStockAsync(productId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsOutOfStockAsync_ShouldReturnTrue_WhenProductIsUnavailable()
        {
            var product = new Product { StockQuantity = 5, IsDeleted = false, IsAvailable = false };
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await productService.IsOutOfStockAsync(productId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsOutOfStockAsync_ShouldReturnFalse_WhenProductIsInStockAndAvailable()
        {
            var product = new Product { StockQuantity = 5, IsDeleted = false, IsAvailable = true };
            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await productService.IsOutOfStockAsync(productId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateAsync_ShouldAddNewProductWithDefaults()
        {
            var formModel = new ProductFormViewModel
            {
                Name = "Lettuce",
                Description = "Green leaf",
                Price = 2.0m,
                ImageUrl = "lettuce.jpg",
                StockQuantity = 20,
                CategoryId = 1
            };

            Product? createdProduct = null;
            productRepoMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(p => createdProduct = p)
                .Returns(Task.CompletedTask);

            await productService.CreateAsync(formModel);

            Assert.IsNotNull(createdProduct);
            Assert.AreEqual("Lettuce", createdProduct!.Name);
            Assert.IsTrue(createdProduct.IsAvailable);
            Assert.IsFalse(createdProduct.IsDeleted);
        }

        [Test]
        public async Task EditAsync_ShouldUpdateProduct_WhenProductExists()
        {
            var product = new Product { Id = productId };
            var editModel = new EditProductViewModel
            {
                Id = productId,
                Name = "Updated Name",
                Description = "Updated Desc",
                Price = 10,
                ImageUrl = "new.jpg",
                StockQuantity = 50,
                CategoryId = 2
            };

            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            productRepoMock.Setup(r => r.UpdateAsync(product));

            await productService.EditAsync(editModel);

            Assert.AreEqual("Updated Name", product.Name);
            Assert.AreEqual("Updated Desc", product.Description);
            Assert.AreEqual(10, product.Price);
            Assert.AreEqual("new.jpg", product.ImageUrl);
            Assert.AreEqual(50, product.StockQuantity);
            Assert.AreEqual(2, product.CategoryId);
        }
        [Test]
        public async Task GetPaginatedAsync_ShouldReturnFilteredPaginatedResults_WithFavoritesAndCartQuantities()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Seeds" };
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "AppleSeeds",
                Description = "Fresh",
                Price = 1.5m,
                StockQuantity = 10,
                IsAvailable = true,
                IsDeleted = false,
                CategoryId = category.Id,
                Category = category
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "OliveSeeds",
                Description = "Black",
                Price = 2.0m,
                StockQuantity = 5,
                IsAvailable = true,
                IsDeleted = false,
                CategoryId = category.Id,
                Category = category
            };

            var allProducts = new List<Product> { product1, product2 };

            productRepoMock.Setup(r => r.GetAllWithCategoryAsync()).ReturnsAsync(allProducts);

            favoriteRepoMock.Setup(r => r.GetUserFavoritesAsync(userId))
                .ReturnsAsync(new List<Favorite> { new Favorite { ProductId = product1.Id } });

            var cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = product1.Id, Quantity = 2 }
                }
            };

            cartRepoMock.Setup(r => r.GetOrCreateCartAsync(userId)).ReturnsAsync(cart);

            categoryRepoMock.Setup(r => r.GetAllSortedAsync()).ReturnsAsync(new List<Category> { category });

            var result = await productService.GetPaginatedAsync(
                page: 1,
                pageSize: 2,
                categoryId: 1,
                searchTerm: "Seeds",
                userId: userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CurrentPage);
            Assert.AreEqual(1, result.TotalPages); 
            Assert.AreEqual(2, result.Products.Count());

            var apple = result.Products.First(p => p.Id == product1.Id);
            Assert.IsTrue(apple.IsFavorite);
            Assert.AreEqual(2, apple.QuantityInCart);

            var banana = result.Products.First(p => p.Id == product2.Id);
            Assert.IsFalse(banana.IsFavorite);
            Assert.AreEqual(0, banana.QuantityInCart);
        }
        [Test]
        public async Task RemoveAsync_ShouldMarkProductAsDeleted()
        {
            var product = new Product
            {
                Id = productId,
                IsDeleted = false
            };

            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            productRepoMock.Setup(r => r.UpdateAsync(product));

            await productService.RemoveAsync(productId);

            Assert.IsTrue(product.IsDeleted);
            Assert.IsNotNull(product.DeletedOn);
            productRepoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }
        [Test]
        public async Task RestoreAsync_ShouldUnmarkProductAsDeleted()
        {
            var product = new Product
            {
                Id = productId,
                IsDeleted = true,
                DeletedOn = DateTime.UtcNow
            };

            productRepoMock.Setup(r => r.GetByIdIncludingDeletedAsync(productId)).ReturnsAsync(product);
            productRepoMock.Setup(r => r.UpdateAsync(product));

            await productService.RestoreAsync(productId);

            Assert.IsFalse(product.IsDeleted);
            Assert.IsNull(product.DeletedOn);
            productRepoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }
        [Test]
        public async Task GetEditAsync_ShouldReturnViewModel_WhenProductExists()
        {
            var product = new Product
            {
                Id = productId,
                Name = "Tomato",
                Description = "Red veggie",
                Price = 1.0m,
                ImageUrl = "tomato.jpg",
                StockQuantity = 50,
                CategoryId = 1
            };

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Veggies" },
                new Category { Id = 2, Name = "Fruits" }
            };

            productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            categoryRepoMock.Setup(r => r.GetAllSortedAsync()).ReturnsAsync(categories);

            var result = await productService.GetEditAsync(productId);

            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
            Assert.AreEqual("Tomato", result.Name);
            Assert.AreEqual(2, result.Categories.Count());
            Assert.AreEqual("Veggies", result.Categories.First().Name);
        }
        [Test]
        public async Task GetDeletedDetailedAsync_ShouldReturnMappedViewModels()
        {
            var deletedProduct = new Product
            {
                Id = productId,
                Name = "Rotten Apple",
                Description = "Bad fruit",
                Price = 0,
                ImageUrl = "rotten.jpg",
                IsDeleted = true,
                DeletedOn = DateTime.UtcNow,
                Category = new Category { Name = "Fruits" }
            };

            productRepoMock.Setup(r => r.GetDeletedProductsAsync())
                .ReturnsAsync(new List<Product> { deletedProduct });

            var result = await productService.GetDeletedDetailedAsync();

            var vm = result.First();
            Assert.AreEqual(deletedProduct.Id, vm.Id);
            Assert.AreEqual("Rotten Apple", vm.Name);
            Assert.AreEqual("Fruits", vm.Category);
            Assert.AreEqual(deletedProduct.DeletedOn, vm.DeletedOn);
        }
        [Test]
        public async Task GetDeletedPaginatedAsync_ShouldReturnFilteredDeletedProducts()
        {
            var category = new Category { Id = 1, Name = "Spoiled" };

            var deletedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Moldy Bread",
                    Description = "Yikes",
                    Price = 0,
                    ImageUrl = "mold.jpg",
                    Category = category,
                    CategoryId = category.Id,
                    DeletedOn = DateTime.UtcNow
                }
            };

            var queryable = deletedProducts.BuildMock();

            productRepoMock.Setup(r => r.GetDeletedAttached()).Returns(queryable);

            var result = await productService.GetDeletedPaginatedAsync(
                page: 1,
                pageSize: 5,
                categoryId: 1,
                searchTerm: "Bread");

            Assert.AreEqual(1, result.DeletedProducts.Count());
            Assert.AreEqual("Moldy Bread", result.DeletedProducts.First().Name);
            Assert.AreEqual(1, result.CurrentPage);
            Assert.AreEqual(1, result.TotalPages);
        }

        [Test]
        public async Task GetCategoriesAsync_ShouldReturnAllCategoryViewModels()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Fruits" },
                new Category { Id = 2, Name = "Vegetables" }
            };

            categoryRepoMock.Setup(r => r.GetAllSortedAsync()).ReturnsAsync(categories);

            var result = await productService.GetCategoriesAsync();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Fruits", result.First().Name);
        }
    }

}