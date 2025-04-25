using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Employee
{
    public class UpdateEmployeeDto
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime HireDate { get; set; }
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public string? Status { get; set; }
    }
}