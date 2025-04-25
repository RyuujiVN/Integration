using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Dtos.Department;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Attendance")]
    [ApiController] 
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepo;
        public AttendanceController(IAttendanceRepository attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAttendance()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var attendance = await _attendanceRepo.GetAllAttendancesAsync();
            if (attendance == null || attendance.Count == 0)
            {
                return NotFound("Không có dữ liệu chấm công");
            }
            return Ok(attendance);
        }
        [HttpGet]
        [Route("{idAttendance:int}/attendance")]
        public async Task<IActionResult> GetAttendanceById([FromRoute] int idAttendance)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var attendance = await _attendanceRepo.GetAttendanceByIdAsync(idAttendance);
            if (attendance == null)
            {
                return NotFound("Không có dữ liệu chấm công");
            }
            return Ok(attendance);
        }
        [HttpPost]
        [Route("addAttendance")]
        public async Task<IActionResult> addAttendance([FromBody] CreateAttendanceDto attendanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var attendanceCreate = await _attendanceRepo.addAttendanceAsync(attendanceDto);
            if (attendanceCreate == null)
            {
                return NotFound("không tìm thấy dữ liệu chấm công");
            }
            return Ok(attendanceCreate);
        }
        [HttpPut]
        [Route("{idAttendance:int}/updateAttendance")]
        public async Task<IActionResult> UpdateAttendance([FromRoute] int idAttendance, [FromBody] UpdateAttendanceDto attendanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var attendanceUpdate = await _attendanceRepo.UpdateAttendanceAsync(attendanceDto, idAttendance);
            if (attendanceUpdate == null)
            {
                return NotFound("Khong tìm thấy dữ liệu chấm công");
            }
            return Ok(attendanceUpdate);
        }
        [HttpDelete]
        [Route("{idAttendance:int}/deleteAttendance")]
        public async Task<IActionResult> DeleteAttendance([FromRoute] int idAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var attendanceDelete = await _attendanceRepo.DeleteAttendanceAsync(idAttendance);
            if (attendanceDelete == false)
            {
                return NotFound("Xóa dữ liệu chấm công không thành công");
            }
            return Ok("Xóa dữ liệu chấm công thành công");
        }
        
    }
    
}