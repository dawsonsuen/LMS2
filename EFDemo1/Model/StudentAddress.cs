using System;

namespace EFDemo1.Model
{
    public class StudentAddress
    {
        public int StudentId { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public Student Student { get; set; }
        public StudentAddress()
        {
        }
    }
}
