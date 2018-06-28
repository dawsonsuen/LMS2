using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo1.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDemo1.Controllers
{
    [Route("api/Course")]
    public class CourseController : Controller
    {
        private ILMSDataStore _dbstore;
        public CourseController(ILMSDataStore dbstore){
            _dbstore = dbstore;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllCourses());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            IActionResult result;
            var course=_dbstore.GetCourse(Id);
            if(course !=null){
                result = Ok(course);
            }else{
                result = NotFound();
            }
            return result;
        }
        
        // POST api/values
        [HttpPost]
		public IActionResult Post([FromBody]Course input)
        {
            Course newCourse = Course.CreateCourseFromBody(input);
			//Assignment newAssignment = Assignment.CreateAssignmentFromBody(enter);
            _dbstore.AddCourse(newCourse);
			//_dbstore.Save();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] Course course)
        {
			_dbstore.EditCourse(Id, course); 
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
			_dbstore.DeleteCourse(Id);
        }
    }
}
