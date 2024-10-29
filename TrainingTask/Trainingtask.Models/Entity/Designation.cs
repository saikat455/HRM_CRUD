namespace Trainingtask.Models.Entity
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public List<Employee>? Employees { get; set; }
    }
}
