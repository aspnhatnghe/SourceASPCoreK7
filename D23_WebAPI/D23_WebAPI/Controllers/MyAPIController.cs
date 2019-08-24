using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D23_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D23_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAPIController : ControllerBase
    {
        static List<string> myList = new List<string>() { "value 1", "value 2"};
        // GET: api/MyAPI
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return myList;
        }

        // GET: api/MyAPI/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute] int id)
        {
            if (id < 0 || id >= myList.Count)
                return BadRequest();
            return this.Ok(myList[id]);
        }

        // POST: api/MyAPI
        [HttpPost]
        public void Post([FromBody] MyModel value)
        {
            myList.Add(value.Data);
        }

        // PUT: api/MyAPI/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MyModel value)
        {
            if (id < 0 || id >= myList.Count)
                return BadRequest();

            myList[id] = value.Data;
            return this.Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id >= myList.Count)
                return BadRequest();
            myList.RemoveAt(id);
            return this.Ok();
        }
    }
}
