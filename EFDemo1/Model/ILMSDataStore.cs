using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EFDemo1.Model
{
    public interface ILMSDataStore
    {
        //Course
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(int Id);
		void AddCourse(Course course);
        void EditCourse(int Id, Course course);
        void DeleteCourse(int Id);
        //Student
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int Id);
        void AddStudent(Student student);
        void EditStudent(int Id, Student student);
        void DeleteStudent(int Id);
        //Lecturer
        IEnumerable<Lecturer> GetAllLecturers();
        Lecturer GetLecturer(int Id);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(int Id, Lecturer lecturer);
        void DeleteLecturer(int Id);
		//Enrolment
        
		void AddEnrolment(int CourseId,int StudentId);
        void DeleteEnrolment(int CourseId, int StudentId);
		bool Save();
    }
}
