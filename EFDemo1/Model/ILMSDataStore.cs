using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EFDemo1.Model
{
    public interface ILMSDataStore
    {
        //Course
        Task<IEnumerable<Course>> GetAllCourses();
        Course GetCourse(int id);
        void AddCourse(Course course);
        void EditCourse(int id, Course course);
        void DeleteCourse(int id);
        //Student
        Task<IEnumerable<Student>> GetAllStudents();
        Student GetStudent(int id);
        void AddStudent(Student student);
        void EditStudent(int id, Student student);
        void DeleteStudent(int id);
        //Lecturer
        Task<IEnumerable<Lecturer>> GetAllLecturers();
        Lecturer GetLecturer(int id);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(int id, Lecturer lecturer);
        void DeleteLecturer(int id);
        //User
        IEnumerable<Profile> GetAllProfiles();
        Profile GetProfile(int id);
        void AddProfile(Profile profile);
        void EditProfile(int id, Profile profile);
        void DeleteProfile(int id);
		//Enrolment
        
		void AddEnrolment(int studentId,int courseId);
        void DeleteEnrolment(int studentId, int courseId);
		bool Save();
    }
}
