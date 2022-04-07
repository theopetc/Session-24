
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;
using Session_24.Services.Repository;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase 
    {
        private readonly IEntityRepo<ProductCategory> _productCatRepo;

        public ProductCategoryController(IEntityRepo<ProductCategory> productCatRepo)
        {
            _productCatRepo = productCatRepo; 
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategoryViewModel>> Get()
        {
            var result = await _productCatRepo.GetAllAsync();
            return result.Select(x => new ProductCategoryViewModel
            {
                ID = x.ID,
                Description = x.Description,
                ProductType = (Shared.ProductType)x.ProductType,
                Code = x.Code
            });
        }
        
        [HttpGet("{id}")]
        public async Task<ProductCategoryViewModel> Get(int id)
        {
            ProductCategoryViewModel model = new();
            if (id != 0)
            {
                var existing = await _productCatRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Description = existing.Description;
                model.ProductType = (Shared.ProductType)existing.ProductType;
                model.Code = existing.Code;
              
            }
            return model;
        }

        [HttpDelete("{ID}")]
        public async Task Delete(int id)
        {
            await _productCatRepo.DeleteAsync(id);
        }

        [HttpPost]
        public async Task Post(ProductCategoryViewModel productCatView)
        {
            var newProductCat = new ProductCategory()
                {
                ID = productCatView.ID,
                Description= productCatView.Description,
                Code = productCatView.Code,
                ProductType= (BlackCoffeeshop.Model.ProductType)productCatView.ProductType
                };

            await _productCatRepo.CreateAsync(newProductCat);
        }

        [HttpPut]
        public async Task<ActionResult> Put(ProductCategoryViewModel productCatView)
        {
            var itemToUpdate = await _productCatRepo.GetByIdAsync(productCatView.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.Description = productCatView.Description;
            itemToUpdate.Code = productCatView.Code;
            itemToUpdate.ProductType = (BlackCoffeeshop.Model.ProductType)productCatView.ProductType;
            await _productCatRepo.UpdateAsync(productCatView.ID, itemToUpdate);
            return Ok();
        }

        [HttpGet("[Action]/{id}")]
        public async Task<ProductCategoryViewModel> GetViewModel(int id)
        {
            ProductCategoryViewModel model = new();
            if (id != 0)
            {
                var existing = await _productCatRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Description = existing.Description;
                model.ProductType = (Shared.ProductType)existing.ProductType;
                model.Code = existing.Code;

            }
            return model;
        }
    }

}
