using AgroShopApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static AgroShopApp.Data.Common.EntityConstants.Category;
namespace AgroShopApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .HasData(GenerateCategoryData());
        }
        private List<Category> GenerateCategoryData()
        {
            List<Category> productCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Seeds" },
                new Category { Id = 2, Name = "Fertilizer" },
                new Category { Id = 3, Name = "Pesticide" }
            };
            return productCategories;
        }
    }
}
