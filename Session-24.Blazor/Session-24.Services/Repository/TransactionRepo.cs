using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;

namespace Session_24.Services.Repository
{
    public class TransactionRepo : IEntityRepo<Transaction>
    {
        private readonly ApplicationContext context;
        public TransactionRepo(ApplicationContext dbCOntext)
        {
            context = dbCOntext;
        }


        public async Task CreateAsync(Transaction currentTransaction)
        {
            if (currentTransaction.ID != 0)
                throw new ArgumentException("Given currentTransaction should not have Id set", nameof(currentTransaction));

            context.Transactions.Add(currentTransaction);

            await context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var foundTransaction = await context.Transactions.SingleOrDefaultAsync(transaction => transaction.ID == id);
            if (foundTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Transactions.Remove(foundTransaction);

            await context.SaveChangesAsync();
        }    

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await context.Transactions.ToListAsync();
        }

       
        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await context.Transactions.SingleOrDefaultAsync(transaction => transaction.ID == id);
        }

       

        public async Task UpdateAsync(int id, Transaction currentTransaction)
        {
            var foundTransaction = await context.Transactions.SingleOrDefaultAsync(transaction => transaction.ID == id);
            if (foundTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");


            foundTransaction.Date = currentTransaction.Date;
            foundTransaction.EmployeeID = currentTransaction.EmployeeID;
            foundTransaction.CustomerID = currentTransaction.CustomerID;
            foundTransaction.TransactionLines = currentTransaction.TransactionLines;
            foundTransaction.PaymentMethod = currentTransaction.PaymentMethod;
            foundTransaction.TotalPrice = currentTransaction.TotalPrice;
            foundTransaction.TotalCost = currentTransaction.TotalCost;
            
            foundTransaction.Customer = currentTransaction.Customer;
            foundTransaction.Employee = currentTransaction.Employee;


            await context.SaveChangesAsync();

        }
    }
}
