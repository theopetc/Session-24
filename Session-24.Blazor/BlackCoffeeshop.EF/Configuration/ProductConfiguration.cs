using BlackCoffeeshop.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlackCoffeeshop.Model;

namespace BlackCoffeeshop.EF.Configuration {
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.ID);
            builder.Property(product => product.ID).ValueGeneratedOnAdd();

            builder.Property(product => product.Code).HasMaxLength(10);
            builder.Property(product => product.Description).HasMaxLength(30);
            builder.Property(product => product.ProductCategoryID).HasMaxLength(10);
            builder.Property(product => product.Price).HasPrecision(7, 2);
            builder.Property(product => product.Cost).HasPrecision(7, 2);


            builder.HasMany(product => product.TransactionLines).WithOne(transactionLine => transactionLine.Product).HasForeignKey(transactionLine => transactionLine.ProductID);
            builder.HasOne(product => product.ProductCategory).WithMany(productCategory => productCategory.Products).HasForeignKey(product => product.ProductCategoryID);
        }
    }
}
