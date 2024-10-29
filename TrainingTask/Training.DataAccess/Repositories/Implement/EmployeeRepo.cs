
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.implement
{
    public class EmployeeRepo : Repository<Employee>,IEmployeeRepo

    {
        public EmployeeRepo(TrainingDbContext db) : base(db)
        {
        }
        public async Task<Employee?> GetById(string id)
        {
            return await db.Employees.FindAsync(id);
        }

        public async Task<List<EmployeeDTO>> GetIncludedept()
        {
            return await db.Employees.Include(d => d.Dept)
                .Select(x=>new EmployeeDTO
                {
                    Id=x.Id,
                    Address = x.Address,
                    DeptId = x.DeptId,
                    Name= x.Name,
                    Phone = x.Phone,
                    City = x.City
                })
                .ToListAsync();
        }
    }
}