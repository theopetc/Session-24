
namespace BlackCoffeeshop.Model;
public class Transaction : BaseEntity {
    public DateTime Date { get; set; }
    public int EmployeeID { get; set; }
    public int CustomerID { get; set; }
    public List<TransactionLine> TransactionLines { get; set; } = new List<TransactionLine>();
    public PaymentMethod PaymentMethod { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalCost { get; set; }
    
    public Customer Customer { get; set; }
    public Employee Employee { get; set; }
    
}
