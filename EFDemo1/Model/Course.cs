using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Course
    {
        public static Course CreateCourseFromBody(Course course){
            Course newCourse = new Course();
            newCourse.Name = course.Name;
            newCourse.MaxNumber = course.MaxNumber;
            newCourse.Credit = course.Credit;
            newCourse.CourseCode = course.CourseCode;
            //newCourse.Description = course.Description;
            return newCourse;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxNumber { get; set; }
        public int Credit { get; set; }
        public string CourseCode { get; set; }
        //public string Description { get; set; }
        public ICollection<Teaching> Teachings { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public Course()
        {
        }
    }
}
