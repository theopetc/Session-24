
using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Services.Repository
{
    public class MonthlyLedgerRepo : IEntityRepo<MonthlyLedger>
    {
        private readonly ApplicationContext _context;

        public MonthlyLedgerRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonthlyLedger>> GetAllAsync()
        {
            return await _context.MonthlyLedgers.ToListAsync();
        }

        public async Task<MonthlyLedger?> GetByIdAsync(int id)
        {
            return await _context.MonthlyLedgers.SingleOrDefaultAsync(mothlyLedger => mothlyLedger.ID == id);
        }

        public async Task CreateAsync(MonthlyLedger entity)
        {
            AddLogic(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, MonthlyLedger entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(MonthlyLedger entity)
        {
            //if (entity.ID ==0)
            //    throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var exist = _context.MonthlyLedgers.FirstOrDefault(ml => ml.Year == entity.Year && ml.Month == entity.Month);
            if (exist != null)
                return;
            _context.MonthlyLedgers.Add(entity);
        }

        private void DeleteLogic(int id)
        {
            var ledger = _context.MonthlyLedgers.SingleOrDefault(mledger => mledger.ID == id);
            if (ledger is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _context.MonthlyLedgers.Remove(ledger);
        }

    }
}
