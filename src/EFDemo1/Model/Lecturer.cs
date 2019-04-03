using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Lecturer
    {
        public static Lecturer CreateLecturerFromBody(Lecturer lecturerInputModel)
        {
            Lecturer newLecturer = new Lecturer();
            newLecturer.Name = lecturerInputModel.Name;
            newLecturer.Payroll = lecturerInputModel.Payroll;
            newLecturer.Feedback = lecturerInputModel.Feedback;
            newLecturer.Email = lecturerInputModel.Email;
            return newLecturer;
        }

        public int Id { get; set; }
        public string LecturerCode { get; set; }
        public string Name { get; set; }
        public double Payroll { get; set; }
        public string Feedback { get; set; }
		public string Email { get; set; }
        public Course Course { get; set; }
        public Lecturer()
        {
        }
    }
}
