using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Course
    {
        public static Course CreateCourseFromBody(Course courseInputeModel){
            Course newCourse = new Course();
            newCourse.Name = courseInputeModel.Name;
            newCourse.MaxNumber = courseInputeModel.MaxNumber;
            newCourse.Credit = courseInputeModel.Credit;
            newCourse.CourseCode = courseInputeModel.CourseCode;
            newCourse.CourseDetail = courseInputeModel.CourseDetail;
            newCourse.Lecturer = courseInputeModel.Lecturer;
            return newCourse;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxNumber { get; set; }
        public int Credit { get; set; }
        public string CourseCode { get; set; }
        public string CourseDetail { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
        public Course()
        {
        }
    }
}
