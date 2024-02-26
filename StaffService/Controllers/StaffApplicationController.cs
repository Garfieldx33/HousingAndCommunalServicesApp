using Microsoft.AspNetCore.Mvc;
using CommonLib.DAL;
using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Channels;
using Grpc.Net.Client;
using StaffService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StaffApplicationController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAllApplications()
        {
            var grpc = new StaffSerciceGrpc();

            //Достаём все заявки
            var apps = grpc.GetAllApplications();

            if (apps.Count == 0)
            {
                return NotFound();
            }
            return Ok(apps);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetApplicationById(int id)
        {
            var grpc = new StaffSerciceGrpc();

            //Достаём заявку по id
            var app = grpc.GetApplicationById(id);

            return Ok(app);
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult PutApplication(Application application)
        {
            var grpc = new StaffSerciceGrpc();

            grpc.UpdateApplication(application);//Находим заявку по id и изменяем статус

            return Ok();
        }

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
