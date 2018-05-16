using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace EFDemo1.Model
{
    public class LMSDataStore:ILMSDataStore
    {
        private LMSDbContext _ctx;
        public LMSDataStore(LMSDbContext ctx)
        {
            _ctx = ctx;
        }
       
        //For Course Controller
        public IEnumerable<Course> GetAllCourses()
        {
            var result=_ctx.Courses
                           .Include(c => c.Assignments)
                           .Include(c => c.Enrolments)
                           .ThenInclude(e => e.Student)
                           .Include(c=>c.Teachings)
                           .ThenInclude(t=>t.Lecturer)
                           .OrderBy(c => c.CourseId).ToList();
            return result;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _ctx.Courses.OrderBy(course => course.CourseId).ToListAsync();
        }
        public Course GetCourse(int CourseId)
        {
            
            return _ctx.Courses.Find(CourseId);
        }
		public void AddCourse(Course course, Assignment assignment)
        {

			_ctx.Courses.Add(course);
			_ctx.Courses.Add(assignment);    
            Save();
            
        }

        public void EditCourse(int CourseId, Course course)
        {
            Course courseToEdit = _ctx.Courses.Find(CourseId);
            courseToEdit.Name = course.Name;
            courseToEdit.MaxNumber = course.MaxNumber;
            courseToEdit.Credit = course.Credit;
            Save();
        }
        public void DeleteCourse(int CourseId)
        {
            var course = _ctx.Courses.Find(CourseId);
            _ctx.Courses.Remove(course);
            Save();
        }
        //For Student Controller
        public IEnumerable<Student> GetAllStudents()
        {
            var result = _ctx.Students
                           .Include(s => s.StudentDetail)
                           .Include(s => s.StudentCountry)
                             .Include(s => s.StudentAddresses)

                             .Include(s=>s.Enrolments)
                             .ThenInclude(e =>e.Course)
                             .ThenInclude(c => c.Assignments)
                           .OrderBy(student => student.StudentId).ToList();


                           return result;
        }
        public Student GetStudent(int StudentId)
        {
            return _ctx.Students.Find(StudentId);
        }
        public void AddStudent(Student student)
        {
            _ctx.Students.Add(student);
            Save();
        }
        public void EditStudent(int StudentId, Student student)
        {
            Student studentToEdit = _ctx.Students.Find(StudentId);
            studentToEdit.Name = student.Name;
            studentToEdit.StudentFee = student.StudentFee;
            studentToEdit.CreditLimited = student.CreditLimited;
            

            Save();
        }
        public void DeleteStudent(int StudentId)
        {
            var student=_ctx.Students.Find(StudentId);
            _ctx.Students.Remove(student);
            Save();
        }
        //For Enrolment Controller

        

        public void AddEnrolment(int StudentId,int CourseId)
        {
            Student student = _ctx.Students.Find(StudentId);
            Course course = _ctx.Courses.Find(CourseId);
            
			var newEnrol = new Enrolment { StudentId = StudentId, CourseId = CourseId };
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
        public IEnumerable<Lecturer> GetAllLecturers()
        {
            var result = _ctx.Lecturers
                .Include(l => l.LecturerDetail)
                             .Include(l=>l.Teachings)
                             .ThenInclude(t=>t.Course)
                           .OrderBy(l => l.LecturerId).ToList();
            return result;
        }
        public Lecturer GetLecturer(int LecturerId)
        {
            return _ctx.Lecturers.Find(LecturerId);
        }
        public void AddLecturer(Lecturer lecturer)
        {
            _ctx.Lecturers.Add(lecturer);
            Save();
        }
        public void EditLecturer(int LecturerId, Lecturer lecturer)
        {
            Lecturer lecturerToEdit = _ctx.Lecturers.Find(LecturerId);
            lecturerToEdit.Name = lecturer.Name;
            lecturerToEdit.Payroll = lecturer.Payroll;
            lecturerToEdit.Feedback = lecturer.Feedback;


            Save();
        }
        public void DeleteLecturer(int LecturerId)
        {
            var lecturer = _ctx.Lecturers.Find(LecturerId);
            _ctx.Lecturers.Remove(lecturer);
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
