using System;

namespace EFDemo1.Model
{
    public class LecturerDetail
    {
        public string Detail { get; set; }
		public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public LecturerDetail()
        {
        }
    }
}
