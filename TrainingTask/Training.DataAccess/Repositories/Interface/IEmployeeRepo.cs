using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.Interface
{
    
  
        public interface IEmployeeRepo : IRepository<Employee>
        {
        Task<List<EmployeeDTO>> GetIncludedept();

        }
    
}
