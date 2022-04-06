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
    public class TransactionHandler
    {
        private readonly ApplicationContext _context;

        public TransactionHandler(ApplicationContext context)
        {
            _context = context;
        }

        public decimal GetTotal(Transaction transaction)
        {
            return transaction.TransactionLines.Sum(transactionLine => transactionLine.Price);
        }

        private bool CheckDiscountAvailibility(Transaction transaction)
        {
            return transaction.TransactionLines.Sum(transactionLine => transactionLine.Price) > 10;
        }

        public void CalculateDiscount(Transaction transaction)
        {
            if (CheckDiscountAvailibility(transaction))
            {
                transaction.TransactionLines.Select(transactionLine => transactionLine.Discount = transactionLine.Price * 0.15m);
            }
        }

        public void CalculateTransactionLinesTotalPrice(Transaction transaction)
        {
            transaction.TransactionLines.Select(transactionLine => transactionLine.TotalPrice = transactionLine.Price - transactionLine.Discount);
        }

        public void CalculateTransactionLinePrice(TransactionLine transactionLine)
        {
            transactionLine.Price = _context.Products.SingleOrDefault(product => product.ID == transactionLine.ProductID).Price * transactionLine.Quantity;
        }

        public bool IsCreditCardRequired(Transaction transaction)
        {
            return transaction.TotalPrice > 50;
        }
    }
}
