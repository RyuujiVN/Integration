using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Dividend
{
    public class UpdateDividendDto
    {
        public int? EmployeeID { get; set; }       
        public decimal DividendAmount { get; set; }
        public DateTime DividendDate { get; set; }
    }
}