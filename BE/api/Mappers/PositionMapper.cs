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
        public static Models.MySql.Position toMySqlPosition(this PositionDto positionDto)
        {
            return new Models.MySql.Position
            {
                PositionID = positionDto.PositionID,
                PositionName = positionDto.PositionName,
            };
        }

        public static Models.SqlServer.Position toSqlServerPosition(this PositionDto positionDto)
        {
            return new Models.SqlServer.Position
            {
                PositionID = positionDto.PositionID,
                PositionName = positionDto.PositionName,
                CreatedAt = positionDto.CreatedAt,
                UpdatedAt = positionDto.UpdatedAt
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
        public static Models.MySql.Position toUpdateMySqlPosition(this UpdatePositionDto positionDto, int id)
        {
            return new Models.MySql.Position
            {
                PositionID = id,
                PositionName = positionDto.PositionName,
            };
        }

        public static Models.SqlServer.Position toUpdateSqlServerPosition(this UpdatePositionDto positionDto, int id)
        {
            return new Models.SqlServer.Position
            {
                PositionID = id,
                PositionName = positionDto.PositionName,
                CreatedAt = positionDto.CreatedAt,
                UpdatedAt = positionDto.UpdatedAt
            };
        }
        public static UpdatePositionDto toUpdatePositionDto(Models.MySql.Position mySqlposition, Models.SqlServer.Position sqlServerPosition)
        {
            return new UpdatePositionDto
            {
                PositionName = mySqlposition.PositionName,
                CreatedAt = sqlServerPosition.CreatedAt,
                UpdatedAt = sqlServerPosition.UpdatedAt
            };
        }
        public static PositionDto toPositionDtoFromUpdate(UpdatePositionDto positionDto, int id)
        {
            return new PositionDto
            {
                PositionID = id,
                PositionName = positionDto.PositionName,
                CreatedAt = positionDto.CreatedAt,
                UpdatedAt = positionDto.UpdatedAt
            };
        }
    }
}