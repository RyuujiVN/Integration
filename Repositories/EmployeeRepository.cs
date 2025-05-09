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
            var mySqlEmployees = _mySqlDbContext.Employees.ToList();

            var sqlServerEmployees = _sqlServerDbContext.Employees.ToList();

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
            var mySqlEmployee = _mySqlDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

            var sqlServerEmployee = _sqlServerDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

            if (mySqlEmployee != null && sqlServerEmployee != null)
            {
                EmployeeDto employeeDto = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
                return Task.FromResult<EmployeeDto?>(employeeDto);
            }
            return Task.FromResult<EmployeeDto?>(null);
        }

        public async Task<EmployeeDto> AddAsync(CreateEmployeeDto employeeDto)
        {
            var existingEmail = await _sqlServerDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == employeeDto.Email);

            if (existingEmail != null)
            {
                throw new InvalidOperationException($"Email {employeeDto.Email} đã tồn tại trong hệ thống.");
            }

            if (employeeDto.DepartmentID.HasValue)
            {
                var departmentExists = await _sqlServerDbContext.Departments
                    .AnyAsync(d => d.DepartmentID == employeeDto.DepartmentID);
                if (!departmentExists)
                {
                    throw new InvalidOperationException($"Phòng ban với ID {employeeDto.DepartmentID} không tồn tại.");
                }
            }

            if (employeeDto.PositionID.HasValue)
            {
                var positionExists = await _sqlServerDbContext.Positions
                    .AnyAsync(p => p.PositionID == employeeDto.PositionID);
                if (!positionExists)
                {
                    throw new InvalidOperationException($"Chức vụ với ID {employeeDto.PositionID} không tồn tại.");
                }
            }

            using var mySqlTransaction = await _mySqlDbContext.Database.BeginTransactionAsync();
            try
            {
                var mySqlEmployee = employeeDto.toCreateMySqlEmployee();
                _mySqlDbContext.Employees.Add(mySqlEmployee);
                await _mySqlDbContext.SaveChangesAsync();

                using var sqlServerTransaction = await _sqlServerDbContext.Database.BeginTransactionAsync();
                try
                {
                    var sqlServerEmployee = employeeDto.toCreateSqlServerEmployee();

                    var entry = _sqlServerDbContext.Entry(sqlServerEmployee);
                    entry.State = EntityState.Added;

                    await _sqlServerDbContext.SaveChangesAsync();

                    await sqlServerTransaction.CommitAsync();
                    await mySqlTransaction.CommitAsync();

                    var result = EmployeeMapper.toEmployeeDto(mySqlEmployee, sqlServerEmployee);
                    return result;
                }
                catch (Exception ex)
                {
                    await sqlServerTransaction.RollbackAsync();
                    throw new Exception($"Lỗi khi thêm vào SQL Server: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                await mySqlTransaction.RollbackAsync();
                throw new Exception($"Lỗi khi thêm nhân viên: {ex.Message}", ex);
            }
        }

        public Task<EmployeeDto?> UpdateAsync(UpdateEmployeeDto employeeDto, int id)
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