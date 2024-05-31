using API.Models.Enums;

namespace API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeAddress { get; set; }
        public AddressCode AddressCode { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public EmployeeAuth? EmployeeAuth { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CashPayment>? CashPayments { get; internal set; }
    }
}
