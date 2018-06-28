using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo1.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDemo1.Controllers
{
    [Route("api/Lecturer")]
    public class LecturerController : Controller
    {
        private ILMSDataStore _dbstore;
        public LecturerController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllLecturers());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            IActionResult result;
            var lecturer = _dbstore.GetLecturer(Id);
            if (lecturer != null)
            {
                result = Ok(lecturer);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Lecturer lecturer)
        {
            var newLecturer = Lecturer.CreateLecturerFromBody(lecturer);
            _dbstore.AddLecturer(newLecturer);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] Lecturer lecturer)
        {
			_dbstore.EditLecturer(Id, lecturer);
        }

        // DELETE api/values/5
        [HttpDelete("{LecturerId}")]
        public void Delete(int Id)
        {
			_dbstore.DeleteLecturer(Id);
        }
    }
}