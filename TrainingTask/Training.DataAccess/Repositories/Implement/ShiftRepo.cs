using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.Implement
{
    public class ShiftRepo : Repository<Shift>, IShiftRepo
    {
        public ShiftRepo(TrainingDbContext db) : base(db)
        {
        }

        // Get Shift by ID
        public async Task<Shift?> GetById(Guid id)
        {
            return await db.Shifts.FindAsync(id);
        }

        // Get all shifts with included company information
        public async Task<List<ShiftDTO>> GetIncludeShift()
        {
            return await db.Shifts.Include(c => c.Company) // Include the related Company
                .Select(x => new ShiftDTO
                {
                    ShiftId = x.ShiftId,
                    ShiftName = x.ShiftName,
                    In = x.In,
                    Out = x.Out,
                    Late = x.Late,
                    ComId = x.ComId // Get the foreign key
                })
                .ToListAsync();
        }

       
    }
}
