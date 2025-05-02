using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Salary
{
    public class UpdateSalaryDto
    {
        
        public DateTime SalaryMonth { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; } = 0;
        public decimal Deductions { get; set; } = 0;
    }
}