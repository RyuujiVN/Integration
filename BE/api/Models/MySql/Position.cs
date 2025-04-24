using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.MySql
{
    [Table("positions")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string PositionName { get; set; } = string.Empty;
    }
}