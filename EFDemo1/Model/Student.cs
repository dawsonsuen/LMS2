using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Student
    {
        public static Student CreateStudentFromBody(Student student)
        {
            Student newStudent = new Student();
            newStudent.Name = student.Name;
            newStudent.StudentFee = student.StudentFee;
            newStudent.CreditLimited = student.CreditLimited;
            return newStudent;
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int StudentFee { get; set; }
        public int CreditLimited { get; set; }
        public StudentDetail StudentDetail { get; set; }
        public StudentCountry StudentCountry { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
        public ICollection<StudentAddress> StudentAddresses { get; set; }
        public Student()
        {
        }
    }
}
