using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Interfaces;
using api.Dtos.Salary;
using api.Models.MySql;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class SalaryRepository:ISalaryRepository
    {
        private readonly MySqlDBContext _context;
        public SalaryRepository(MySqlDBContext context)
        {
            _context = context;
        }
        public async Task<List<SalaryDto>> GetAllSalariesAsync()
        {
            return await _context.Salaries
                .Select(s => new SalaryDto
                {
                    EmployeeID = s.EmployeeID,
                    SalaryID = s.SalaryID,
                    SalaryMonth = s.SalaryMonth,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Deductions = s.Deductions,
                    NetSalary = s.NetSalary,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();
        }
        public async Task<SalaryDto?> GetSalaryByIdAsync(int idSalary)
        {
            var salary = await _context.Salaries
                .Where(s => s.SalaryID == idSalary)
                .Select(s => new SalaryDto
                {
                    EmployeeID = s.EmployeeID,
                    SalaryID = s.SalaryID,
                    SalaryMonth = s.SalaryMonth,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Deductions = s.Deductions,
                    NetSalary = s.NetSalary,
                    CreatedAt = s.CreatedAt
                })
                .FirstOrDefaultAsync();
            if (salary == null)
            {
                return null;
            }
            return salary;
        }
        public async Task<SalaryDto?> AddSalaryAsync(CreateSalaryDto salaryDto)
        {
           
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == salaryDto.EmployeeID);
            if (existingEmployee == null)
            {
                return null; 
            }
            decimal netSalary = salaryDto.BaseSalary + salaryDto.Bonus - salaryDto.Deductions;
            var salaryModel = new Salary
            {
                SalaryID = salaryDto.SalaryID,
                EmployeeID = salaryDto.EmployeeID,
                SalaryMonth = salaryDto.SalaryMonth,
                BaseSalary = salaryDto.BaseSalary,
                Bonus = salaryDto.Bonus,
                Deductions = salaryDto.Deductions,
                NetSalary = netSalary,
                CreatedAt = DateTime.Now
            };

            await _context.Salaries.AddAsync(salaryModel);
            await _context.SaveChangesAsync();
            var returnDto = new SalaryDto
            {
                SalaryID = salaryModel.SalaryID,
                EmployeeID = salaryModel.EmployeeID,
                SalaryMonth = salaryModel.SalaryMonth,
                BaseSalary = salaryModel.BaseSalary,
                Bonus = salaryModel.Bonus,
                Deductions = salaryModel.Deductions,
                NetSalary = netSalary,
                CreatedAt = salaryModel.CreatedAt
            };
            return returnDto;
        }
        public async Task<SalaryDto?> UpdateSalaryAsync(UpdateSalaryDto salaryDto, int idSalary)
        {
            var salary = await _context.Salaries.FindAsync(idSalary); 
            decimal netSalary = salaryDto.BaseSalary + salaryDto.Bonus - salaryDto.Deductions;
            salary.SalaryMonth = salaryDto.SalaryMonth;
            salary.BaseSalary = salaryDto.BaseSalary;
            salary.Bonus = salaryDto.Bonus;
            salary.Deductions = salaryDto.Deductions;
            salary.NetSalary = netSalary;
            await _context.SaveChangesAsync();
            return new SalaryDto
            {
                SalaryID = salary.SalaryID,
                EmployeeID = salary.EmployeeID,
                SalaryMonth = salary.SalaryMonth,
                BaseSalary = salary.BaseSalary,
                Bonus = salary.Bonus,
                Deductions = salary.Deductions,
                NetSalary = netSalary,
                CreatedAt = DateTime.Now
            };
        }
        public async Task<bool> DeleteSalaryAsync(int idSalary)
        {
            var salary = await _context.Salaries.FindAsync(idSalary);
            if (salary == null)
            {
                return false;
            }
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}