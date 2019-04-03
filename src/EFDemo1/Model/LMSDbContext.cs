using System;
using Microsoft.EntityFrameworkCore;

namespace EFDemo1.Model
{
    public class LMSDbContext: DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public LMSDbContext(DbContextOptions<LMSDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Course
            modelBuilder.Entity<Course>().HasOne(c => c.Lecturer);

            //Student
            modelBuilder.Entity<Student>().HasMany(c => c.Enrolments);

            //Enrolment(many Student,many Course to many)
            modelBuilder.Entity<Enrolment>()
                        .HasKey(e => new { e.CourseId, e.StudentId });
            
            modelBuilder.Entity<Enrolment>()
                        .HasOne(e => e.Course)
                        .WithMany(c => c.Enrolments)
                        .HasForeignKey(e => e.CourseId);
            
            modelBuilder.Entity<Enrolment>()
                        .HasOne(e => e.Student)
                        .WithMany(s => s.Enrolments)
                        .HasForeignKey(e => e.StudentId);
            
            //Lecturer
            modelBuilder.Entity<Lecturer>().HasOne(l => l.Course);

            //User
            modelBuilder.Entity<Profile>().HasKey(a => a.Id);

            modelBuilder.Entity<Profile>().Property(a => a.Id).ValueGeneratedOnAdd();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            var connectionString = "server = 13.54.17.147; userid = lms_teamaroot; pwd =password; port = 3306; database = lms_teama; sslmode = none";
            optionsBuilder.UseMySQL(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
