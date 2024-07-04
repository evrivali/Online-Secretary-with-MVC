using Microsoft.EntityFrameworkCore;
using ergasiaMVC.Models;

namespace ergasiaMVC.Data
{
    public class MVC_Project_DbContext: DbContext
    {
        public MVC_Project_DbContext(DbContextOptions<MVC_Project_DbContext> options):base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<course_has_students>()
                  .HasKey(m => new { m.COURSE_idCOURSE, m.STUDENTS_RegistrationNumber });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<course_has_students> courses_have_students { get; set; }
    }
}
