using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.implement;
using Training.DataAccess.Repositories.Implement;
using Training.DataAccess.Repositories.Interface;

namespace Training.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICompanyRepo CompanyRepo { get; private set; }
        public IdepartmentRepo DepartmentRepo { get; private set; }
        public IDesignationRepo DesignationRepo { get; private set; }
        public IEmployeeRepo EmployeeRepo { get; private set; }
        public IShiftRepo ShiftRepo { get; private set; }
       

        public UnitOfWork(TrainingDbContext db)
        {
            DepartmentRepo = new DepartmentRepo(db);
            DesignationRepo = new DesignationRepo(db);
            EmployeeRepo = new EmployeeRepo(db);
            CompanyRepo = new CompanyRepo(db);
            ShiftRepo = new ShiftRepo(db);
        }

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
