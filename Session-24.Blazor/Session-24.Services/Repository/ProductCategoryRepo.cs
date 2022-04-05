using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;

namespace BlackCoffeeshop.EF.Configuration {
    public class ProductCategoryRepo : IEntityRepo<ProductCategory>
    {
        private readonly ApplicationContext context;
        public ProductCategoryRepo(ApplicationContext dbCOntext)
        {
            context = dbCOntext;
        }     

        public async Task CreateAsync(ProductCategory entity)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.ProductCategories.Add(entity);

            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {

            var dbProdCat = context.ProductCategories.SingleOrDefault(prodCat => prodCat.ID == id);
            if (dbProdCat is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.ProductCategories.Remove(dbProdCat);

            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await context.ProductCategories.SingleOrDefaultAsync(prodCat => prodCat.ID == id);
        }

        public async Task UpdateAsync(int id, ProductCategory entity)
        {

            var dbProdCat = context.ProductCategories.SingleOrDefault(prodCat => prodCat.ID == id);
            if (dbProdCat is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            dbProdCat.Code = entity.Code;
            dbProdCat.ProductType = entity.ProductType;
            dbProdCat.Description = entity.Description;

            await context.SaveChangesAsync();
        }       
    }
}
