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
        private readonly List<Employee> _context;

        public EmployeeHandler(List<Employee> context)
        {
            _context = context;
        }

        public bool CheckAddAvailiability(EmployeeType employee)
        {
            switch (employee)
            {
                case EmployeeType.Manager:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Manager).Count() < 1;
                case EmployeeType.Cashier:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Cashier).Count() < 2;
                case EmployeeType.Barista:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Barista).Count() < 2;
                default:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Waiter).Count() < 3;
            }
        }

        public bool CheckDeleteAvailiability(EmployeeType employee)
        {
            switch (employee)
            {
                case EmployeeType.Manager:
                    return false;
                case EmployeeType.Cashier:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Cashier).Count() > 1;
                case EmployeeType.Barista:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Barista).Count() > 1;
                default:
                    return _context.Where(employee => employee.EmployeeType == EmployeeType.Waiter).Count() > 1;
            }
        }

        public bool CheckUpdateAvailibility(EmployeeType oldEmployee, EmployeeType newEmployee)
        {
            if (oldEmployee == newEmployee)
                return true;
            return CheckAddAvailiability(newEmployee) && CheckDeleteAvailiability(oldEmployee);
        }
    }
}
