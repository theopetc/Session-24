using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Services.Handlers
{
    public class EmployeeHandler
    {
        private readonly ApplicationContext _context;

        public EmployeeHandler(ApplicationContext context)
        {
            _context = context;
        }

        public bool CheckAddAvailiability(Employee employee)
        {
            switch (employee.EmployeeType)
            {
                case EmployeeType.Manager:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Manager).Count() < 1;
                case EmployeeType.Cashier:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Cashier).Count() < 2;
                case EmployeeType.Barista:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Barista).Count() < 2;
                default:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Waiter).Count() < 3;
            }
        }

        public bool CheckDeleteAvailiability(Employee employee)
        {
            switch (employee.EmployeeType)
            {
                case EmployeeType.Manager:
                    return false;
                case EmployeeType.Cashier:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Cashier).Count() > 1;
                case EmployeeType.Barista:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Barista).Count() > 1;
                default:
                    return _context.Employees.Where(employee => employee.EmployeeType == EmployeeType.Waiter).Count() > 1;
            }
        }

        public bool CheckUpdateAvailibility(Employee oldEmployee, Employee newEmployee)
        {
            if (oldEmployee.EmployeeType == newEmployee.EmployeeType)
                return true;
            return CheckAddAvailiability(newEmployee) && CheckDeleteAvailiability(oldEmployee);
        }
    }
}
