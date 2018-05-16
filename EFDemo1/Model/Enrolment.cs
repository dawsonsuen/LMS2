using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Enrolment
    {
        public static Enrolment CreateEnrolmontFromBody(Enrolment enrolment)
        {
            Enrolment newEnrolment = new Enrolment();
            newEnrolment.CourseGrade = enrolment.CourseGrade;
            newEnrolment.EnrolmentDate = enrolment.EnrolmentDate;
            newEnrolment.Status = enrolment.Status;
            return newEnrolment;
        }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public string CourseGrade { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public string Status { get; set; }
        public Enrolment()
        {
        }
    }
}
