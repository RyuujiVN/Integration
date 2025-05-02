using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Salary;
using api.Models.MySql;

namespace api.Mappers
{
    public static class SalaryMapper
    {
        public static SalaryDto toSalaryDto(this Salary salary)
        {
            return new SalaryDto
            {
                SalaryID = salary.SalaryID,
                EmployeeID = salary.EmployeeID,
                SalaryMonth = salary.SalaryMonth,
                BaseSalary = salary.BaseSalary,
                Bonus = salary.Bonus,
                Deductions = salary.Deductions,
                CreatedAt = salary.CreatedAt
            };
        }
        public static Salary toCreateSalary(this CreateSalaryDto salaryDto)
        {
            return new Salary
            {
                SalaryID = salaryDto.SalaryID,
                EmployeeID = salaryDto.EmployeeID,
                SalaryMonth = salaryDto.SalaryMonth,
                BaseSalary = salaryDto.BaseSalary,
                Bonus = salaryDto.Bonus,
                Deductions = salaryDto.Deductions,
                CreatedAt = salaryDto.CreatedAt
            };
        }
        public static Salary toUpdateSalary(this UpdateSalaryDto salaryDto)
        {
            return new Salary
            {
                SalaryMonth = salaryDto.SalaryMonth,
                BaseSalary = salaryDto.BaseSalary,
                Bonus = salaryDto.Bonus,
                Deductions = salaryDto.Deductions,
            };
        }
    }
}