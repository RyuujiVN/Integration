using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Models.MySql;

namespace api.Mappers
{
    public static class AttendanceMapper
    {
        public static AttendanceDto toAttendanceDto(this Attendance attendance)
        {
            return new AttendanceDto
            {
                AttendanceID = attendance.AttendanceID,
                EmployeeID = attendance.EmployeeID,
                WorkDays = attendance.WorkDays,
                AbsentDays = attendance.AbsentDays,
                LeaveDays = attendance.LeaveDays,
                AttendanceMonth = attendance.AttendanceMonth,
                CreatedAt = attendance.CreatedAt
            };
        }
        public static Attendance toAttendanceCreate(this CreateAttendanceDto createAttendanceDto)
        {
            return new Attendance
            {   
                EmployeeID = createAttendanceDto.EmployeeID,
                AttendanceID = createAttendanceDto.AttendanceID,
                WorkDays = createAttendanceDto.WorkDays,
                AbsentDays = createAttendanceDto.AbsentDays,
                LeaveDays = createAttendanceDto.LeaveDays,
                AttendanceMonth = createAttendanceDto.AttendanceMonth,
                CreatedAt = createAttendanceDto.CreatedAt
            };
        }
        public static Attendance toAttendanceUpdate(this UpdateAttendanceDto updateAttendanceDto)
        {
            return new Attendance
            {
                WorkDays = updateAttendanceDto.WorkDays,
                AbsentDays = updateAttendanceDto.AbsentDays,
                LeaveDays = updateAttendanceDto.LeaveDays,
                AttendanceMonth = updateAttendanceDto.AttendanceMonth,
            
            };
            
        }
    }
}