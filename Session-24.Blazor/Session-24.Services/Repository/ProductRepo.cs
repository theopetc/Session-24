using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;

namespace BlackCoffeeshop.EF.Configuration {
    public class ProductRepo : IEntityRepo<Product>
    {
        private readonly ApplicationContext context;
        public ProductRepo(ApplicationContext dbCOntext)
        {
            context = dbCOntext;
        }
        
        public async Task CreateAsync(Product entity)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Products.Add(entity);

            await context.SaveChangesAsync();
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products.SingleOrDefaultAsync(prod => prod.ID == id);
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var dbProd = context.Products.SingleOrDefault(prod => prod.ID == id);
            if (dbProd is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Products.Remove(dbProd);

            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Product entity)
        {
            var dbProd = context.Products.SingleOrDefault(prod => prod.ID == id);
            if (dbProd is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");


            dbProd.Code = entity.Code;
            dbProd.Description = entity.Description;
            dbProd.Cost = entity.Cost;
            dbProd.Price = entity.Price;
            dbProd.ProductCategoryID = entity.ProductCategoryID;

            await context.SaveChangesAsync();
        }

    }
}
