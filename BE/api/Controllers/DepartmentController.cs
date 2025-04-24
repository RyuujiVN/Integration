using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Department;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAllDepartments()
        {
            var departments = await _departmentRepo.GetAllDepartmentsAsync();
            if (departments == null || departments.Count == 0)
            {
                return NotFound("Phòng ban trống");
            }
            return Ok(departments);

        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentRepo.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound("Không tìm thấy phòng ban có id: " + id);
            }
            return Ok(department);
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult> AddDepartment([FromBody] CreateDepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
            }

            var checkExist = await _departmentRepo.GetDepartmentByIdAsync(departmentDto.DepartmentID);
            if (checkExist != null)
            {
                return BadRequest("Phòng ban đã tồn tại, vui lòng kiểm tra lại");
            }

            var departmentUpdate = await _departmentRepo.AddAsync(departmentDto);
            if (departmentUpdate == null)
            {
                return BadRequest("Thêm phòng ban không thành công");
            }

            return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentUpdate.DepartmentID }, departmentUpdate);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto departmentDto, int id)
        {

            if (departmentDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
            }
            var departmentUpdate = await _departmentRepo.UpdateDepartmentAsync(departmentDto, id);

            if (departmentUpdate == null)
            {
                return NotFound("Không tìm thấy phòng ban để cập nhật");
            }
            return Ok(departmentUpdate);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentRepo.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound("Không tìm thấy phòng ban có id: " + id);
            }
            var result = await _departmentRepo.DeleteDepartmentAsync(id);
            return Ok("Xóa phòng ban thành công");
        }
    }
}