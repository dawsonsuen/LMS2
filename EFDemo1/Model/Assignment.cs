using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
	
    public class Assignment
    {
		public static Assignment CreateAssignmentFromBody(Assignment assignment)
        {
            Assignment newAssignment = new Assignment();
            newAssignment.Code = assignment.Code;
            newAssignment.Detail = assignment.Detail;

            return newAssignment;
        }
        public int Code { get; set; }
        public string Detail { get; set; }
        public Course Course { get; set; }
        public Assignment()
        {
        }
    }
}
