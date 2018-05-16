using System;

namespace EFDemo1.Model
{
    public class StudentCountry
    {
        public int StudentId { get; set; }
        public string Country { get; set; }
        public Student Student { get; set; }

        public StudentCountry()
        {
        }
    }
}
