namespace Trainingtask.Models.Entity
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

 
        public List<Employee>? Employees { get; set; }
    }
}
