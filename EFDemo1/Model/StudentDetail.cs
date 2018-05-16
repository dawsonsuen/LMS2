using System;

namespace EFDemo1.Model
{
    public class StudentDetail
    {
    public string Detail { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public StudentDetail()
        {
        }
    }
}
