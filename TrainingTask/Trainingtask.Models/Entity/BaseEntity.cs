namespace Trainingtask.Models.Entity
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; }
        public string upby { get; set; } = string.Empty;
        public DateTime Created {  get; set; } 
    }
}
