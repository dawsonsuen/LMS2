using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EFDemo1.Model
{
    public class Student
    {

        public static Student CreateStudentFromBody(Student studentInputModel)
        {
            Student newStudent = new Student();
            newStudent.Name = studentInputModel.Name;
            newStudent.StudentFee = studentInputModel.StudentFee;
            newStudent.CreditLimited = studentInputModel.CreditLimited;

            return newStudent;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentFee { get; set; }
        public int CreditLimited { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
        public Student()
        {
        }
    }
}
