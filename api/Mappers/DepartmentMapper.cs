using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

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

        public static api.Models.SqlServer.Department toCreateSqlServerDepartment(this DepartmentUpdateAndCreateDto dto, int id)
        {
            return new api.Models.SqlServer.Department
            {
                DepartmentID = id,
                DepartmentName = dto.DepartmentName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        public static api.Models.MySql.Department toCreateMySqlDepartment(this DepartmentUpdateAndCreateDto dto, int id)
        {
            return new api.Models.MySql.Department
            {
                DepartmentID = id,
                DepartmentName = dto.DepartmentName,
            };
        }

    }
}