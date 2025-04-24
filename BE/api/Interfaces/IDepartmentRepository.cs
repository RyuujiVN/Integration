using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Department;

namespace api.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto?>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> AddAsync(CreateDepartmentDto departmentDto);
        Task<DepartmentDto?> UpdateDepartmentAsync(UpdateDepartmentDto departmentDto, int id);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}