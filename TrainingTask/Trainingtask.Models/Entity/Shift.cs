using System;

namespace Trainingtask.Models.Entity
{
    public class Shift : BaseEntity
    {
        public Guid ShiftId { get; set; } = Guid.NewGuid();

        public string ShiftName { get; set; } = string.Empty;  // Shift Name

        public TimeSpan In { get; set; }  // Shift In Time

        public TimeSpan Out { get; set; }  // Shift Out Time

        public TimeSpan Late { get; set; }  // Late Time

        public string ComId { get; set; } = string.Empty;  // Foreign key for company
        public Company Company { get; set; } // Navigation property for company
    }
}
