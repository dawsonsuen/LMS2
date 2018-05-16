using System;

namespace EFDemo1.Model
{
    public class Teaching
    {
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
        public Course Course { get; set; }
        public Lecturer Lecturer { get; set; }


        public Teaching()
        {
        }
    }
}
