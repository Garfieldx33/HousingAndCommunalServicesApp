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
    public class StaffApplicationController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            //Как-то достаём все заявки
            var apps = new Application[] { new Application(), new Application() };

            if (apps.Length == 0)
            {
                return NotFound();
            }
            return Ok(apps);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Как-то достаём заявку по id
            return Ok(new Application());
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Application application)
        {
            //Как-то находим заявку по id и изменяем статус
            return Ok();
        }

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
