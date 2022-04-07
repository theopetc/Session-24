
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Blazor.Shared
{
    public class ProductCategoryViewModel
    {
        [MinLength(1)]
        public string Description { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
        [MinLength(1)]
        public string Code { get; set; }
        public int ID { get; set; }
    }

    public class ProductCategoryListViewModel
    {
        public List<ProductCategoryViewModel> ProductCategories { get; set; } = new List<ProductCategoryViewModel>();
    }

}
