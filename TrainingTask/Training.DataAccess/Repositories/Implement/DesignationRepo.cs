
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.implement
{
    public class DesignationRepo : Repository<Designation>, IDesignationRepo

    {
        public DesignationRepo(TrainingDbContext db) : base(db)
        {
        }


    }
}