using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;

namespace BlackCoffeeshop.EF.Configuration {
    public class TransactionLineRepo : IEntityRepo<TransactionLine>
    {
        private readonly ApplicationContext context;
        public TransactionLineRepo(ApplicationContext dbCOntext)
        {
            context = dbCOntext;
        }
         
        public async Task UpdateAsync(int id, TransactionLine entity)
        {
            var foundTransLine = await context.TransactionLines.SingleOrDefaultAsync(transLine => transLine.ID == id);
            if (foundTransLine is null)
                return;

            foundTransLine.ProductID = entity.ProductID;
            foundTransLine.TransactionID = entity.TransactionID;
            foundTransLine.Quantity = entity.Quantity;
            foundTransLine.Price = entity.Price;
            foundTransLine.Discount = entity.Discount;
            foundTransLine.TotalPrice = entity.TotalPrice;

            await context.SaveChangesAsync();
        }
        public async Task<TransactionLine?> GetByIdAsync(int id)
        {
            return await context.TransactionLines.SingleOrDefaultAsync(transline => transline.ID == id);
        }
        public async Task CreateAsync(TransactionLine entity)
        {
            await context.TransactionLines.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var foundTransLine = await context.TransactionLines.SingleOrDefaultAsync(transLine => transLine.ID == id);
            if (foundTransLine is null)
                return;

            context.TransactionLines.Remove(foundTransLine);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TransactionLine>> GetAllAsync()
        {
            return await context.TransactionLines.ToListAsync();
        }       
    }
}
