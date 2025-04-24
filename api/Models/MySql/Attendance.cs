using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.MySql
{
    [Table("attendance")]
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public int? EmployeeID { get; set; }
        public int WorkDays { get; set; }
        public int AbsentDays { get; set; } = 0;
        public int LeaveDays { get; set; } = 0;
        public DateTime AttendanceMonth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
    }
}