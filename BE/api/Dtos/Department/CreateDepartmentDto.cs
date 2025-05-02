using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Department
{
    public class CreateDepartmentDto
    {
        public int DepartmentID {get; set;}
        public string DepartmentName { get; set; } = string.Empty;
    }
}