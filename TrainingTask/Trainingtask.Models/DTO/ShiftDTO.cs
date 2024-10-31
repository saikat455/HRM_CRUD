using System;

namespace Trainingtask.Models.DTO
{
    public class ShiftDTO
    {
        public Guid ShiftId { get; set; } = Guid.Empty;// Primary Key

        public string ComId { get; set; } = string.Empty; // Foreign Key for Company

        public string ShiftName { get; set; } = string.Empty; // Shift Name

        public TimeSpan In { get; set; } // Shift In Time

        public TimeSpan Out { get; set; } // Shift Out Time

        public TimeSpan Late { get; set; } // Late Time
    }
}
