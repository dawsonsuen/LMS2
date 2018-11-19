using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo1.Model;
using Microsoft.AspNetCore.Cors;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbstore.GetAllCourses());
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
            _dbstore.AddCourse(newCourse);
			_dbstore.Save();
            return Ok();
        }

        // PUT api/values/id
        [HttpPut("{Id}")]
        
        public void Put(int Id, [FromBody] Course course)
        {
			_dbstore.EditCourse(Id, course); 
        }

        // DELETE api/values/id
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
			_dbstore.DeleteCourse(Id);
        }
    }
}
