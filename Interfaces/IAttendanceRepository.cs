using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;

namespace api.Interfaces
{
    public interface IAttendanceRepository 
    {
        Task<List<AttendanceDto>> GetAllAttendancesAsync();
        Task<AttendanceDto?> GetAttendanceByIdAsync(int idAttendance);
        Task<AttendanceDto> addAttendanceAsync(CreateAttendanceDto attendanceDto, int idEmployee);
        Task<AttendanceDto?> UpdateAttendanceAsync(UpdateAttendanceDto attendanceDto, int idAttendance);
        Task<bool> DeleteAttendanceAsync(int idAttendance);
    }
}