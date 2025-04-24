using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Dtos.Employee;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MySqlDBContext _mySqlDbContext;
        private readonly SqlServerDBContext _sqlServerDbContext;

        public EmployeeRepository(MySqlDBContext mySqlDbContext, SqlServerDBContext sqlServerDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
            _sqlServerDbContext = sqlServerDbContext;
        }

        public Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var mySqlEmployees = _mySqlDbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .ToList();

            var sqlServerEmployees = _sqlServerDbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .ToList();

            var listEmployeeDto = new List<EmployeeDto>();
            foreach (var mySqlEmployee in mySqlEmployees)
            {
                foreach (var sqlServerEmployee in sqlServerEmployees)
                {
                    if (mySqlEmployee.EmployeeID == sqlServerEmployee.EmployeeID)
                    {
                        var employeeDto = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
                        listEmployeeDto.Add(employeeDto);
                        break;
                    }
                }
            }
            return Task.FromResult(listEmployeeDto);
        }

        public Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var mySqlEmployee = _mySqlDbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefault(e => e.EmployeeID == id);

            var sqlServerEmployee = _sqlServerDbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefault(e => e.EmployeeID == id);

            if (mySqlEmployee != null && sqlServerEmployee != null)
            {
                EmployeeDto employeeDto = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
                return Task.FromResult<EmployeeDto?>(employeeDto);
            }
            return Task.FromResult<EmployeeDto?>(null);
        }

        public Task<EmployeeDto> AddAsync(EmployeeUpdateAndCreateDto employeeDto, int id)
        {
            var mySqlEmployee = employeeDto.toCreateMySqlEmployee(id);
            _mySqlDbContext.Employees.Add(mySqlEmployee);
            _mySqlDbContext.SaveChanges();

            var sqlServerEmployee = employeeDto.toCreateSqlServerEmployee(id);
            _sqlServerDbContext.Employees.Add(sqlServerEmployee);
            _sqlServerDbContext.SaveChanges();

            var result = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
            return Task.FromResult(result);
        }

        public Task<EmployeeDto?> UpdateAsync(EmployeeUpdateAndCreateDto employeeDto, int id)
        {
            var mySqlEmployee = _mySqlDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);
            var sqlServerEmployee = _sqlServerDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);
            if (mySqlEmployee != null && sqlServerEmployee != null)
            {
                mySqlEmployee.FullName = employeeDto.FullName;
                mySqlEmployee.DepartmentID = employeeDto.DepartmentID;
                mySqlEmployee.PositionID = employeeDto.PositionID;
                mySqlEmployee.Status = employeeDto.Status;

                sqlServerEmployee.FullName = employeeDto.FullName;
                sqlServerEmployee.DateOfBirth = employeeDto.DateOfBirth;
                sqlServerEmployee.Gender = employeeDto.Gender;
                sqlServerEmployee.PhoneNumber = employeeDto.PhoneNumber;
                sqlServerEmployee.Email = employeeDto.Email;
                sqlServerEmployee.HireDate = employeeDto.HireDate;
                sqlServerEmployee.DepartmentID = employeeDto.DepartmentID;
                sqlServerEmployee.PositionID = employeeDto.PositionID;
                sqlServerEmployee.Status = employeeDto.Status;
                sqlServerEmployee.UpdatedAt = DateTime.Now;
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();

                var updatedEmployee = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
                return Task.FromResult<EmployeeDto?>(updatedEmployee);
            }
            return Task.FromResult<EmployeeDto?>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var mySqlEmployee = _mySqlDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);
            var sqlServerEmployee = _sqlServerDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

            if (mySqlEmployee != null && sqlServerEmployee != null)
            {
                _mySqlDbContext.Employees.Remove(mySqlEmployee);
                _sqlServerDbContext.Employees.Remove(sqlServerEmployee);
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}