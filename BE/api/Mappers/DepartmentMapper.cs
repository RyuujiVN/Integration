using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Department;

namespace api.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto toDepartmentDto(api.Models.MySql.Department mySqlDepartment, api.Models.SqlServer.Department sqlServerDepartment)
        {
            return new DepartmentDto
            {
                DepartmentID = mySqlDepartment.DepartmentID,
                DepartmentName = mySqlDepartment.DepartmentName,
                CreatedAt = sqlServerDepartment.CreatedAt,
                UpdatedAt = sqlServerDepartment.UpdatedAt
            };
        }

        public static api.Models.SqlServer.Department toCreateSqlServerDepartment(this CreateDepartmentDto dto)
        {
            return new api.Models.SqlServer.Department
            {
                DepartmentID = dto.DepartmentID,
                DepartmentName = dto.DepartmentName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        public static api.Models.MySql.Department toCreateMySqlDepartment(this CreateDepartmentDto dto)
        {
            return new api.Models.MySql.Department
            {
                DepartmentID = dto.DepartmentID,
                DepartmentName = dto.DepartmentName,
            };
        }

        public static api.Models.SqlServer.Department toUpdateSqlServerDepartment(this UpdateDepartmentDto dto, int id)
        {
            return new api.Models.SqlServer.Department
            {
                DepartmentID = id,
                DepartmentName = dto.DepartmentName,
                UpdatedAt = DateTime.Now
            };
        }
        public static api.Models.MySql.Department toUpdateMySqlDepartment(this UpdateDepartmentDto dto, int id)
        {
            return new api.Models.MySql.Department
            {
                DepartmentID = id,
                DepartmentName = dto.DepartmentName,
            };
        }
    }
}