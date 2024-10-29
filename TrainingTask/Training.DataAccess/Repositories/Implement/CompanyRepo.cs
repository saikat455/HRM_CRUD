using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace Training.DataAccess.Repositories.Implement
{
    public class CompanyRepo : Repository<Company>,ICompanyRepo
    {
        public CompanyRepo(TrainingDbContext db) : base(db)
        {
        }

        public async Task<Company?> GetById(string id)
        {
            return await db.Companies.FindAsync(id);
        }

        public async Task<List<CompanyDTO>> GetAllCompanies()
        {
            return await db.Companies
                .Select(x => new CompanyDTO
                {
                    ComId = x.ComId,
                    ComName = x.ComName,
                    Basic = x.Basic,
                    Hrent = x.Hrent,
                    Medical = x.Medical,
                    IsInactive = x.IsInactive
                })
                .ToListAsync();
        }

        public Task<List<CompanyDTO>> GetIncludedept()
        {
            throw new NotImplementedException();
        }
    }
}
