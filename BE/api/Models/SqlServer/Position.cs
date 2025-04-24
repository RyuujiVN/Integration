using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.SqlServer
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string PositionName { get; set; } = string.Empty;
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}