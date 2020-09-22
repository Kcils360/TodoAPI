﻿using System;
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
            var stJson = System.IO.File.ReadAllText(@".\Data\todos.json");
            return JsonSerializer.Deserialize<Todo[]>(stJson).ToArray();
            
        }


        // POST: api/Todo
        [HttpPost]
        public void Post([FromBody] Todo newTodo)
        {
            string path = @".\Data\todos.json";
            Todo tmpTodo = new Todo { title = newTodo.title, completed = newTodo.completed };

            var stJson = System.IO.File.ReadAllText(path);
            var liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();

            List<Todo> tmpAr = new List<Todo>(liJson);
            tmpAr.Add(tmpTodo);
            string str = JsonSerializer.Serialize(tmpAr.ToArray());

            System.IO.File.WriteAllText(path, str);           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //read the json file
            //remove the desired object
            //re-write the whole file
        }

        // UPDATE: api/todo
        [HttpPut]
        public void Put([FromBody] Todo edit)
        {
            string path = @".\Data\todos.json";

            var stJson = System.IO.File.ReadAllText(path);
            List<Todo> liJson = JsonSerializer.Deserialize<Todo[]>(stJson).ToList();

            List<Todo> tmpAr = new List<Todo>(liJson);
            int idx = tmpAr.FindIndex(x => x.title == edit.title);
            tmpAr[idx].completed = edit.completed;

            string str = JsonSerializer.Serialize(tmpAr.ToArray());
            System.IO.File.WriteAllText(path, str);
            //re-write json file
            //return Get();
        }

    }
}
