using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo1.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDemo1.Controllers
{
    [Route("api/Profile")]
    public class ProfileController : Controller
    {
        private ILMSDataStore _dbstore;
        public ProfileController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllProfiles());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            IActionResult result;
            var profile = _dbstore.GetProfile(Id);
            if (profile != null)
            {
                result = Ok(profile);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Profile profile)
        {
            Profile newProfile = Profile.CreateProfileFromBody(profile);
            _dbstore.AddProfile(newProfile);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] Profile profile)
        {
            _dbstore.EditProfile(Id, profile);
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _dbstore.DeleteProfile(Id);
        }
    }
}
