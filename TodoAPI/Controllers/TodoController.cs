using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            return JsonConvert.DeserializeObject(text).ToString();
        }


        // POST: api/Todo
        [HttpPost]
        public void Post([FromBody] object value)
        {
            //this is not correct.  figure out how to write the json
            System.IO.File.AppendAllTextAsync(@"C:\Users\Gregory\Desktop\TodoAPI\TodoAPI\Data\todos.json", value.ToString());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        string text = System.IO.File.ReadAllText(@"C:\Users\Gregory\Desktop\TodoAPI\TodoAPI\Data\todos.json");
        
    }
}
