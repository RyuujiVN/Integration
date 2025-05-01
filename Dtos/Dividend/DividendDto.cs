using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Dividend
{
    public class DividendDto
    {
        public int DividendID { get; set; }
        public int? EmployeeID { get; set; }       
        public decimal DividendAmount { get; set; }
        public DateTime DividendDate { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}