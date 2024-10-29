
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.implement
{
    public class DepartmentRepo : Repository<Department>, IdepartmentRepo

    {
        public DepartmentRepo(TrainingDbContext db) : base(db)
        {
        }

       
    }
}