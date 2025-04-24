using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Dtos;
using api.Dtos.Department;
using api.Interfaces;
using api.Mappers;
using api.Models.MySql;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MySqlDBContext _mySqlDbContext;
        private readonly SqlServerDBContext _sqlServerDbContext;

        public DepartmentRepository(MySqlDBContext mySqlDbContext, SqlServerDBContext sqlServerDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
            _sqlServerDbContext = sqlServerDbContext;
        }

        public Task<List<DepartmentDto?>> GetAllDepartmentsAsync()
        {
            var mySqlDepartments = _mySqlDbContext.Departments.ToList();
            var sqlServerDepartments = _sqlServerDbContext.Departments.ToList();
            List<DepartmentDto?> departmentDtos = new List<DepartmentDto?>();
            foreach (var mySql in mySqlDepartments)
            {
                foreach (var sqlServer in sqlServerDepartments)
                {
                    if (mySql.DepartmentID == sqlServer.DepartmentID)
                    {
                        DepartmentDto addDepartment = DepartmentMapper.toDepartmentDto(mySql, sqlServer);
                        departmentDtos.Add(addDepartment);
                        break;
                    }
                }
            }
            return Task.FromResult(departmentDtos);
        }

        public Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var mySqlDepartments = _mySqlDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            var sqlServerDepartments = _sqlServerDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            if (mySqlDepartments != null && sqlServerDepartments != null)
            {
                DepartmentDto departmentDto = DepartmentMapper.toDepartmentDto(mySqlDepartments, sqlServerDepartments);
                return Task.FromResult<DepartmentDto?>(departmentDto);
            }
            return Task.FromResult<DepartmentDto?>(null);
        }

        public Task<DepartmentDto> AddAsync(CreateDepartmentDto departmentDto)
        {
            var mySqlDepartment = departmentDto.toCreateMySqlDepartment();
            _mySqlDbContext.Departments.Add(mySqlDepartment);
            _mySqlDbContext.SaveChanges();
            var sqlServerDepartment = departmentDto.toCreateSqlServerDepartment();
            _sqlServerDbContext.Departments.Add(sqlServerDepartment);
            _sqlServerDbContext.SaveChanges();
            var department = DepartmentMapper.toDepartmentDto(mySqlDepartment, sqlServerDepartment);
            return Task.FromResult(department);
        }

        public Task<DepartmentDto?> UpdateDepartmentAsync(UpdateDepartmentDto departmentDto, int id)
        {
            var mySqlDepartment = _mySqlDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            var sqlServerDepartment = _sqlServerDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            if (mySqlDepartment != null && sqlServerDepartment != null)
            {
                mySqlDepartment.DepartmentName = departmentDto.DepartmentName;
                sqlServerDepartment.DepartmentName = departmentDto.DepartmentName;
                sqlServerDepartment.UpdatedAt = DateTime.Now;
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();
                var updatedDepartment = DepartmentMapper.toDepartmentDto(mySqlDepartment, sqlServerDepartment);
                return Task.FromResult<DepartmentDto?>(updatedDepartment);
            }
            return Task.FromResult<DepartmentDto?>(null);
        }

        public Task<bool> DeleteDepartmentAsync(int id)
        {
            var mySqlDepartment = _mySqlDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            var sqlServerDepartment = _sqlServerDbContext.Departments.FirstOrDefault(d => d.DepartmentID == id);
            if (mySqlDepartment != null && sqlServerDepartment != null)
            {
                _mySqlDbContext.Departments.Remove(mySqlDepartment);
                _sqlServerDbContext.Departments.Remove(sqlServerDepartment);
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}