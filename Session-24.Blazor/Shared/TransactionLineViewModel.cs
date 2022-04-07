using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Blazor.Shared
{
    public class TransactionLineViewModel
    {
        public int EmployeeID { get; set; }
        public int ID { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int ProductID { get; set; }
        public int TransactionID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Cost { get; set; }
    }

    public class ProductTransctionViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }

    public class ProductTransctionListViewModel
    {
        public List<ProductTransctionViewModel> ProductViewModels { get; set; } = new();

    }

    public class TransactionLineListViewModel
    {
        public List<TransactionLineViewModel> TransactionLineViewModels { get; set; } = new();
    }
}
