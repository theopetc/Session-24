using BlackCoffeeshop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Blazor.Shared
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ProductCategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public ProductCategory ProductCategory { get; set; }        
        //public List<TransactionLine> TransactionLines { get; set; }
    }
    public class ProductEditViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ProductCategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        //public List<TransactionLine> TransactionLines { get; set; }
    }

    public class ProductListViewModel
    {
        public List<ProductViewModel> ProductViewModels { get; set; } = new List<ProductViewModel>();
    }
}
