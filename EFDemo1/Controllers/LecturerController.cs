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

        [HttpGet("{LecturerId}")]
        public IActionResult Get(int LecturerId)
        {
            IActionResult result;
            var lecturer = _dbstore.GetLecturer(LecturerId);
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
        [HttpPut("{LecturerId}")]
        public void Put(int LecturerId, [FromBody] Lecturer lecturer)
        {
			_dbstore.EditLecturer(LecturerId, lecturer);
        }

        // DELETE api/values/5
        [HttpDelete("{LecturerId}")]
        public void Delete(int LecturerId)
        {
			_dbstore.DeleteLecturer(LecturerId);
        }
    }
}