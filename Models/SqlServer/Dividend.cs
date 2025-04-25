using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.SqlServer
{
    [Table("Dividends")]
    public class Dividend
    {
        [Key]
        public int DividendID { get; set; }
        
        public int? EmployeeID { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal DividendAmount { get; set; }
        
        [Required]
        public DateTime DividendDate { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
    }
}