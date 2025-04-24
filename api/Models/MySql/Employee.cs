using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.MySql
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public string? Status { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        [ForeignKey("PositionID")]
        public virtual Position? Position { get; set; }
    }
}