using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EFDemo1.Model
{
    public class LMSDataStore: ILMSDataStore
    {
        private LMSDbContext _ctx;
        public LMSDataStore(LMSDbContext ctx)
        {
            _ctx = ctx;
        }
       
        //For Course Controller
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var result= await _ctx.Courses
                             .Include(c => c.Lecturer)
                             .Include(c => c.Enrolments)
                             .ThenInclude(e => e.Student)
                             .OrderBy(c => c.Id).ToListAsync();
            return result;
        }
        
        public Course GetCourse(int Id)
        {

            return _ctx.Courses.Include(c => c.Lecturer).SingleOrDefault(x => x.Id == Id);
        }
        
        public void AddCourse(Course course)
        {
            
			_ctx.Courses.Add(course);
            Save();
            
        }

        public void EditCourse(int Id, Course course)
        {
            Course courseToEdit = _ctx.Courses.Find(Id);
            // courseToEdit.Name = course.Name;
            // courseToEdit.MaxNumber = course.MaxNumber;
            // courseToEdit.Credit = course.Credit;
			// courseToEdit.CourseCode = course.CourseCode;
            // courseToEdit.CourseDetail = course.CourseDetail;
            courseToEdit.Lecturer = course.Lecturer;
            _ctx.Courses.Update(courseToEdit);
            Save();
        }
        public void DeleteCourse(int Id)
        {
            var course = _ctx.Courses.Find(Id);
            _ctx.Courses.Remove(course);
            Save();
        }
        //For Student Controller
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var result = await _ctx.Students
                               .Include(s=>s.Enrolments)
                               .ThenInclude(e =>e.Course)
                               .OrderBy(student => student.Id).ToListAsync();
            return result;
        }
        public Student GetStudent(int Id)
        {
            return _ctx.Students.SingleOrDefault(x => x.Id == Id);
        }
        public void AddStudent(Student student)
        {
            _ctx.Students.Add(student);
            Save();
        }
        public void EditStudent(int Id, Student student)
        {
            Student studentToEdit = _ctx.Students.Find(Id);
            studentToEdit.Name = student.Name;
            studentToEdit.StudentFee = student.StudentFee;
            studentToEdit.CreditLimited = student.CreditLimited;
            

            Save();
        }
        public void DeleteStudent(int Id)
        {
            var student=_ctx.Students.Find(Id);
            _ctx.Students.Remove(student);
            Save();
        }

        //For Enrolment Controller
        public void AddEnrolment(int StudentId,int CourseId)
        {
            Student student = _ctx.Students.Find(StudentId);
            Course course = _ctx.Courses.Find(CourseId);
            
			var newEnrol = new Enrolment { StudentId = StudentId, CourseId = CourseId };
            _ctx.Enrolments.Add(newEnrol);
            Save();
        }
        public void DeleteEnrolment(int StudentId,int CourseId)
        {
            var enrol = _ctx.Enrolments.Find(CourseId,StudentId);
            if(enrol!=null){
                _ctx.Enrolments.Remove(enrol);
            }
            Save();
        }

        //For Lecturer Controller
        public async Task<IEnumerable<Lecturer>> GetAllLecturers()
        {
            var result = await _ctx.Lecturers
                               .Include(t=>t.Course)
                               .OrderBy(l => l.Id).ToListAsync();
            return result;
        }
        public Lecturer GetLecturer(int Id)
        {
            return _ctx.Lecturers.SingleOrDefault(x => x.Id == Id);
        }
        public void AddLecturer(Lecturer lecturer)
        {
            _ctx.Lecturers.Add(lecturer);
            Save();
        }
        public void EditLecturer(int Id, Lecturer lecturer)
        {
            Lecturer lecturerToEdit = _ctx.Lecturers.Find(Id);
            lecturerToEdit.Name = lecturer.Name;
            lecturerToEdit.Payroll = lecturer.Payroll;
            lecturerToEdit.Feedback = lecturer.Feedback;
            
            Save();
        }
        public void DeleteLecturer(int Id)
        {
            var lecturer = _ctx.Lecturers.Find(Id);
            _ctx.Lecturers.Remove(lecturer);
            Save();
        }

        //For User Controller
        public IEnumerable<Profile> GetAllProfiles()
        {
            var result = _ctx.Profiles
                
                           .OrderBy(u => u.Id).ToList();
            return result;
        }
        public Profile GetProfile(int Id)
        {
            return _ctx.Profiles.Find(Id);
        }
        public void AddProfile(Profile profile)
        {
            _ctx.Profiles.Add(profile);
            Save();
        }
        public void EditProfile(int Id, Profile profile)
        {
            Profile profileToEdit = _ctx.Profiles.Find(Id);
            profileToEdit.Name = profile.Name;
            profileToEdit.UserId = profile.UserId;
            profileToEdit.PhoneNumber = profile.PhoneNumber;
            profileToEdit.EmailAddress = profile.EmailAddress;

            Save();
        }
        public void DeleteProfile(int Id)
        {
            var profile = _ctx.Profiles.Find(Id);
            _ctx.Profiles.Remove(profile);
            Save();
        }
		public bool Save()
        {
            //True for success , False should throw exception
            return (_ctx.SaveChanges() >= 0);
        }

        public async Task<bool> SaveAsync()
        {
            //True for success , False should throw exception
            var result = await _ctx.SaveChangesAsync();
            return (result >= 0);
        }
    }
}
