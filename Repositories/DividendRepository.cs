using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Dtos.Dividend;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Models;


namespace api.Repositories
{
    public class DividendRepository : IDividendRepository
    {
        private readonly SqlServerDBContext _context; 
        public DividendRepository(SqlServerDBContext context)
        {
            _context = context;
        }

        public async Task<List<DividendDto>> GetAllDividendsAsync()
        {
            return await _context.Dividends
                .Select(d => new DividendDto
                {
                    
                    EmployeeID = d.EmployeeID,
                    DividendID = d.DividendID,
                    DividendAmount = d.DividendAmount,
                    DividendDate = d.DividendDate,
                    CreatedAt = d.CreatedAt
                })
                .ToListAsync(); 
        }
        public async Task<DividendDto?> GetDividendByIdAsync(int idDividend)
        {
            var dividend = await _context.Dividends
                .Where(d => d.DividendID == idDividend)
                .Select(d => new DividendDto
                {
                    DividendID = d.DividendID,
                    EmployeeID = d.EmployeeID,
                    DividendAmount = d.DividendAmount,
                    DividendDate = d.DividendDate,
                    CreatedAt = d.CreatedAt
                })
                .FirstOrDefaultAsync();
            if (dividend == null)
            {
                return null;
            }
            return dividend;
        }
        public async Task<DividendDto?> AddDividendAsync(CreateDividendDto dividendDto)
        {
            var existingDividend = await _context.Dividends
                .FirstOrDefaultAsync(d => d.EmployeeID == dividendDto.EmployeeID&&d.DividendID== dividendDto.DividendID);
            if (existingDividend != null)
            {
                return null;
            }
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == dividendDto.EmployeeID);
            if (existingEmployee == null)
            {
                return null;
            }
            var dividendModel = dividendDto.toCreateDividend();
            await _context.Dividends.AddAsync(dividendModel);
            await _context.SaveChangesAsync();
            var returnDto = DividendMapper.toDividendDto(dividendModel);
            return returnDto;
        }
        public async Task<DividendDto?> UpdateDividendAsync(UpdateDividendDto dividendDto, int idDividend)
        {
            var dividend = await _context.Dividends.FindAsync(idDividend);
            if (dividend == null)
            {
                return null;
            }
            if(dividendDto.EmployeeID != dividend.EmployeeID)
            {
                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeID == dividendDto.EmployeeID);
                if (existingEmployee == null)
                {
                    return null;
                }
            }
            dividend.EmployeeID = dividendDto.EmployeeID;
            dividend.DividendAmount = dividendDto.DividendAmount;
            dividend.DividendDate = dividendDto.DividendDate;
            await _context.SaveChangesAsync();
            return DividendMapper.toDividendDto(dividend);
        }
        public async Task<bool> DeleteDividendAsync(int idDividend)
        {
            var dividend = await _context.Dividends.FindAsync(idDividend);
            if (dividend == null)
            {
                return false;
            }
            _context.Dividends.Remove(dividend);
            await _context.SaveChangesAsync();
            return true;
        }    
        public async Task<double> GetTotalDividendsByEmployeeId(int employeeId)
        {
            var totalDividends = await _context.Dividends
                .Where(d => d.EmployeeID == employeeId)
                .SumAsync(d => d.DividendAmount);
            return (double) totalDividends;
        }
    }
}