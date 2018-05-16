using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Lecturer
    {
        public static Lecturer CreateLecturerFromBody(Lecturer lecturer)
        {
            Lecturer newLecturer = new Lecturer();
            newLecturer.Name = lecturer.Name;
            newLecturer.Payroll = lecturer.Payroll;
            newLecturer.Feedback = lecturer.Feedback;

            return newLecturer;
        }

        public int LecturerId { get; set; }
        public string Name { get; set; }
        public double Payroll { get; set; }
        public string Feedback { get; set; }
		public LecturerDetail LecturerDetail { get; set; }
        public ICollection<Teaching> Teachings { get; set; }
        public Lecturer()
        {
        }
    }
}
