using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Services.Handlers
{
    public class MonthlyLedgerHandler
    {
        private const int _RENT_COST = 3000;
        private readonly ApplicationContext _context;

        public MonthlyLedgerHandler(ApplicationContext context)
        {
            _context = context;

        }

        public async Task<decimal> GetIncome(MonthlyLedger monthlyLedger)
        {
            return await _context.Transactions.Where(transaction => transaction.Date.Year == monthlyLedger.Year && transaction.Date.Month == monthlyLedger.Month).SumAsync(transaction => transaction.TotalPrice);
        }

        private async Task<decimal> GetProductExpences(MonthlyLedger monthlyLedger)
        {
            return await _context.Transactions.Where(transaction => transaction.Date.Year == monthlyLedger.Year && transaction.Date.Month == monthlyLedger.Month).SumAsync(transaction => transaction.TotalCost);
            
        }

        private async Task<decimal> GetStuffExpences()
        {
            return await _context.Employees.SumAsync(employee => employee.SalaryPerMonth);
        }

        public async Task<decimal> GetMonthlyExpenses(MonthlyLedger monthlyLedger)
        {
            return await GetProductExpences(monthlyLedger) + await GetStuffExpences() + _RENT_COST;
        }

        public decimal GetTotal(MonthlyLedger monthlyLedger)
        {
            return monthlyLedger.Income - monthlyLedger.Expenses;
        }

        //public async Task<bool> MonthlyLedgerExists(MonthlyLedger monthlyLedger)
        //{
        //    return await _context.MonthlyLedgers.FirstOrDefaultAsync(monthlyL => monthlyL.Year == monthlyLedger.Year && monthlyL.Month == monthlyLedger.Month) is not null;
        //}
    }
}
