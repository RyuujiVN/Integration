using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Salary;

namespace api.Interfaces
{
    public interface ISalaryRepository
    {
        Task<List<SalaryDto>> GetAllSalariesAsync();
        Task<SalaryDto?> GetSalaryByIdAsync(int idSalary);
        Task<SalaryDto?> AddSalaryAsync(CreateSalaryDto salaryDto);
        Task<SalaryDto?> UpdateSalaryAsync(UpdateSalaryDto salaryDto, int idSalary);
        Task<bool> DeleteSalaryAsync(int idSalary);



    }
}