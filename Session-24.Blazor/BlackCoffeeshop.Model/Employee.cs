using System.Text.Json.Serialization;

namespace BlackCoffeeshop.Model {
    public class Employee : BaseEntity {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public EmployeeType EmployeeType { get; set; }
        public decimal SalaryPerMonth { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Employee() {
        }


    }
}
