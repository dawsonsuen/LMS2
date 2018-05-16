using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo1.Model;
using EFDemo1.Model.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDemo1.Controllers
{
    [Route("api/Enrolment")]
    public class EnrolmentController : Controller
    {
        private ILMSDataStore _dbstore;
        public EnrolmentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        // POST api/values
        [HttpPost()]
        public void Post([FromBody]EnrolmentDTO value)
        {
            _dbstore.AddEnrolment(value.StudentId, value.CourseId);
        }
        [HttpDelete]
        public void Delete([FromBody]EnrolmentDTO value)
        {
            _dbstore.DeleteEnrolment(value.StudentId, value.CourseId);
        }
    }
}
