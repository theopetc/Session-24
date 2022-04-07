using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_24.Blazor.Shared
{
    public class MonthlyLedgerViewModel
    {
        public int ID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Total { get; set; }
    }

    public class MonthlyLedgerListViewModel
    {
        public List<MonthlyLedgerViewModel> MonthlyLedgers { get; set; } = new List<MonthlyLedgerViewModel>();
    }
}
