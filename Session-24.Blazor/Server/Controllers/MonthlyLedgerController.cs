
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;
using Session_24.Services.Handlers;
using Session_24.Services.Repository;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthlyLedgerController : ControllerBase
    {
        private readonly IEntityRepo<MonthlyLedger> _monthlyledgerRepo;
        private MonthlyLedgerHandler _monthlyLedgerHandler;

        public MonthlyLedgerController(IEntityRepo<MonthlyLedger> monthlyledgerRepo, MonthlyLedgerHandler monthlyLedgerHandle)
        {
            _monthlyledgerRepo = monthlyledgerRepo;
            _monthlyLedgerHandler = monthlyLedgerHandle;
        }

        [HttpGet]
        public async Task<IEnumerable<MonthlyLedgerViewModel>> Get()
        {
            var result = await _monthlyledgerRepo.GetAllAsync();
            return result.Select(x => new MonthlyLedgerViewModel
            {
                ID = x.ID,
                Year = x.Year,
                Month = x.Month,
                Income = x.Income,
                Expenses = x.Expenses,
                Total = x.Total
            });
        }

        [HttpDelete("{ID}")]
        public async Task Delete(int id)
        {
             await _monthlyledgerRepo.DeleteAsync(id);
        }

        [HttpPost]
        public async Task Post(MonthlyLedgerViewModel monthlyLedgerView)
        {

            var newMonthly = new MonthlyLedger()
            {
                ID = monthlyLedgerView.ID,
                Year = monthlyLedgerView.Year,
                Month = monthlyLedgerView.Month
            };

            newMonthly.Income = await _monthlyLedgerHandler.GetIncome(newMonthly);
            newMonthly.Expenses = await _monthlyLedgerHandler.GetMonthlyExpenses(newMonthly);
            newMonthly.Total = _monthlyLedgerHandler.GetTotal(newMonthly);
            if (await _monthlyLedgerHandler.MonthlyLedgerExists(newMonthly))
                return;
            await _monthlyledgerRepo.CreateAsync(newMonthly);
        }

        [HttpGet("[Action]/{id}")]
        public async Task<MonthlyLedgerViewModel> GetViewModel(int id)
        {
            MonthlyLedgerViewModel model = new();
            if (id != 0)
            {
                var existing = await _monthlyledgerRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Year = existing.Year;
                model.Month = existing.Month;
                model.Expenses = existing.Expenses;
                model.Total = existing.Total;
                model.Income = existing.Income;

            }
            return model;
        }

    }
}
