using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;

namespace Training.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        IdepartmentRepo DepartmentRepo { get; }
        IDesignationRepo DesignationRepo { get; }
        IEmployeeRepo EmployeeRepo { get; }
        ICompanyRepo CompanyRepo { get; }
       
    }
}
