using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebRestApp.Models;
namespace WebRestApp.Controllers
{
    [Produces("application/json")]
    [Route("api/ToDo")]
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _toDoDbContext;

        public ToDoController(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext; // Add the timmer 
            if (_toDoDbContext.todoItemes.Count() == 0)
            {
                _toDoDbContext.todoItemes.Add(new ToDoItems { Name = "Item1" });
                _toDoDbContext.SaveChanges();

            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _toDoDbContext.todoItemes.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItems item)
        {
            if (item == null)
            {
                NotFound();
            }
            _toDoDbContext.todoItemes.Add(item);
            _toDoDbContext.SaveChanges();

            return CreatedAtRoute("GoTodo", new { id = item.Id }, item);
        }

        //Update the 
        [HttpPut("{Id}")]
        public IActionResult Update(long id, [FromBody] ToDoItems items)
        {
            
            if(items == null || items.Id != id)
            {
                BadRequest("It is bad request");

            }

            var Items = _toDoDbContext.todoItemes.FirstOrDefault(c => c.Id != id);

            if (items == null)
            {
                BadRequest("It is the best managed to forms the!");
            }
            Items.IsComplete = items.IsComplete;
            Items.Name = items.Name;
            _toDoDbContext.todoItemes.Update(Items); //Update DB

            return new NoContentResult();

        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(long id)
        {
            var todo = _toDoDbContext.todoItemes.Where(m => m.Id == id).FirstOrDefault();
            if (todo == null)
            {
                BadRequest("The item to be deleted is not found.");
            }

            _toDoDbContext.todoItemes.Remove(todo);

            return new NoContentResult();
        }

    }
}