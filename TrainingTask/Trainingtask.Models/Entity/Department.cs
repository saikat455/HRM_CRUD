using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingtask.Models.Entity
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Employee>? Employees { get; set; }
    }
}
