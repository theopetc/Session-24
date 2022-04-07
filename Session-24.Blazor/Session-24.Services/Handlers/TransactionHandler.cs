using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Session_24.Blazor.Shared;

namespace Session_24.Services.Handlers
{
    public class TransactionHandler
    {
        //private readonly ProductTransctionListViewModel _productTransactionList;
        private ApplicationContext _context;


        public TransactionHandler()
        {
           
        }

        public decimal GetTotal(List<TransactionLineViewModel> transactionLines)
        {
            return transactionLines.Sum(transactionLine => transactionLine.TotalPrice);
        }

        public decimal GetTotalCost(List<TransactionLineViewModel> transactionLines)
        {
            return transactionLines.Sum(transactionLine => transactionLine.Cost);
        }

        private bool CheckDiscountAvailibility(List<TransactionLineViewModel> transactionLines)
        {
            return transactionLines.Sum(transactionLine => transactionLine.Price) > 10;
        }

        public void CalculateDiscount(List<TransactionLineViewModel> transactionLines)
        {
            if (CheckDiscountAvailibility(transactionLines))
            {
                foreach (var transactionLine in transactionLines)
                {
                    transactionLine.Discount = transactionLine.Price * 0.15m;
                }
            }
        }

        public void CalculateTransactionLinesTotalPrice(List<TransactionLineViewModel> transactionLines)
        {
            foreach(var transactionLine in transactionLines)
            {
                transactionLine.TotalPrice = transactionLine.Price - transactionLine.Discount;
            }
        }

        public void  CalculateTransactionLinePrice(TransactionLineViewModel transactionLine, decimal productPrice )
        {
            transactionLine.Price = productPrice * transactionLine.Quantity;
        }

        public bool IsCreditCardRequired(Transaction transaction)
        {
            return transaction.TotalPrice > 50;
        }
    }
}
