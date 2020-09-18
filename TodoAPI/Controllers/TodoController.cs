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
        string text = System.IO.File.ReadAllText(@".\Data\todos.txt");

        // GET: api/Todo
        [HttpGet]
        public object Get()
        {
            string sndtxt = "[ " + text + " ]";
            return JsonConvert.DeserializeObject(sndtxt).ToString();
        }


        // POST: api/Todo
        [HttpPost]
        public void Post([FromBody] object value)
        {
            //this is not correct.  figure out how to write the json
            System.IO.File.AppendAllTextAsync(@".\Data\todos.txt", value.ToString()+",");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
