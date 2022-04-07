using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase 
    {
        private readonly IEntityRepo<Product> _productRepo;
        private readonly IEntityRepo<ProductCategory> _productCategoryRepo;

        public ProductController(IEntityRepo<Product> productRepo, IEntityRepo<ProductCategory> productCategoryRepo)
        {
            _productRepo = productRepo;
            _productCategoryRepo = productCategoryRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            var result = await _productRepo.GetAllAsync();
            return result.Select(x => new ProductViewModel
            {
                ID = x.ID,
                Description = x.Description,
                ProductCategoryID = x.ProductCategoryID,
                Code = x.Code,
                Cost = x.Cost,
                Price = x.Price,

                ProductCategory = new ProductCategory()
                {
                    ID = x.ProductCategoryID,
                    Code = x.ProductCategory.Code,
                    ProductType = x.ProductCategory.ProductType,
                    Description = x.ProductCategory.Description

                }

            }) ;
        }

        [HttpGet("{id}")]
        public async Task<ProductEditViewModel> Get(int id)
        {
            
            ProductEditViewModel model = new();
            if (id != 0)
            {
                var existing = await _productRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Description = existing.Description;
                model.ProductCategoryID = existing.ProductCategoryID;
                model.Code = existing.Code;
                model.Cost = existing.Cost;
                model.Price = existing.Price;

                var allProductCategories = await _productCategoryRepo.GetAllAsync();

                foreach (var item in allProductCategories)
                {
                    model.ProductCategories.Add(new ProductCategory()
                    {
                        Code = item.Code,
                        ID = item.ID,
                        ProductType = item.ProductType,
                        Description = item.Description
                    });
                }
            }
            return model;
        }

        [HttpPost]
        public async Task Post(ProductEditViewModel productView)
        {
            var newProduct = new Product()
            {
                ID = productView.ID,
                Description = productView.Description,
                Code = productView.Code,
                ProductCategoryID = productView.ProductCategoryID,
                Cost = productView.Cost,
                Price= productView.Price,
            };

            //var allProductCategories = await _productCategoryRepo.GetAllAsync();

            //foreach (var item in allProductCategories)
            //{
            //    productView.ProductCategories.Add(new ProductCategory()
            //    {
            //        Code = item.Code,
            //        ID = item.ID,
            //        ProductType = item.ProductType,
            //        Description = item.Description
            //    });
            //}

            await _productRepo.CreateAsync(newProduct);
        }

        [HttpPut]
        public async Task<ActionResult> Put(ProductEditViewModel productView)
        {
            var itemToUpdate = await _productRepo.GetByIdAsync(productView.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.ID = productView.ID;
            itemToUpdate.Description = productView.Description;
            itemToUpdate.Code = productView.Code;
            itemToUpdate.ProductCategoryID = productView.ProductCategoryID;
            itemToUpdate.Cost = productView.Cost;
            itemToUpdate.Price = productView.Price;

            //var allProductCategories = await _productCategoryRepo.GetAllAsync();

            //foreach (var item in allProductCategories)
            //{
            //    productView.ProductCategories.Add(new ProductCategory()
            //    {
            //        Code = item.Code,
            //        ID = item.ID,
            //        ProductType = item.ProductType,
            //        Description = item.Description
            //    });
            //}

            await _productRepo.UpdateAsync(productView.ID, itemToUpdate);
            return Ok();
        }

        [HttpDelete("{ID}")] 
        public async Task Delete(int id)
        { 
            var selectedEmployee = await _productRepo.GetByIdAsync(id); 
            if (selectedEmployee is null) { return; } 
            
            
            await _productRepo.DeleteAsync(id);
        }
    }
}
