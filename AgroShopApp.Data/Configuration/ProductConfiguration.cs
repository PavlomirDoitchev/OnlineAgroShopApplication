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
        new Product
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Heirloom Tomato Seeds",
            Description = "Rich, juicy tomatoes perfect for home gardening. Non-GMO and high germination rate.",
            Price = 3.49m,
            StockQuantity = 100,
            ImageUrl = "/images/seeds-tomato.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-14),
            CategoryId = 1
        },
        new Product
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Organic Lettuce Seeds",
            Description = "Fast-growing leafy greens ideal for spring gardens.",
            Price = 2.99m,
            StockQuantity = 80,
            ImageUrl = "/images/seeds-lettuce.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-10),
            CategoryId = 1
        },
        new Product
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "All-Natural Fertilizer 5kg",
            Description = "Boost your plant health with organic nutrients. Safe for vegetables and flowers.",
            Price = 12.95m,
            StockQuantity = 50,
            ImageUrl = "/images/fertilizer-organic.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-20),
            CategoryId = 2
        },
        new Product
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "Liquid Plant Booster",
            Description = "Concentrated growth enhancer for root development and yield.",
            Price = 8.49m,
            StockQuantity = 65,
            ImageUrl = "/images/fertilizer-liquid.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-5),
            CategoryId = 2
        },
        new Product
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Name = "Eco-Friendly Insect Repellent",
            Description = "Protect your crops from pests without harmful chemicals.",
            Price = 5.75m,
            StockQuantity = 70,
            ImageUrl = "/images/pesticide-eco.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-7),
            CategoryId = 3
        },
        new Product
        {
            Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            Name = "Neem Oil Pesticide 1L",
            Description = "Effective natural solution against leaf-eating insects.",
            Price = 9.99m,
            StockQuantity = 45,
            ImageUrl = "/images/pesticide-neem.jpg",
            AddedOn = DateTime.UtcNow.AddDays(-2),
            CategoryId = 3
        }
    };
        }
    }

}
