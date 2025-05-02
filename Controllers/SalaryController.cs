using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Position;
using api.Interfaces;
using api.Dtos.Salary;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public SalaryController(ISalaryRepository salaryRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
        }
        [HttpGet]
        [Route("GetAllSalaries")]
        public async Task<IActionResult> GetAllSalaries()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var salaries = await _salaryRepository.GetAllSalariesAsync();
            if (salaries == null || !salaries.Any())
            {
                return NotFound("Không tìm thấy mã lương nào.");
            }
            return Ok(salaries);
        }
        [HttpGet]
        [Route("GetSalaryById/{idSalary}")]
        public async Task<IActionResult> GetSalaryById(int idSalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var salary = await _salaryRepository.GetSalaryByIdAsync(idSalary);
            if (salary == null)
            {
                return NotFound("Không tìm thấy mã lương.");
            }
            return Ok(salary);
        }
        [HttpPost]
        [Route("AddSalary")]
        public async Task<IActionResult> AddSalary([FromBody] CreateSalaryDto salaryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkExist = await _salaryRepository.GetSalaryByIdAsync(salaryDto.SalaryID);
            if (checkExist != null)
            {
                return BadRequest("Mã lương đã tồn tại, vui lòng kiểm tra lại");
            }
            int idEmployee = salaryDto.EmployeeID??0;
            var checkEmployee = await _employeeRepository.GetEmployeeByIdAsync(idEmployee);
            if (checkEmployee == null)
            {
                return BadRequest("Mã nhân viên không tồn tại, vui lòng kiểm tra lại");
            }
            if(salaryDto.BaseSalary < 0)
            {
                return BadRequest("Lương cơ bản không hợp lệ, vui lòng kiểm tra lại");
            }
            if(salaryDto.Bonus< 0)
            {
                return BadRequest("Thưởng không hợp lệ, vui lòng kiểm tra lại");
            }
            if(salaryDto.Deductions  < 0)
            {
                return BadRequest("Phạt không hợp lệ, vui lòng kiểm tra lại");
            }
            if(salaryDto.BaseSalary< salaryDto.Bonus)
            {
                return BadRequest("Lương cơ bản không được nhỏ hơn thưởng, vui lòng kiểm tra lại");
            }
            if(salaryDto.BaseSalary==salaryDto.Deductions)
            {
                return BadRequest("Chúc mừng bạn đã có một tháng làm việc không hiệu quả, xin thành thật chia buồn với bạn");
            }
            
            if(salaryDto.BaseSalary == 0 && salaryDto.Bonus == 0 && salaryDto.Deductions == 0)
            {
                return BadRequest("Lương cơ bản, thưởng và phụ cấp không được để trống, vui lòng kiểm tra lại");
            }
            var salary = await _salaryRepository.AddSalaryAsync(salaryDto);
            if (salary == null)
            {
                return BadRequest("Mã nhân viên đã tồn tại , vui lòng kiểm tra lại.");
            }
            return CreatedAtAction(nameof(GetSalaryById), new { idSalary = salary.SalaryID }, salary);
        }
        [HttpPut]
        [Route("UpdateSalary/{idSalary}")]
        public async Task<IActionResult> UpdateSalary([FromBody] UpdateSalaryDto salaryDto, int idSalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkExist = await _salaryRepository.GetSalaryByIdAsync(idSalary);
            if (checkExist == null)
            {
                return NotFound("Không tìm thấy mã lương.");
            }
            var updatedSalary = await _salaryRepository.UpdateSalaryAsync(salaryDto, idSalary);
            if (updatedSalary == null)
            {
                return NotFound("Không tìm thấy mã lương.");
            }
            return Ok(updatedSalary);
        }
        [HttpDelete]
        [Route("DeleteSalary/{idSalary}")]
        public async Task<IActionResult> DeleteSalary(int idSalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkExist = await _salaryRepository.GetSalaryByIdAsync(idSalary);
            if (checkExist == null)
            {
                return NotFound("Không tìm thấy mã lương.");
            }
            var deletedSalary = await _salaryRepository.DeleteSalaryAsync(idSalary);
            if (deletedSalary == false)
            {
                return NotFound("Không tìm thấy mã lương.");
            }
            return Ok("Xóa mã lương thành công.");
        }
    }
    
}