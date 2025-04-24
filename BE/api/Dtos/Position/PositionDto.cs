using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Position
{
    public class PositionDto
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; } = string.Empty;
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}