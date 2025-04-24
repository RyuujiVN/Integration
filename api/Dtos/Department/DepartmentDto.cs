namespace api.Dtos
{
    public class DepartmentDto
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}