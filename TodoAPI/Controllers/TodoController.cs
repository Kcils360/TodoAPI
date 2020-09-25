using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TodoAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        

        // GET: api/Todo
        [HttpGet]
        public ActionResult<Todo[]> Get()
        {
            //read the json file, transmit the array only
            var stJson = System.IO.File.ReadAllText(@".\Data\todos.json");
            return Ok(JsonSerializer.Deserialize<Todo[]>(stJson));            
        }


        // POST: api/Todo
        [HttpPost]
        public ActionResult<Todo> Post([FromBody] Todo newTodo)
        {
            //read the json file
            string path = @".\Data\todos.json";
            //convert to a list
            var stJson = System.IO.File.ReadAllText(path);
            var liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
            //add new todo
            liJson.Add(newTodo);
            //re-write file 
            string str = JsonSerializer.Serialize(liJson);
            System.IO.File.WriteAllText(path, str);
            return Ok(newTodo);
        }

        // UPDATE: api/todo
        [HttpPut]
        public ActionResult<Todo> Put([FromBody] Todo edit)
        {
            //read json file
            string path = @".\Data\todos.json";
            var stJson = System.IO.File.ReadAllText(path);
            List<Todo> liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
            //find and edit the requested todo
            Todo item = liJson.Find(i => i.title == edit.title);
            item.completed = edit.completed;
            //re-write json file
            string str = JsonSerializer.Serialize(liJson);
            System.IO.File.WriteAllText(path, str);
            return Ok(item);
        }

        // DELETE: api/ApiWithActions/title
        [HttpDelete("{title}")]
        public ActionResult<string> Delete(string title)
        {
            //read the json file
            string path = @".\Data\todos.json";
            var stJson = System.IO.File.ReadAllText(path);
            var liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
            //remove the desired object
            List<Todo> tmpList = liJson.Where(t => t.title != title).ToList();
            //re-write the whole file
            string str = JsonSerializer.Serialize(tmpList.ToArray());
            System.IO.File.WriteAllText(path, str);
            //return the new list
            return Ok(title);
        }

    }
}
