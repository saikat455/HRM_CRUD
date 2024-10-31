using System.ComponentModel.DataAnnotations;

namespace Trainingtask.Models.Entity
{
    public class Company
    {
        [Key]
        public string ComId { get; set; } = Guid.NewGuid().ToString();
        public string ComName { get; set; } = string.Empty;
        public decimal Basic { get; set; }
        public decimal Hrent { get; set; }
        public decimal Medical { get; set; }
        public bool IsInactive { get; set; }

        public List<Shift>? Shifts { get; set; }

        public decimal CalculateTotalSalary(decimal baseSalary)
        {
            decimal basicSalary = baseSalary * Basic;
            decimal houseRent = baseSalary * Hrent;
            decimal medicalAllowance = baseSalary * Medical;
            return basicSalary + houseRent + medicalAllowance;
        }
    }
}
