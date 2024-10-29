namespace Trainingtask.Models.Entity
{
    public class Employee : BaseEntity
    {

        public string Name { get; set; } = string.Empty;  // Employee's name
        public string Address { get; set; } = string.Empty;  // Employee's address
        public string City { get; set; } = string.Empty;  // Employee's city
        public string Phone { get; set; } = string.Empty;  // Employee's phone number

        public string DeptId { get; set; } = string.Empty;  // Foreign key for department
        public Department? Dept { get; set; }  // Navigation property for department

        public string DesigId { get; set; } = string.Empty;  // Foreign key for designation
        public Designation? Desig { get; set; }
    }
}
