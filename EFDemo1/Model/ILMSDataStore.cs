using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EFDemo1.Model
{
    public interface ILMSDataStore
    {
        //Course
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(int CourseId);
		void AddCourse(Course course, Assignment assignment);
        void EditCourse(int CourseId, Course course);
        void DeleteCourse(int CourseId);
        //Student
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int StudentId);
        void AddStudent(Student student);
        void EditStudent(int StudentId, Student student);
        void DeleteStudent(int StudentId);
        //Lecturer
        IEnumerable<Lecturer> GetAllLecturers();
        Lecturer GetLecturer(int LecturerId);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(int LecturerId, Lecturer lecturer);
        void DeleteLecturer(int LecturerId);
		//Enrolment
        
		void AddEnrolment(int CourseId,int StudentId);
        void DeleteEnrolment(int CourseId, int StudentId);
		bool Save();
    }
}
