//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Trainingtask.Models.Entity;

//namespace Training.DataAccess
//{
//    public class TrainingDbContext : DbContext
//    {
//        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
//        {
//        }
//        public DbSet<Shift> Shifts { get; set; }
//        public DbSet<Department> Departments { get; set; }
//        public DbSet<Employee> Employees { get; set; }
//        public DbSet<Designation> Designations { get; set; }
//        public DbSet<Company> Companies { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Configuring Employee entity relationships
//            modelBuilder.Entity<Employee>()
//                .HasOne(e => e.Desig)
//                .WithMany(d => d.Employees)
//                .HasForeignKey(e => e.DesigId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Configuring Company entity
//            modelBuilder.Entity<Company>()
//                .HasKey(c => c.ComId);


//        }


//    }
//}


using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainingtask.Models.Entity;

namespace Training.DataAccess
{
    public class TrainingDbContext : DbContext
    {
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring Employee entity relationships
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Desig)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesigId)
                .OnDelete(DeleteBehavior.Cascade);

            

            // Configuring Company entity
            modelBuilder.Entity<Company>()
                .HasKey(c => c.ComId);
        }
    }
}

