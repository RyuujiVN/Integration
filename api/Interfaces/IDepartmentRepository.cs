using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto?>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> AddAsync(DepartmentUpdateAndCreateDto departmentDto, int id);
        Task<DepartmentDto?> UpdateDepartmentAsync(DepartmentUpdateAndCreateDto departmentDto, int id);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}