using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using EFDemo1.Model;
namespace EFDemo1.Controllers
{
    [Route("api/Student")]
    public class StudentController : Controller
    {

		private ILMSDataStore _dbstore;
        public StudentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbstore.GetAllStudents());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            IActionResult result;
            var student = _dbstore.GetStudent(Id);
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
            _dbstore.Save();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{Id}")]
		public void Put(int Id, [FromBody] Student student)
        {
			_dbstore.EditStudent(Id, student);
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _dbstore.DeleteStudent(Id);
        }
    }
}

