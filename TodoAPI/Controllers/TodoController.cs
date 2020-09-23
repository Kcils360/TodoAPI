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
        public object Get()
        {
            //read the json file, transmit the array only
            var stJson = System.IO.File.ReadAllText(@".\Data\todos.json");
            return JsonSerializer.Deserialize<Todo[]>(stJson).ToArray();
            
        }


        // POST: api/Todo
        [HttpPost]
        public void Post([FromBody] Todo newTodo)
        {
            //read the json file
            string path = @".\Data\todos.json";
            Todo tmpTodo = new Todo { title = newTodo.title, completed = newTodo.completed };
            //convert to a list
            var stJson = System.IO.File.ReadAllText(path);
            var liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
            //add new todo
            List<Todo> tmpAr = new List<Todo>(liJson);
            tmpAr.Add(tmpTodo);
            //re-write file 
            string str = JsonSerializer.Serialize(tmpAr.ToArray());
            System.IO.File.WriteAllText(path, str);           
        }

        // UPDATE: api/todo
        [HttpPut]
        public void Put([FromBody] Todo edit)
        {
            //read json file
            string path = @".\Data\todos.json";
            var stJson = System.IO.File.ReadAllText(path);
            List<Todo> liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
            //find and edit the requested todo
            List<Todo> tmpAr = new List<Todo>(liJson);
            int idx = tmpAr.FindIndex(x => x.title == edit.title);
            tmpAr[idx].completed = edit.completed;
            //re-write json file
            string str = JsonSerializer.Serialize(tmpAr.ToArray());
            System.IO.File.WriteAllText(path, str);
        }

        // DELETE: api/ApiWithActions/title
        [HttpDelete("{title}")]
        public object Delete(string title)
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
            return tmpList.ToArray();
        }

    }
}
