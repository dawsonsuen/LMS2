using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using EFDemo1.Model;
namespace EFDemo1.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {

		private ILMSDataStore _dbstore;
        public StudentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllStudents());
        }

        [HttpGet("{StudentId}")]
        public IActionResult Get(int StudentId)
        {
            IActionResult result;
            var student = _dbstore.GetStudent(StudentId);
            if (student != null)
            {
                result = Ok(student);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Student input)
        {
            Student newStudent = Student.CreateStudentFromBody(input);
			_dbstore.AddStudent(newStudent);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{StudentId}")]
		public void Put(int StudentId, [FromBody] Student student)
        {
			_dbstore.EditStudent(StudentId, student);
        }

        // DELETE api/values/5
        [HttpDelete("{StudentId}")]
        public void Delete(int StudentId)
        {
            _dbstore.DeleteStudent(StudentId);
        }
    }
}

