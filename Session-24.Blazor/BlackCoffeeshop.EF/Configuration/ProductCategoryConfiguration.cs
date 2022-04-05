using BlackCoffeeshop.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlackCoffeeshop.Model;

namespace BlackCoffeeshop.EF.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
          
            builder.ToTable("ProductCategories");

            builder.HasKey(productcategory => productcategory.ID);
            builder.Property(productcategory => productcategory.ID).ValueGeneratedOnAdd();

            builder.Property(productcategory => productcategory.Code).HasMaxLength(10);
            builder.Property(productcategory => productcategory.Description).HasMaxLength(30);
            builder.Property(productCategory => productCategory.ProductType).HasConversion(productType => productType.ToString(), productType => (ProductType)Enum.Parse(typeof(ProductType), productType)).HasMaxLength(15);



            builder.HasMany(productCategory => productCategory.Products).WithOne(product => product.ProductCategory).HasForeignKey(product => product.ProductCategoryID);
            
        }
    }
}
