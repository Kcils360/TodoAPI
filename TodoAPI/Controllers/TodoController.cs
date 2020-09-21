using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TodoAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        

        // GET: api/Todo
        [HttpGet]
        public object Get()
        {
            //read the json file, transmit the array only
            return JsonSerializer.Deserialize<Todo>(@".\Data\todos.json");
        }


        // POST: api/Todo
        [HttpPost]
        public void Post([FromBody] object value)
        {
            //this is not correct.  figure out how to write the json
            System.IO.File.AppendAllText(@".\Data\todos.txt", value.ToString()+",");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //read the json file
            //remove the desired object
            //re-write the whole file
        }

        
    }
}
