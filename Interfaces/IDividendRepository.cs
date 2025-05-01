using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Dividend;

namespace api.Interfaces
{
    public interface IDividendRepository
    {
        Task<List<DividendDto>> GetAllDividendsAsync();
        Task<DividendDto?> GetDividendByIdAsync(int idAttendance);
        Task<DividendDto?> AddDividendAsync(CreateDividendDto dividendDto);
        Task<DividendDto?> UpdateDividendAsync(UpdateDividendDto dividendDto, int idAttendance);
        Task<bool> DeleteDividendAsync(int idAttendance);
        Task<double> GetTotalDividendsByEmployeeId(int employeeId);
    }
}