using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;
using Session_24.Services.Handlers;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionLineController
    {
        private readonly IEntityRepo<Transaction> _transactionRepo;
        private readonly IEntityRepo<TransactionLine> _transactionLineRepo;
        private readonly IEntityRepo<Product> _productRepo;
        private readonly IEntityRepo<Customer> _customerRepo;

        public TransactionLineController(IEntityRepo<Transaction> transactionRepo, IEntityRepo<TransactionLine> transactionLineRepo,
            IEntityRepo<Product> productRepo, IEntityRepo<Customer> customerRepo)
        {
            _transactionRepo = transactionRepo;
            _transactionLineRepo = transactionLineRepo;
            _productRepo = productRepo;
            _customerRepo = customerRepo;
        }


        [HttpGet("lines/{ID}")]
        public async Task<IEnumerable<TransactionLineViewModel>> GetTransactionLines(int Id)
        {
            var result = await _transactionLineRepo.GetAllAsync();

            TransactionLineListViewModel transactionLineListViewModel = new TransactionLineListViewModel();

            foreach (var transactionLine in result)
            {
                if (transactionLine.TransactionID == Id)
                {
                    var ProductDescription = (await _productRepo.GetByIdAsync(transactionLine.ProductID)).Description;
                    transactionLineListViewModel.TransactionLineViewModels.Add(new TransactionLineViewModel()
                    {
                        ID = transactionLine.ID,
                        ProductDescription = ProductDescription,
                        TransactionID = transactionLine.TransactionID,
                        Quantity = transactionLine.Quantity,
                        Price = transactionLine.Price,
                        Discount = transactionLine.Discount,
                        TotalPrice = transactionLine.TotalPrice

                    });
                }
            }
            return transactionLineListViewModel.TransactionLineViewModels;
        }
        [HttpGet("product/{ID}")]
        public async Task<ProductTransctionViewModel> GetProductById(int Id)
        {
            var result = await _productRepo.GetByIdAsync(Id);

            ProductTransctionViewModel productViewModel = new ProductTransctionViewModel()
            {
                Code = result.Code,
                Description = result.Description,
                ID = result.ID,
                Cost = result.Cost,
                Price = result.Price
            };
            return productViewModel;

        }

        [HttpGet("products")]
        public async Task<ProductTransctionListViewModel> GetProducts()
        {
            var result = await _productRepo.GetAllAsync();

            ProductTransctionListViewModel productTransactionListViewModel = new();

            foreach (var product in result)
            {

                productTransactionListViewModel.ProductViewModels.Add(new ProductTransctionViewModel()
                {
                    ID = product.ID,
                    Code = product.Code,
                    Cost = product.Cost,
                    Price = product.Price,
                    Description = product.Description

                });
            }
            return productTransactionListViewModel;
        }


        [HttpPost]
        public async Task Post(List<TransactionLineViewModel> transactionLines)
        {
            int employeeID = transactionLines[0].EmployeeID;
            var handler = new TransactionHandler();
            var customer = _customerRepo.GetAllAsync();
            var newTransaction = new Transaction()
            {
                TotalPrice = handler.GetTotal(transactionLines),
                CustomerID = customer.Id,
                Date = DateTime.Now,
                PaymentMethod = PaymentMethod.Cash,
                TotalCost = handler.GetTotalCost(transactionLines),
                EmployeeID = employeeID,
            };
            await _transactionRepo.CreateAsync(newTransaction);

            var transactions = await _transactionRepo.GetAllAsync();
            int newTransactionID = transactions.FirstOrDefault(x => x.TransactionLines.Count<1).ID;
            foreach(var transactionLine in transactionLines)
            {
                await _transactionLineRepo.CreateAsync(new TransactionLine()
                {
                    TransactionID = newTransactionID,
                    Discount = transactionLine.Discount,
                    Price = transactionLine.Price,
                    ProductID = transactionLine.ProductID,
                    Quantity = transactionLine.Quantity,
                    TotalPrice = transactionLine.TotalPrice
                });
            }
        }
    }
}
