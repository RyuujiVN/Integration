using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class CreateAttendanceDto
    {
        public int AttendanceID { get; set; }
        public int WorkDays { get; set; }
        public int AbsentDays { get; set; } = 0;
        public int LeaveDays { get; set; } = 0;
        public DateTime AttendanceMonth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}