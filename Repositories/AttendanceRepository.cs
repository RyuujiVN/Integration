using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Dtos.Attendance;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Models;

namespace api.Repositories
{
    public class AttendanceRepository : IAttendanceRepository    
    {
        private readonly MySqlDBContext _mySqlDBContext;
        public AttendanceRepository(MySqlDBContext mySqlDBContext)
        {
            _mySqlDBContext = mySqlDBContext;
        }
        public async Task<List<AttendanceDto>> GetAllAttendancesAsync()
        {
            return await _mySqlDBContext.Attendances
                .Select(a => new AttendanceDto
                {
                    
                    WorkDays = a.WorkDays,
                    AbsentDays = a.AbsentDays,
                    LeaveDays = a.LeaveDays,
                    AttendanceMonth = a.AttendanceMonth,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
        }
        public async Task<AttendanceDto?> GetAttendanceByIdAsync(int idAttendance)
        {
            var attendance = await _mySqlDBContext.Attendances
                .Where(a => a.AttendanceID == idAttendance)
                .Select(a => new AttendanceDto
                {
                    WorkDays = a.WorkDays,
                    AbsentDays = a.AbsentDays,
                    LeaveDays = a.LeaveDays,
                    AttendanceMonth = a.AttendanceMonth,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();
            if (attendance == null)
            {
                return null;
            }
            return attendance;
        }
        public async Task<AttendanceDto> addAttendanceAsync(CreateAttendanceDto attendanceDto,int idAttendance)
        {
            var attendanceModel = attendanceDto.toAttendanceCreate();
            await _mySqlDBContext.Attendances.AddAsync(attendanceModel);
            await _mySqlDBContext.SaveChangesAsync();
            var returnDto = AttendanceMapper.toAttendenceDto(attendanceModel);
            return returnDto;
        }
        public async Task<AttendanceDto?> UpdateAttendanceAsync(UpdateAttendanceDto attendanceDto, int idAttendance)
        {
            var attendanceModel = await _mySqlDBContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceID == idAttendance);
            if (attendanceModel == null)
            {
                return null;
            }
            attendanceModel.WorkDays = attendanceDto.WorkDays;
            attendanceModel.AbsentDays = attendanceDto.AbsentDays;
            attendanceModel.LeaveDays = attendanceDto.LeaveDays;
            attendanceModel.AttendanceMonth = attendanceDto.AttendanceMonth;
            await _mySqlDBContext.SaveChangesAsync();
            return AttendanceMapper.toAttendenceDto(attendanceModel);
        }
        public async Task<bool> DeleteAttendanceAsync(int idAttendance)
        {
            var attendance = await _mySqlDBContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceID == idAttendance);
            if (attendance == null)
            {
                return false;
            }
            _mySqlDBContext.Attendances.Remove(attendance);
            await _mySqlDBContext.SaveChangesAsync();
            return true;
        }
    }
}