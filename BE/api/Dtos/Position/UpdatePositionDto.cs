using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Position
{
    public class UpdatePositionDto
    {
        public string PositionName { get; set; } = string.Empty;
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}