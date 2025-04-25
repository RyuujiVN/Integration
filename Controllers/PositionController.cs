using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Position;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _positionRepo;
        public PositionController(IPositionRepository positionRepo)
        {
            _positionRepo = positionRepo;
        }
        [HttpGet("getall")]
        public async Task<ActionResult> GetAllPositions()
        {
            var result = await _positionRepo.GetAllPositionsAsync();
            if (result == null)
            {
                return NotFound(new { message = "Không có vị trí nào tồn tại" });
            }
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult> GetPositionById(int id)
        {
            var position = await _positionRepo.GetPositionByIdAsync(id);
            if (position == null)
            {
                return NotFound(new { message = "Không tìm thấy vị trí có ID: " + id });
            }
            return Ok(position);
        }

        [HttpPost("AddPosition")]
        public async Task<ActionResult> AddPosition([FromBody] PositionDto positionDto) 
        {
            if (positionDto == null){
                return null;
            }
            await _positionRepo.AddAsync(positionDto);
            return CreatedAtAction(nameof(GetAllPositions), new { id = positionDto.PositionID }, positionDto);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdatePosition(int id, [FromBody] UpdatePositionDto positionDto)
        {
            if (positionDto == null)
            {
                return BadRequest("Dữ liệu cập nhật không được để trống");
            }
            var position = await _positionRepo.UpdatePositionAsync(id, positionDto);
            if (position == null)
            {
                return NotFound(new { message = "Không tìm thấy vị trí có ID: " + id });
            }
            return Ok(Mappers.PositionMapper.toPositionDtoFromUpdate(position, id));
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeletePosition(int id)
        {
            var position = await _positionRepo.GetPositionByIdAsync(id);
            if (position == null)
            {
                return NotFound(new { message = "Không tìm thấy vị trí có ID: " + id });
            }
            await _positionRepo.DeletePositionAsync(id);
            return Ok(new { message = "Xóa vị trí thành công" });
        }
    }
}