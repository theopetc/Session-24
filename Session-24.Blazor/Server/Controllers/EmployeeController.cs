using BlackCoffeeshop.EF.Repository;
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.Mvc;
using Session_24.Blazor.Shared;
using Session_24.Services.Handlers;

namespace Session_24.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEntityRepo<Employee> _employeeRepo;

        public EmployeeController(IEntityRepo<Employee> entityRepo)
        {
            _employeeRepo = entityRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeViewModel>> Get()
        {
            var result = await _employeeRepo.GetAllAsync();
            return result.Select(x => new EmployeeViewModel
            {
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                EmployeeType = x.EmployeeType,
                SalaryPerMonth = x.SalaryPerMonth
            });
        }

        [HttpGet("{id}")]
        public async Task<EmployeeViewModel> Get(int id)
        {
            EmployeeViewModel model = new();
            if (id != 0)
            {
                var existing = await _employeeRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.SalaryPerMonth = existing.SalaryPerMonth;
                model.EmployeeType = existing.EmployeeType;

            }
            return model;
        }

        [HttpPost]
        public async Task Post(EmployeeViewModel employeeViewModel)
        {
            var newEmployee = new Employee()
            {
                ID = employeeViewModel.ID,
                Name= employeeViewModel.Name,
                Surname= employeeViewModel.Surname,
                SalaryPerMonth = employeeViewModel.SalaryPerMonth,
                EmployeeType = employeeViewModel.EmployeeType
            };
            var employees = await _employeeRepo.GetAllAsync();
            var employeeHandler = new EmployeeHandler(employees.ToList());
            if (!employeeHandler.CheckAddAvailiability(newEmployee.EmployeeType))
                return;
            await _employeeRepo.CreateAsync(newEmployee);
        }

        [HttpPut]
        public async Task<ActionResult> Put(EmployeeViewModel employeeViewModel)
        {
            var itemToUpdate = await _employeeRepo.GetByIdAsync(employeeViewModel.ID);
            if (itemToUpdate is null) return NotFound();

            var oldEmployeeType = itemToUpdate.EmployeeType;
            itemToUpdate.Name = employeeViewModel.Name;
            itemToUpdate.Surname = employeeViewModel.Surname;
            itemToUpdate.SalaryPerMonth = employeeViewModel.SalaryPerMonth;
            itemToUpdate.EmployeeType = employeeViewModel.EmployeeType;

            var employees = await _employeeRepo.GetAllAsync();
            var employeeHandler = new EmployeeHandler(employees.ToList());
            
            if (!employeeHandler.CheckUpdateAvailibility(oldEmployeeType, itemToUpdate.EmployeeType))
                return BadRequest();

            await _employeeRepo.UpdateAsync(employeeViewModel.ID, itemToUpdate);
            return Ok();
        }

        [HttpDelete("{ID}")]
        public async Task Delete(int id)
        {
            var selectedEmployee = await _employeeRepo.GetByIdAsync(id);
            if (selectedEmployee is null)
            {
                return;
            }
            var employees = await _employeeRepo.GetAllAsync();
            var employeeHandler = new EmployeeHandler(employees.ToList());
            if (!employeeHandler.CheckDeleteAvailiability(selectedEmployee.EmployeeType))
                return;
            await _employeeRepo.DeleteAsync(id);
        }
    }
}
