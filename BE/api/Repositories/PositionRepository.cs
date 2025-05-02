using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Dtos.Position;
using api.Interfaces;
using api.Mappers;
using api.Models.MySql;
namespace api.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly MySqlDBContext _mySqlDbContext;
        private readonly SqlServerDBContext _sqlServerDbContext;
        public PositionRepository(MySqlDBContext mySqlDbContext, SqlServerDBContext sqlServerDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
            _sqlServerDbContext = sqlServerDbContext;
        }
        public Task<List<PositionDto?>> GetAllPositionsAsync()
        {
            var mySqlPositions = _mySqlDbContext.Positions.ToList();
            var sqlServerPositions = _sqlServerDbContext.Positions.ToList();
            var listPositionDto = new List<PositionDto?>();
            foreach (var mySqlPosition in mySqlPositions)
            {
                foreach (var sqlServerPosition in sqlServerPositions)
                {
                    if (mySqlPosition.PositionID == sqlServerPosition.PositionID)
                    {
                        var positionDto = PositionMapper.toPositionDto(mySqlPosition, sqlServerPosition);
                        listPositionDto.Add(positionDto);
                        break;
                    }
                }
            }
            return Task.FromResult(listPositionDto);
        }

        public Task<PositionDto?> GetPositionByIdAsync(int id)
        {
            var mySqlPosition = _mySqlDbContext.Positions.FirstOrDefault(d => d.PositionID == id);
            var sqlServerPosition = _sqlServerDbContext.Positions.FirstOrDefault(d => d.PositionID == id);
            bool checkMySql = mySqlPosition != null;
            bool checkSqlServer = sqlServerPosition != null;
            if (checkMySql && checkSqlServer)
            {
                PositionDto positionDto = PositionMapper.toPositionDto(mySqlPosition!, sqlServerPosition!);
                return Task.FromResult<PositionDto?>(positionDto);
            }
            return Task.FromResult<PositionDto?>(null);
        }
        public Task<PositionDto> AddAsync(CreatePositionDto positionDto)
        {
            var positionMySql = positionDto.toMySqlPosition();
            _mySqlDbContext.Positions.Add(positionMySql);
            _mySqlDbContext.SaveChanges();
            var positionSqlServer = positionDto.toSqlServerPosition();
            _sqlServerDbContext.Positions.Add(positionSqlServer);
            _sqlServerDbContext.SaveChanges();
            return Task.FromResult(PositionMapper.toPositionDto(positionMySql, positionSqlServer));
        }

        public Task<PositionDto?> UpdatePositionAsync(int id, UpdatePositionDto positionDto)
        {
            var mySqlPosition = _mySqlDbContext.Positions
            .FirstOrDefault(d => d.PositionID == id);
            var sqlServerPosition = _sqlServerDbContext.Positions
            .FirstOrDefault(d => d.PositionID == id);
            bool checkMySql = mySqlPosition != null;
            bool checkSqlServer = sqlServerPosition != null;
            if (checkMySql && checkSqlServer)
            {
                mySqlPosition!.PositionName = positionDto.PositionName;
                sqlServerPosition!.PositionName = positionDto.PositionName;
                sqlServerPosition.UpdatedAt = DateTime.Now;
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();
                return Task.FromResult<PositionDto?>(Mappers.PositionMapper.toPositionDto(mySqlPosition, sqlServerPosition));
            }
            return Task.FromResult<PositionDto?>(null);
        }

        public Task<bool> DeletePositionAsync(int id)
        {
            var mySqlPosition = _mySqlDbContext.Positions
            .FirstOrDefault(d => d.PositionID == id);
            var sqlServerPosition = _sqlServerDbContext.Positions
            .FirstOrDefault(d => d.PositionID == id);
            bool checkMySql = mySqlPosition != null;
            bool checkSqlServer = sqlServerPosition != null;
            if (checkMySql && checkSqlServer)
            {
                _mySqlDbContext.Positions.Remove(mySqlPosition!);
                _sqlServerDbContext.Positions.Remove(sqlServerPosition!);
                _mySqlDbContext.SaveChanges();
                _sqlServerDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}