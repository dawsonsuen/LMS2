using System;
using Microsoft.EntityFrameworkCore;

namespace EFDemo1.Model
{
    public class LMSDbContext: DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public LMSDbContext(DbContextOptions<LMSDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Course
            modelBuilder.Entity<Course>().HasKey(a => a.CourseId);
            modelBuilder.Entity<Course>().Property(a => a.CourseId).ValueGeneratedOnAdd();
            //Assignment(one Course to many)
            modelBuilder.Entity<Assignment>().HasKey(a => a.Code);

            modelBuilder.Entity<Assignment>().Property(a => a.Code).ValueGeneratedOnAdd();
            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Assignments)
                        .WithOne(a => a.Course);
            //Student
            modelBuilder.Entity<Student>().HasKey(a => a.StudentId);
            modelBuilder.Entity<Student>().Property(a => a.StudentId).ValueGeneratedOnAdd();

            //StudentDetail(one to one)
            modelBuilder.Entity<StudentDetail>().HasKey(a => a.StudentId);

            modelBuilder.Entity<StudentDetail>()
                        .Property(a => a.StudentId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>()
                        .HasOne(student => student.StudentDetail)
                        .WithOne(stdDetail => stdDetail.Student)
                        .HasForeignKey<StudentDetail>(stdDetail => stdDetail.StudentId);

            //Student Country(one to one)
            modelBuilder.Entity<StudentCountry>()
                        .HasKey(a => a.StudentId);
            modelBuilder.Entity<StudentCountry>()
                        .Property(a => a.StudentId).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Student>()
                        .HasOne(std => std.StudentCountry)
                        .WithOne(stdc => stdc.Student)
                        .HasForeignKey<StudentCountry>(stdc => stdc.StudentId);
            //Student Address(one Student to many)
            modelBuilder.Entity<StudentAddress>()
                       .HasKey(a => a.StudentId);
            modelBuilder.Entity<StudentAddress>()
                        .Property(a => a.StudentId).ValueGeneratedOnAdd();
           
            modelBuilder.Entity<Student>()
                        .HasMany(s => s.StudentAddresses)
                        .WithOne(a => a.Student);
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
            modelBuilder.Entity<Lecturer>().HasKey(a => a.LecturerId);

            modelBuilder.Entity<Lecturer>().Property(a => a.LecturerId).ValueGeneratedOnAdd();
           
            //LecturerDetail(one Lecturer to one LecturerDetail)




			modelBuilder.Entity<LecturerDetail>().HasKey(a => a.LecturerId);
            modelBuilder.Entity<LecturerDetail>()
			            .Property(a => a.LecturerId).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Lecturer>()
                        .HasOne(l => l.LecturerDetail)
                        .WithOne(lDetail => lDetail.Lecturer)
                        .HasForeignKey<LecturerDetail>(lDetail => lDetail.LecturerId);
            //Teaching(many Course, many Lecturer to many)
            modelBuilder.Entity<Teaching>()
                        .HasKey(t => new { t.CourseId, t.LecturerId });

            modelBuilder.Entity<Teaching>()
                        .HasOne(t => t.Course)
                        .WithMany(c => c.Teachings)
                        .HasForeignKey(t => t.CourseId);
            modelBuilder.Entity<Teaching>()
                        .HasOne(t => t.Lecturer)
                        .WithMany(l => l.Teachings)
                        .HasForeignKey(t => t.LecturerId);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
			var connectionString = "server = localhost; userid = root; pwd =; port = 3306; database = lms_teama; sslmode = none";
            optionsBuilder.UseMySQL(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
