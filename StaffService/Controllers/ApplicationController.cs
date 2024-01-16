using Microsoft.AspNetCore.Mvc;
using CommonLib.DAL;
using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Application> Get()
        {
            return new Application[] { new Application(), new Application() };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Application Get(int id)
        {
            return new Application();
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Application application)
        {
            return Ok();
        }

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
