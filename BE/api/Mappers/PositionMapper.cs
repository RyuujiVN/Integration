using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Position;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Mappers
{
    public static class PositionMapper
    {
        public static Models.MySql.Position toMySqlPosition(this CreatePositionDto positionDto)
        {
            return new Models.MySql.Position
            {
                PositionID = positionDto.PositionID,
                PositionName = positionDto.PositionName,
            };
        }

        public static Models.SqlServer.Position toSqlServerPosition(this CreatePositionDto positionDto)
        {
            return new Models.SqlServer.Position
            {
                PositionID = positionDto.PositionID,
                PositionName = positionDto.PositionName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        public static PositionDto toPositionDto(Models.MySql.Position mySqlposition, Models.SqlServer.Position sqlServerPosition)
        {
            return new PositionDto
            {
                PositionID = mySqlposition.PositionID,
                PositionName = mySqlposition.PositionName,
                CreatedAt = sqlServerPosition.CreatedAt,
                UpdatedAt = sqlServerPosition.UpdatedAt
            };
        }

        public static UpdatePositionDto toUpdatePositionDto(Models.MySql.Position mySqlposition, Models.SqlServer.Position sqlServerPosition)
        {
            return new UpdatePositionDto
            {
                PositionName = mySqlposition.PositionName,
            };
        }
    }
}