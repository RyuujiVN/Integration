using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Salary
{
    public class SalaryDto
    {
        public int SalaryID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime SalaryMonth { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; } = 0;
        public decimal Deductions { get; set; } = 0;
        public decimal NetSalary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}