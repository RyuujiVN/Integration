using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.MySql
{
    [Table("salaries")]
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime SalaryMonth { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal BaseSalary { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Bonus { get; set; } = 0;
        [Column(TypeName = "decimal(12,2)")]
        public decimal Deductions { get; set; } = 0;
        [Column(TypeName = "decimal(12,2)")]
        public decimal NetSalary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
    }
}