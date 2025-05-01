using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Dividend;

namespace api.Controllers
{
    [Route("api/Dividend")]
    [ApiController]
    public class DividendController : ControllerBase
    {
        private readonly IDividendRepository _dividendRepository;
        public DividendController(IDividendRepository dividendRepository)
        {
            _dividendRepository = dividendRepository;
        }

        [HttpGet]
        [Route("GetAllDividends")]
        public async Task<IActionResult> GetAllDividends()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dividends = await _dividendRepository.GetAllDividendsAsync();
            if (dividends == null || !dividends.Any())
            {
                return NotFound("Không tìm thấy cổ tức nào.");
            }
            return Ok(dividends);
        }

        [HttpGet]
        [Route("GetDividendById/{idDividend}")]
        public async Task<IActionResult> GetDividendById(int idDividend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dividend = await _dividendRepository.GetDividendByIdAsync(idDividend);
            if (dividend == null)
            {
                return NotFound("Không tìm thấy cổ tức.");
            }
            return Ok(dividend);
        }

        [HttpPost]
        [Route("AddDividend")]
        public async Task<IActionResult> AddDividend([FromBody] CreateDividendDto dividendDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdDividend = await _dividendRepository.AddDividendAsync(dividendDto);
            if (createdDividend == null)
            {
                return BadRequest("Nhân viên không tồn tại hoặc Cổ tức đã tồn tại.");
            }
            return CreatedAtAction(nameof(GetDividendById), new { idDividend = createdDividend.DividendID }, createdDividend);
            
        }

        [HttpPut]
        [Route("UpdateDividend/{idDividend}")]
        public async Task<IActionResult> UpdateDividend([FromBody] UpdateDividendDto dividendDto, int idDividend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedDividend = await _dividendRepository.UpdateDividendAsync(dividendDto, idDividend);
            if (updatedDividend == null)
            {
                return NotFound("Không tìm thấy cổ tức hoặc nhân viên không tồn tại.");
            }
            return Ok(updatedDividend);
        }

        [HttpDelete]
        [Route("DeleteDividend/{idDividend}")]
        public async Task<IActionResult> DeleteDividend(int idDividend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isDeleted = await _dividendRepository.DeleteDividendAsync(idDividend);
            if (!isDeleted)
            {
                return NotFound("Không tìm thấy cổ tức.");
            }
            return Ok("Xóa cổ tức thành công.");
        }
        [HttpGet]
        [Route("GetAmountDividendofEmployee/{idEmployee}")]
        public async Task<IActionResult> GetAmountDividendOfEmployee(int idEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dividends = await _dividendRepository.GetAllDividendsAsync();
            if (dividends == null || !dividends.Any())
            {
                return NotFound("Không tìm thấy cổ tức nào.");
            }
            double totalDividend = await _dividendRepository.GetTotalDividendsByEmployeeId(idEmployee);
            if (totalDividend == 0)
            {
                return NotFound("Không tìm thấy cổ tức nào cho nhân viên này.");
            }
            return Ok(totalDividend);

        }

    }
}
