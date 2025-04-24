using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepo.GetAllEmployeesAsync();
            if (employees == null || employees.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên nào" });
            }
            return Ok(employees);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên có ID: " + id });
            }
            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult> AddEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
            }
            var checkExist = await _employeeRepo.GetEmployeeByIdAsync(employeeDto.EmployeeID);
            if (checkExist != null)
            {
                return BadRequest("Nhân viên đã tồn tại, vui lòng kiểm tra lại");
            }
            var created = await _employeeRepo.AddAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = created.EmployeeID }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto employeeDto, int id)
        {
            if (employeeDto == null)
            {
                return BadRequest("Dữ liệu cập nhật không được để trống");
            }

            var updated = await _employeeRepo.UpdateAsync(employeeDto, id);
            if (updated == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên có ID: " + id });
            }

            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên có ID: " + id });
            }

            await _employeeRepo.DeleteAsync(id);
            return Ok(new { message = "Xóa nhân viên thành công" });
        }
    }
}