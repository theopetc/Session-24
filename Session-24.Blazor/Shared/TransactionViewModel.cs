using BlackCoffeeshop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Blazor.Shared
{
    public class TransactionViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerDescription { get; set; }
        public List<TransactionLine> TransactionLines { get; set; } = new List<TransactionLine>();
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalCost { get; set; }
    }


    public class TransactionListViewModel
    {
        public List<TransactionViewModel> TransactionViewModels { get; set; } = new List<TransactionViewModel>();
        public List<EmployeeViewModel> Emlpoyees { get; set; } = new List<EmployeeViewModel>();

    }

    public class TransactionDetailsViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public List<TransactionLine> TransactionLines = new();

    }

    public class TransactionEditViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerDescription { get; set; }
        public List<TransactionLine> TransactionLines { get; set; } = new List<TransactionLine>();
        public List<Employee> Emlpoyees { get; set; } = new List<Employee>();
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalCost { get; set; }
    }
}
