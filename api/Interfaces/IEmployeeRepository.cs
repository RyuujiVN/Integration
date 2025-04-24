using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;

namespace api.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> AddAsync(EmployeeUpdateAndCreateDto employeeDto, int id);
        Task<EmployeeDto?> UpdateAsync(EmployeeUpdateAndCreateDto employeeDto, int id);
        Task<bool> DeleteAsync(int id);
    }
}