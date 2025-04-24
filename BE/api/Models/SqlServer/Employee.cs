using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.SqlServer
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(10)]
        public string? Gender { get; set; }
        
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        
        [StringLength(100)]
        public string? Email { get; set; }
        
        [Required]
        public DateTime HireDate { get; set; }
        
        public int? DepartmentID { get; set; }
        
        public int? PositionID { get; set; }
        
        [StringLength(50)]
        public string? Status { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }
        
        [ForeignKey("PositionID")]
        public virtual Position? Position { get; set; }
    }
}