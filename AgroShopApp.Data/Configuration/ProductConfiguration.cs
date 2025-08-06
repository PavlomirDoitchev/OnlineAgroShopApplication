using AgroShopApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static AgroShopApp.Data.Common.EntityConstants.Product;

namespace AgroShopApp.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity
                .HasKey(p => p.Id);

            entity
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            entity
                .Property(p => p.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength);

            entity
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity
                .Property(p => p.StockQuantity)
                .IsRequired()
                .HasDefaultValue(10);

            entity
                .Property(p => p.IsAvailable)
                .HasDefaultValue(true);

            entity
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasQueryFilter(p => p.IsDeleted == false);

            entity
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasData(SeedProducts());
        }
        private List<Product> SeedProducts()
        {
            return new List<Product>
            {
                // Seeds Category (ID = 1)
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "Heirloom Tomato Seeds", Description = "Rich, juicy tomatoes perfect for home gardening. Non-GMO and high germination rate.", Price = 3.49m, StockQuantity = 100, ImageUrl = "/images/seeds-tomato.jpg", AddedOn = DateTime.Now.AddDays(-14), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Organic Lettuce Seeds", Description = "Fast-growing leafy greens ideal for spring gardens.", Price = 2.99m, StockQuantity = 80, ImageUrl = "/images/seeds-lettuce.jpg", AddedOn = DateTime.Now.AddDays(-10), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Cucumber Seeds", Description = "Crunchy cucumbers, suitable for pickling or fresh eating.", Price = 2.59m, StockQuantity = 70, ImageUrl = "https://images.unsplash.com/photo-1449300079323-02e209d9d3a6?q=80&w=1548&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-7), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "Carrot Seeds", Description = "Sweet and crisp carrots with fast growth cycle.", Price = 1.99m, StockQuantity = 90, ImageUrl = "https://images.unsplash.com/photo-1447175008436-054170c2e979?q=80&w=1998&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-12), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Name = "Spinach Seeds", Description = "Cold-hardy and packed with nutrition.", Price = 2.49m, StockQuantity = 60, ImageUrl = "https://images.unsplash.com/photo-1576045057995-568f588f82fb?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-8), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Name = "Pepper Seeds", Description = "Hot and sweet varieties perfect for salsa.", Price = 3.99m, StockQuantity = 75, ImageUrl = "https://images.unsplash.com/photo-1608737637507-9aaeb9f4bf30?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-9), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Name = "Basil Seeds", Description = "Aromatic herbs for cooking and companion planting.", Price = 1.89m, StockQuantity = 100, ImageUrl = "https://images.unsplash.com/photo-1627738663093-d0779d56e3bc?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-6), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Name = "Zucchini Seeds", Description = "High-yielding summer squash variety.", Price = 2.39m, StockQuantity = 55, ImageUrl = "https://images.unsplash.com/photo-1596056094719-10ba4f7ea650?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-5), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Name = "Pumpkin Seeds", Description = "Large pumpkins ideal for decoration and pie.", Price = 3.25m, StockQuantity = 40, ImageUrl = "https://images.unsplash.com/photo-1506917728037-b6af01a7d403?q=80&w=1548&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-11), CategoryId = 1 },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), Name = "Radish Seeds", Description = "Fast-growing root vegetable for spring or fall.", Price = 1.79m, StockQuantity = 85, ImageUrl = "https://images.unsplash.com/photo-1589753014594-0676c69bbcbe?q=80&w=1480&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-4), CategoryId = 1 },

                // Fertilizer Category (ID = 2)
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), Name = "Organic Fertilizer 5kg", Description = "All-natural fertilizer for vegetables and flowers.", Price = 12.95m, StockQuantity = 50, ImageUrl = "/images/fertilizer-organic.jpg", AddedOn = DateTime.Now.AddDays(-20), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), Name = "Liquid Plant Booster", Description = "Concentrated enhancer for better root development.", Price = 8.49m, StockQuantity = 65, ImageUrl = "/images/fertilizer-liquid.jpg", AddedOn = DateTime.Now.AddDays(-5), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), Name = "Compost Mix", Description = "Rich compost to improve soil structure.", Price = 6.99m, StockQuantity = 70, ImageUrl = "https://images.unsplash.com/photo-1649577193391-f13d769d011d?q=80&w=1742&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", AddedOn = DateTime.Now.AddDays(-13), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), Name = "Worm Castings", Description = "Natural soil amendment high in nutrients.", Price = 9.49m, StockQuantity = 45, ImageUrl = "https://unclejimswormfarm.com/wp-content/uploads/2016/02/harvesting-worm-castings.jpeg", AddedOn = DateTime.Now.AddDays(-10), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), Name = "Bone Meal Fertilizer", Description = "Promotes strong root growth and blooms.", Price = 5.95m, StockQuantity = 60, ImageUrl = "https://radhakrishnaagriculture.in/cdn/shop/files/boneMeal.jpg?v=1711435429", AddedOn = DateTime.Now.AddDays(-8), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000006"), Name = "Fish Emulsion", Description = "Liquid organic fertilizer for leafy greens.", Price = 7.25m, StockQuantity = 40, ImageUrl = "https://www.pennington.com/-/media/Project/OneWeb/Pennington/Images/blog/fertilizer/what-is-fish-fertilizer/fish-fertilizer-og.jpg", AddedOn = DateTime.Now.AddDays(-6), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000007"), Name = "Seaweed Extract", Description = "Boosts plant resistance and nutrient uptake.", Price = 6.75m, StockQuantity = 55, ImageUrl = "https://www.marketresearchintellect.com/images/blogs/ocean-s-bounty-the-rising-tide-of-seaweed-fertilizer-market-growth.webp", AddedOn = DateTime.Now.AddDays(-9), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000008"), Name = "Slow Release Pellets", Description = "Feeds plants for up to 3 months.", Price = 10.00m, StockQuantity = 50, ImageUrl = "https://assets.manufactum.de/p/207/207508/207508_02.jpg/organic-fertilizer-sheep-wool-pellets.jpg", AddedOn = DateTime.Now.AddDays(-4), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000009"), Name = "Nitrogen Boost", Description = "Fast-acting nitrogen formula for leafy growth.", Price = 4.95m, StockQuantity = 65, ImageUrl = "https://cdn.shopify.com/s/files/1/0015/4976/2632/files/Nitrogen_Max1_whiite.jpg?v=1752089739&width=600&height=600&crop=center", AddedOn = DateTime.Now.AddDays(-3), CategoryId = 2 },
                new Product { Id = Guid.Parse("20000000-0000-0000-0000-000000000010"), Name = "All-Purpose Fertilizer", Description = "Balanced nutrients for all plants.", Price = 6.49m, StockQuantity = 85, ImageUrl = "https://i5.walmartimages.com/seo/Expert-Gardener-All-Purpose-Plant-Food-Fertilizer-12-0-12-40-lb_b0d92f08-b9c5-4ccd-b212-42087b2ce829.76e288d920602343a57f64968148409e.jpeg", AddedOn = DateTime.Now.AddDays(-2), CategoryId = 2 },

                // Pesticide Category (ID = 3)
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000001"), Name = "Eco-Friendly Insect Repellent", Description = "Protect your crops from pests without chemicals.", Price = 5.75m, StockQuantity = 70, ImageUrl = "/images/pesticide-eco.jpg", AddedOn = DateTime.Now.AddDays(-7), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000002"), Name = "Neem Oil Spray", Description = "Effective against mites, aphids, and fungi.", Price = 9.99m, StockQuantity = 45, ImageUrl = "/images/pesticide-neem.jpg", AddedOn = DateTime.Now.AddDays(-2), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000003"), Name = "Garlic Pest Repellent", Description = "Natural garlic-based spray for flying insects.", Price = 4.99m, StockQuantity = 60, ImageUrl = "https://www.arbico-organics.com/images/uploads/1452603_Garlic_Barrier_AG_600x600.jpg", AddedOn = DateTime.Now.AddDays(-3), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000004"), Name = "Insecticidal Soap", Description = "Kills soft-bodied insects on contact.", Price = 6.50m, StockQuantity = 50, ImageUrl = "https://m.media-amazon.com/images/I/81-uTI1JLnL._UF350,350_QL80_.jpg", AddedOn = DateTime.Now.AddDays(-6), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000005"), Name = "Pyrethrin Spray", Description = "Fast knockdown effect for garden pests.", Price = 8.75m, StockQuantity = 55, ImageUrl = "https://m.media-amazon.com/images/I/61Cmdc161eL.jpg", AddedOn = DateTime.Now.AddDays(-8), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000006"), Name = "Slugo Bait", Description = "Effective for snails and slugs in vegetable beds.", Price = 5.25m, StockQuantity = 40, ImageUrl = "https://www.nexles.com/media/catalog/product/cache/18/thumbnail/500x/8083c875e83be300356bb052a4e4af68/a/u/au_190090_def_ps.png.jpg", AddedOn = DateTime.Now.AddDays(-5), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000007"), Name = "BT Caterpillar Control", Description = "Bacillus thuringiensis for caterpillar management.", Price = 7.85m, StockQuantity = 45, ImageUrl = "https://files.plytix.com/api/v1.1/file/public_files/pim/assets/43/37/8d/5e/5e8d3743202d9eba64d3af60/images/12/a0/da/63/63daa01245952636f4885023/8066_LifeStyle_02.jpg", AddedOn = DateTime.Now.AddDays(-4), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000008"), Name = "DE Powder", Description = "Diatomaceous earth for crawling insects.", Price = 6.10m, StockQuantity = 35, ImageUrl = "https://dombikagro.com/image/catalog/HOMEVO/organichen_preparat_pesticiden_neutralizator_homevo_homevo_neutralizator_pesticide.jpg", AddedOn = DateTime.Now.AddDays(-6), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000009"), Name = "Chili Pepper Spray", Description = "Repels rodents and bugs naturally.", Price = 3.99m, StockQuantity = 50, ImageUrl = "https://m.media-amazon.com/images/I/612sBQNgyNL.jpg", AddedOn = DateTime.Now.AddDays(-9), CategoryId = 3 },
                new Product { Id = Guid.Parse("30000000-0000-0000-0000-000000000010"), Name = "Sulfur Dust", Description = "Fungicide and mite control for vegetables.", Price = 4.25m, StockQuantity = 60, ImageUrl = "https://m.media-amazon.com/images/I/51sTPn6ij7L._SL1500_.jpg", AddedOn = DateTime.Now.AddDays(-10), CategoryId = 3 }
            };
        }
    }

}
