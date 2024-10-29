namespace Trainingtask.Models.DTO
{
    public class CompanyDTO
    {
        public string ComId { get; set; } = string.Empty;  // Company ID
        public string ComName { get; set; } = string.Empty;  // Company Name
        public decimal Basic { get; set; }  // Basic allowance
        public decimal Hrent { get; set; }  // House rent allowance
        public decimal Medical { get; set; }  // Medical allowance
        public bool IsInactive { get; set; }  // Inactive status
    }
}
