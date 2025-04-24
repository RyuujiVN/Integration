using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Position;
using api.Models.MySql;

namespace api.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<PositionDto?>> GetAllPositionsAsync();
        Task<PositionDto> AddAsync(CreatePositionDto positionDto);
        Task<PositionDto?> GetPositionByIdAsync (int id);
        Task<PositionDto?> UpdatePositionAsync(int id, UpdatePositionDto positionDto);
        Task<bool> DeletePositionAsync(int id);
    }
}