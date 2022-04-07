using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;

namespace Session_24.Services.Repository
{
    public class CustomerRepo : IEntityRepo<Customer>
    {
        private readonly ApplicationContext context;
        public CustomerRepo(ApplicationContext dbCOntext)
        {
            context = dbCOntext;
        }

        public async Task CreateAsync(Customer entity)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Customers.Add(entity);
            await context.SaveChangesAsync();
        }
      
        public async Task DeleteAsync(int id)
        {
            var foundCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (foundCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");


            context.Customers.Remove(foundCustomer);
            await context.SaveChangesAsync();
        }        

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();
        }
        
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await context.Customers.SingleOrDefaultAsync(customer => customer.ID == id);

        }      

        public async Task UpdateAsync(int id, Customer entity)
        {
            var foundCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (foundCustomer is null)
                return;

            foundCustomer.Code = entity.Code;
            foundCustomer.Description = entity.Description;
            foundCustomer.Transactions = entity.Transactions;

            await context.SaveChangesAsync();
        }

    }
}
