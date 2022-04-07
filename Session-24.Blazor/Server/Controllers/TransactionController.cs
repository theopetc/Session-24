using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController
    {
        private readonly IEntityRepo<Transaction> _transactionRepo;
        private readonly IEntityRepo<Customer> _customerRepo;
        private readonly IEntityRepo<Employee> _employeeRepo;
        private readonly IEntityRepo<TransactionLine> _transactionLineRepo;
        private readonly IEntityRepo<Product> _productRepo;

        public TransactionController(IEntityRepo<Transaction> transactionRepo, IEntityRepo<Customer> customerRepo,
            IEntityRepo<Employee> employeeRepo, IEntityRepo<TransactionLine> transactionLineRepo, IEntityRepo<Product> productLineRepo)
        {
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
            _employeeRepo = employeeRepo;
            _transactionLineRepo = transactionLineRepo;
            _productRepo = productLineRepo;
        }


        [HttpGet]
        public async Task<TransactionListViewModel> Get()
        {
            var result = await _transactionRepo.GetAllAsync();
            var employees = await _employeeRepo.GetAllAsync();
            TransactionListViewModel transactionListViewModel = new TransactionListViewModel();

            foreach (var employee in employees)
            {
                transactionListViewModel.Emlpoyees.Add(new EmployeeViewModel()
                {
                    ID = employee.ID,
                    Name = employee.FullName,
                    Surname = employee.Surname,
                    EmployeeType = employee.EmployeeType,
                    SalaryPerMonth = employee.SalaryPerMonth
                });
            }

            foreach (var transaction in result)
            {
                var EmployeeName = (await _employeeRepo.GetByIdAsync(transaction.EmployeeID)).FullName;
                var CustomerCode = (await _customerRepo.GetByIdAsync(transaction.CustomerID)).Description;
                transactionListViewModel.TransactionViewModels.Add(new TransactionViewModel()
                {
                    ID = transaction.ID,
                    Date = transaction.Date,
                    EmployeeName = EmployeeName,
                    CustomerDescription = CustomerCode,
                    PaymentMethod = transaction.PaymentMethod,
                    TotalPrice = transaction.TotalPrice,
                    TotalCost = transaction.TotalCost

                });
            }
            return transactionListViewModel;

        }

        [HttpDelete("{ID}")]
        public async Task Delete(int id)
        {
            if (await _transactionRepo.GetByIdAsync(id) is null) return;

            await _transactionRepo.DeleteAsync(id);
        }
    }
}

