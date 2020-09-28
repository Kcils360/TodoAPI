using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace TodoAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        string _path = @".\Data\todos.json";

        // GET: api/Todo
        [HttpGet]
        public ActionResult<Todo[]> Get()
        {
            string stJson = System.IO.File.ReadAllText(_path);
            return Ok(JsonSerializer.Deserialize<Todo[]>(stJson));            
        }


        // POST: api/Todo
        [HttpPost]
        public ActionResult<Todo> Post([FromBody] Todo newTodo)
        {
            var liJson = readJson(_path);
            liJson.Add(newTodo);
            writeJson(liJson);
            return Ok(newTodo);
        }

        // UPDATE: api/todo
        [HttpPut]
        public ActionResult<Todo> Put([FromBody] Todo edit)
        {
            var liJson = readJson(_path);
            Todo item = liJson.Find(i => i.title == edit.title);
            item.completed = edit.completed;
            writeJson(liJson);
            return Ok(item);
        }

        // DELETE: api/ApiWithActions/title
        [HttpDelete("{title}")]
        public ActionResult<string> Delete(string title)
        {
            var liJson = readJson(_path);
            List<Todo> tmpList = liJson.Where(t => t.title != title).ToList();
            writeJson(tmpList);
            return Ok(title);
        }

        //helper methods
        private List<Todo> readJson(string path)
        {
            string stJson = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<Todo[]>(stJson).ToList();
        }

        private void writeJson(List<Todo> list)
        {
            string str = JsonSerializer.Serialize(list);
            System.IO.File.WriteAllText(_path, str);
        }

    }
}
