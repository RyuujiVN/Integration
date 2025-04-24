using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;

namespace api.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto toEmployeeDto(api.Models.MySql.Employee mySqlEmployee, api.Models.SqlServer.Employee sqlServerEmployee)
        {
            return new EmployeeDto
            {
                EmployeeID = mySqlEmployee.EmployeeID,
                FullName = mySqlEmployee.FullName,
                DateOfBirth = sqlServerEmployee.DateOfBirth,
                Gender = sqlServerEmployee.Gender,
                PhoneNumber = sqlServerEmployee.PhoneNumber,
                Email = sqlServerEmployee.Email,
                HireDate = sqlServerEmployee.HireDate,
                DepartmentID = mySqlEmployee.DepartmentID,
                PositionID = mySqlEmployee.PositionID,
                Status = mySqlEmployee.Status,
                CreatedAt = sqlServerEmployee.CreatedAt,
                UpdatedAt = sqlServerEmployee.UpdatedAt,
            };
        }
        public static api.Models.MySql.Employee toCreateMySqlEmployee(this EmployeeUpdateAndCreateDto dto, int id)
        {
            return new api.Models.MySql.Employee
            {
                EmployeeID = id,
                FullName = dto.FullName,
                DepartmentID = dto.DepartmentID,
                PositionID = dto.PositionID,
                Status = dto.Status
            };
        }

        public static api.Models.SqlServer.Employee toCreateSqlServerEmployee(this EmployeeUpdateAndCreateDto dto, int id)
        {
            return new api.Models.SqlServer.Employee
            {
                EmployeeID = id,
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                HireDate = dto.HireDate,
                DepartmentID = dto.DepartmentID,
                PositionID = dto.PositionID,
                Status = dto.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}