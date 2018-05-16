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

        [HttpGet("{CourseId}")]
        public IActionResult Get(int CourseId)
        {
            IActionResult result;
            var course=_dbstore.GetCourse(CourseId);
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
			Assignment newAssignment = Assignment.CreateAssignmentFromBody(input);
            _dbstore.AddCourse(newCourse,newAssignment);
			_dbstore.Save();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{CourseId}")]
        public void Put(int CourseId, [FromBody] Course course)
        {
			_dbstore.EditCourse(CourseId, course); 
        }

        // DELETE api/values/5
        [HttpDelete("{CourseId}")]
        public void Delete(int CourseId)
        {
			_dbstore.DeleteCourse(CourseId);
        }
    }
}
